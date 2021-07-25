using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBoss : MonoBehaviour, IEntityStats
{
    
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;
    public GameObject[] sunBeamBulletPatterns;


    /*ENTITY STATS INTERFACE DATA MEMBERS*/
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage)
    {
        if (_health - damage <= 0)
        {
            die();
        }
        _health -= damage;
        BHealthUI.SetCurrentHealth(health);
    }

    public void heal(float amount)
    {
        BHealthUI.SetCurrentHealth(health);
    }

    public void die()
    {
        bossDialogue.StartDialogue();
    }

    // Start is called before the first frame update
    void Start()
    {
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
