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
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
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
        _health -= damage;
        BHealthUI.SetCurrentHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
