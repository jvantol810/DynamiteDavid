using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IEntityStats
{
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
    public void heal(float amount) {
        if(_health + amount > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += amount;
        }
    }

    public void die()
    {
        Debug.Log("PLAYER DIED");
    }

    /*METHODS NOT INCLUDED IN ENTITY STATS INTERFACE*/
   
}