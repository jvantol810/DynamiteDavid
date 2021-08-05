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
    [Header("Boss Thresholds")]
    public List<float> healthThresholds;
    private int currentThresholdIndex = 0;
    private int nextThresholdIndex = 1;
    private float currentThreshold;
    private float nextThreshold;
    private BaseBossBehavior behavior;
    
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
    public Sprite hurtSprite;
    public Sprite normalSprite;
    public Sprite epicSprite;
    private float hurtTimer = 0;
    private bool hurt = false;
    public void Awake()
    {
        behavior = GetComponent<BaseBossBehavior>();
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
            
            //Increment health threshold, checking if it's at the end
            //if(currentThresholdIndex + 1 < healthThresholds.Count)
            //{
            //    currentThresholdIndex++;
            //}
            ////Trigger next phase
            //behavior.nextActionChain();
            //Debug.Log("Current threshold: " + currentThresholdIndex);
            //Debug.Log("Next Threshold: " + nextThresholdIndex);
            //behavior.nextActionChain();
            
            //if(nextThresholdIndex + 1 >= healthThresholds.Count)
            //{
               
            //    nextThresholdIndex = -1;
            //}
            //else
            //{
            //    nextThresholdIndex++;
            //}
            //if(currentThresholdIndex + 1 >= healthThresholds.Count)
            //{
            //    currentThresholdIndex = healthThresholds.Count - 1;
            //}
            //else
            //{
            //    currentThresholdIndex++;
            //}
            //if(currentThresholdIndex != -1)
            //{
            //    currentThreshold = healthThresholds[currentThresholdIndex];
            //}
            //if(nextThresholdIndex != -1)
            //{
            //    nextThreshold = healthThresholds[nextThresholdIndex];
            //}
            //if(currentThresholdIndex != -1)
            //{
            //    behavior.currentActionChainIndex = currentThresholdIndex;
            //}

            
            //Debug.Log("After incrementing - Current threshold: " + currentThresholdIndex);
            //Debug.Log("After incrementing - Next Threshold: " + nextThresholdIndex);
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
                if(health <= 22)
                {
                    GetComponent<SpriteRenderer>().sprite = epicSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = normalSprite;
                }
                
                hurtTimer = 0.5f;
                hurt = false;
            }
        }
       
    }
}
