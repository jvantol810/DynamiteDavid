using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupernovaBoss : MonoBehaviour, IEntityStats
{
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;
    public GameObject[] novaBulletPatterns;
    public GameObject ShockwavePattern;
    public GameObject Shield;
    public Sprite normalSprite;
    public Sprite attackingSprite;
    public Sprite hurtSprite;
    public Sprite deadSprite;
    public GameObject supernovaFlash;

    /*ENTITY STATS INTERFACE DATA MEMBERS*/
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    
    bool fightStarted;
    private bool dead;
    private SpriteRenderer sr;
    private bool damageEffectActive = false;
    private float damageEffectTimer = 0f;
    int lastBulletPatternUsed;
    [Header("Supernova State")]
    [SerializeField]
    private int attacksSinceBarrier;


    [Header("Movement")]
    [SerializeField]
    private string attackPhase;
    [SerializeField]
    private Vector3 patternPos;

    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage)
    {
        if (fightStarted)
        {
            if (_health - damage <= 0)
            {
                die();
            }
            _health -= damage;
            damageEffectTimer = .2f;
            damageEffectActive = true;
            BHealthUI.SetCurrentHealth(health);
        }
    }

    public void heal(float amount)
    {
        BHealthUI.SetCurrentHealth(health);
    }

    public void die()
    {
        if(!dead)
        {
            StartCoroutine(EndBoss());
            dead = true;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        fightStarted = false;
        attackPhase = "None";
        sr = GetComponent<SpriteRenderer>();
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
        lastBulletPatternUsed = -1;
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && fightStarted)
        {
            if (damageEffectActive)
            {
                UpdateDamageEffectTimer();
                sr.color = new Color(1f, .4f, .4f, 1f);
                sr.sprite = hurtSprite;
            }
            else
            {
                sr.color = new Color(1f, 1f, 1f, 1f);
                if (attackPhase == "None")
                {
                    sr.sprite = normalSprite;
                }
                else
                {
                    sr.sprite = attackingSprite;
                }
            }
        }
        else
        {
            if (supernovaFlash.activeInHierarchy)
            {
                supernovaFlash.transform.localScale = supernovaFlash.transform.localScale + (new Vector3(1f, 1f, 1f) * Time.deltaTime);
                supernovaFlash.transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * 30f));
            }
        }
        transform.position = Vector2.Lerp(transform.position, patternPos, Time.deltaTime * 3f);
    }


    private void UpdateDamageEffectTimer()
    {
        damageEffectTimer -= Time.deltaTime;
        if (damageEffectTimer <= 0f)
        {
            damageEffectTimer = 0f;
            damageEffectActive = false;
        }
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(.1f);
        BHealthUI.SetLabel("Supernova    ");
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
        yield return new WaitForSeconds(2f);
        patternPos = new Vector3(0f, 3f, 0f);
        fightStarted = true;
        Shield.SetActive(true);
        yield return new WaitForSeconds(.8f);
        StartCoroutine(NextAttackPhase());
    }

    private IEnumerator NextAttackPhase()
    {
        if(!Shield.activeInHierarchy)
        {
            attacksSinceBarrier++;
        }
        int randRoll = ReturnNewSkill();
        GameObject currentAttackPattern = novaBulletPatterns[randRoll];
        attackPhase = currentAttackPattern.name;
        lastBulletPatternUsed = randRoll;
        currentAttackPattern.SetActive(true);
        currentAttackPattern.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(5f);
        currentAttackPattern.SetActive(false);
        yield return new WaitForSeconds(8f);
        TryCreateBarrier();
        if (health > 0f)
        {
            StartCoroutine(NextAttackPhase());
        }
    }
    private void TryCreateBarrier()
    {
        if(!Shield.activeInHierarchy && attacksSinceBarrier >= 3)
        {
            Shield.SetActive(true);
            Shield.GetComponent<Barrier>().AwakeAgain();
        }
    }

    private int ReturnNewSkill()
    {
        int roll;
        if(novaBulletPatterns.Length == 1)
        {
            //return 0;
        }
        //int roll = Random.Range(0, sunBeamBulletPatterns.Length);
        do
        {
            roll = Random.Range(0, novaBulletPatterns.Length);
        } while (roll == lastBulletPatternUsed);
        return roll;
    }

    private void DisableAttacks()
    {
        for (int i = 0; i < novaBulletPatterns.Length; i++)
        {
            novaBulletPatterns[i].SetActive(false);
        }
    }

    private IEnumerator EndBoss()
    {
        DisableAttacks();
        yield return new WaitForSeconds(5f);
        patternPos = new Vector3(0f, 3f, 0f);
        sr.sprite = deadSprite;
        yield return new WaitForSeconds(1f);
        supernovaFlash.SetActive(true);
        bossDialogue.StartDialogue();
    }

}
