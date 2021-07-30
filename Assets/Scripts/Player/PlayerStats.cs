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

    //LOOKING FOR HEALTHUI
    public PlayerHealthUI healthUI;
    //Look for the UI on start, and initialize values:
    public void Start()
    {
        healthUI = GameObject.FindGameObjectWithTag("PlayerHUI").GetComponent<PlayerHealthUI>();
        healthUI.SetMaxHealth(_maxHealth);
        healthUI.SetCurrentHealth(_health);

    }


    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage) {
        Debug.Log("Player took damage!");
        if(_health - damage <= 0)
        {
            _health = 0f;
            die();
        }
        _health -= damage;
        Debug.Log("Health: " + _health + "/" + _maxHealth);
        healthUI.SetCurrentHealth(_health);
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
        healthUI.SetCurrentHealth(_health);
    }

    public void die()
    {
        Debug.Log("PLAYER DIED");
        GameObject gameOverUI = GameObject.Find("GameOverExplosion");
        gameOverUI.GetComponent<GameOverUI>().ToggleGameOver();
    }

    /*METHODS NOT INCLUDED IN ENTITY STATS INTERFACE*/
   
}