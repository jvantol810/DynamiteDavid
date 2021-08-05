using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerStats : MonoBehaviour, IEntityStats
{
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
    public Sprite normalSprite;
    public Sprite hurtSprite;
    private float hurtTimer = 0;
    private bool hurt = false;

    public void takeDamage(float damage)
    {
        hurt = true;
        _health -= damage;
        if(_health <= 0)
        {
            die();
        }
    }

    public void heal(float increase)
    {

    }

    public void die()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
