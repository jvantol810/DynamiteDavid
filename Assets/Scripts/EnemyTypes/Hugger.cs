using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour, IEntityStats
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed = 2;
    public float damage = 1;

    /*ENTITY STATS INTERFACE DATA MEMBERS*/
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }
    
    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage) {
        if(_health - damage <= 0)
        {
            die();
        }
        _health -= damage;
    }
    
    public void heal(float amount) {}

    public void die()
    {
        Destroy(this);
    }

    /*METHODS NOT INCLUDED IN ENTITY STATS INTERFACE*/
    private void ChasePlayer()
    {
        //Hugger enemy gets player position and follows
        Vector2 playerPos = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.fixedDeltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collison)
    {
        if (collison.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().takeDamage(damage * Time.fixedDeltaTime);
        }
    }
    
    public void OnCollisionStay2D(Collision2D collison)
    {
        if (collison.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerStats>().takeDamage(damage * Time.fixedDeltaTime);
        }
    }

    void Awake()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChasePlayer();
        
        
    }
}
