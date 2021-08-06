using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EMPEricAI : MonoBehaviour, IEntityStats
{

    public BossHealthUI bossHealthUI;

    [Header("IEntityStats")]
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite spStage1;
    public Sprite spStage2;
    public Sprite spStage3;

    [Header("Lights")]
    public Light2D[] stageLights;

    private bool midAction = false;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthUI.SetMaxHealth(health);
        bossHealthUI.SetCurrentHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float amt) 
    {
        if (health - amt <= 0) {
            die();
        }
        health -= amt;
    }

    public void heal(float amt) 
    {

    }

    public void die() 
    {

    }
}
