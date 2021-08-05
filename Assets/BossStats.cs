using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour, IEntityStats
{
    public BossHealthUI BHealthUI;
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    private List<float> healthThresholds;
    private int currentThresholdIndex = 0;
    private int nextThresholdIndex = 1;
    private float currentThreshold;
    private float nextThreshold;
    private BaseBossBehavior behavior;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
    public Sprite hurtSprite;
    public Sprite normalSprite;
    private float hurtTimer = 0;
    private bool hurt = false;
    public void Awake()
    {
        behavior = GetComponent<BaseBossBehavior>();
        healthThresholds = new List<float>();
        healthThresholds.Add(70f);
        healthThresholds.Add(40f);
        healthThresholds.Add(10f);
        currentThreshold = healthThresholds[currentThresholdIndex];
        nextThreshold = healthThresholds[nextThresholdIndex];
        
    }
    public void die()
    {
        Destroy(gameObject);
    }

    public void heal(float increase)
    {
        _health += increase;
        BHealthUI.SetCurrentHealth(health);
    }

    // Start is called before the first frame update
    void Start()
    {
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
    }

    public void takeDamage(float damage)
    {
        
        hurt = true;
        _health -= damage;
        BHealthUI.SetCurrentHealth(health);
        if (_health <= 0)
        {
            die();
        }
        else if(_health <= currentThreshold)
        {
            Debug.Log("Current threshold: " + currentThreshold);
            Debug.Log("Next Threshold: " + nextThreshold);
            behavior.nextActionChain();
            if(nextThresholdIndex >= healthThresholds.Count)
            {
               
                nextThresholdIndex = 0;
                Debug.Log("Next threshold set to 0!");
            }
            else
            {
                nextThresholdIndex++;
            }
            currentThresholdIndex++;
            currentThreshold = healthThresholds[currentThresholdIndex];
            nextThreshold = healthThresholds[nextThresholdIndex];
            Debug.Log("After incrementing - Current threshold: " + currentThreshold);
            Debug.Log("After incrementing - Next Threshold: " + nextThreshold);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hurt)
        {
            if (hurtTimer > 0)
            {
                GetComponent<SpriteRenderer>().sprite = hurtSprite;
                hurtTimer -= Time.deltaTime;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = normalSprite;
                hurtTimer = 0.5f;
                hurt = false;
            }
        }
       
    }
}
