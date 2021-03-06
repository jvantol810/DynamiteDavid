using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBoss : MonoBehaviour, IEntityStats
{
    
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;
    public GameObject[] sunBeamBulletPatterns;
    public int lastBulletPatternUsed;
    public Sprite normalSprite;
    public Sprite attackingSprite;
    public Sprite hurtSprite;
    public Sprite deadSprite;
    public GameObject supernovaFlash;
    public GameObject supernovaBoss;
    private bool dead;

    /*ENTITY STATS INTERFACE DATA MEMBERS*/
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
    [Header("Fun Coloring")]
    [SerializeField]
    private Color currentDamageColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField]
    private float colorScale = 1f;
    private SpriteRenderer sr;
    private bool damageEffectActive = false;
    private float damageEffectTimer = 0f;
    [Header("Movement")]
    [SerializeField]
    private string attackPhase;
    [SerializeField]
    private Vector3 patternPos;

    bool fightStarted;
    //Scale from 1f, 1f, 1f, 1f to 1f, .6f, .6f, 1f

    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage)
    {
        if(fightStarted)
        {
            if (_health - damage <= 0)
            {
                die();
            }
            _health -= damage;
            damageEffectTimer = .2f;
            damageEffectActive = true;
            BHealthUI.SetCurrentHealth(health);
            colorScale = .6f + (0.4f * 1 / (maxHealth / health));
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
            dead = true;
            StopAllCoroutines();
            StartCoroutine(EndBoss());
            
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        fightStarted = false;
        sr = GetComponent<SpriteRenderer>();
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
        lastBulletPatternUsed = -1;
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead && fightStarted)
        {
        currentDamageColor = new Color(1f, colorScale, colorScale, 1f);
        if(damageEffectActive)
        {
            UpdateDamageEffectTimer();
            sr.color = new Color(1f, .4f, .4f, 1f);
            sr.sprite = hurtSprite;
        }
        else
        {
            if (sr.color != currentDamageColor)
            {
                sr.color = currentDamageColor;
            }
            if(attackPhase == "None")
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
            if(supernovaFlash.activeInHierarchy)
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
        if(damageEffectTimer <= 0f)
        {
            damageEffectTimer = 0f;
            damageEffectActive = false;
        }
    }

    private IEnumerator NextAttackPhase()
    {
        int randRoll = ReturnNewSkill();
        lastBulletPatternUsed = randRoll;
        GameObject currentAttackPattern = sunBeamBulletPatterns[randRoll];
        attackPhase = currentAttackPattern.name;
        
        currentAttackPattern.SetActive(true);
        patternPos = currentAttackPattern.transform.position;
        yield return new WaitForSeconds(0.4f);
        currentAttackPattern.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(5f);
        currentAttackPattern.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        patternPos = new Vector3(0f, 3f, 0f);
        attackPhase = "None";
        yield return new WaitForSeconds(3.5f);
        if (health > 0f)
        {
            StartCoroutine(NextAttackPhase());
        }
    }

    private IEnumerator EndBoss()
    {
        DisableAttacks();
        yield return new WaitForSeconds(1.4f);
        patternPos = new Vector3(0f, 3f, 0f);
        yield return new WaitForSeconds(1f);
        sr.sprite = deadSprite;
        bossDialogue.StartDialogue();
        supernovaFlash.SetActive(true);
        yield return new WaitForSeconds(3f);
        supernovaFlash.SetActive(false);
        supernovaBoss.SetActive(true);
        gameObject.SetActive(false);
    }

    private void DisableAttacks()
    {
        for(int i = 0; i < sunBeamBulletPatterns.Length; i++)
        {
            sunBeamBulletPatterns[i].SetActive(false);
        }
    }

    private int ReturnNewSkill()
    {
        int roll;
        //int roll = Random.Range(0, sunBeamBulletPatterns.Length);
        do
        {
            roll = Random.Range(0, sunBeamBulletPatterns.Length);
        } while (roll == lastBulletPatternUsed);
        return roll;
    }

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(.1f);
        bossDialogue.StartDialogue();
        yield return new WaitForSeconds(6f);
        fightStarted = true;
        StartCoroutine(NextAttackPhase());
    }
}
