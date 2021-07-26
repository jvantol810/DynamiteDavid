using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBoss : MonoBehaviour, IEntityStats
{
    
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;
    public GameObject[] sunBeamBulletPatterns;
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
    //Scale from 1f, 1f, 1f, 1f to 1f, .6f, .6f, 1f

    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage)
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

    public void heal(float amount)
    {
        BHealthUI.SetCurrentHealth(health);
    }

    public void die()
    {
        if(!dead)
        {
            dead = true;
            bossDialogue.StartDialogue();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
        StartCoroutine(NextAttackPhase());
    }

    // Update is called once per frame
    void Update()
    {
        currentDamageColor = new Color(1f, colorScale, colorScale, 1f);
        if(damageEffectActive)
        {
            UpdateDamageEffectTimer();
            sr.color = new Color(1f, .4f, .4f, 1f);
        }
        else
        {
            if (sr.color != currentDamageColor)
            {
                sr.color = currentDamageColor;
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
        GameObject currentAttackPattern = sunBeamBulletPatterns[Random.Range(0, sunBeamBulletPatterns.Length)];
        attackPhase = currentAttackPattern.name;
        
        currentAttackPattern.SetActive(true);
        patternPos = currentAttackPattern.transform.position;
        yield return new WaitForSeconds(0.4f);
        currentAttackPattern.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(5f);
        patternPos = new Vector3(0f, 3f, 0f);
        currentAttackPattern.SetActive(false);
        yield return new WaitForSeconds(5f);
        if (health > 0f)
        {
            StartCoroutine(NextAttackPhase());
        }
    }

}
