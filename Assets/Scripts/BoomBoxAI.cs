using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BoomBoxAI : MonoBehaviour, IEntityStats
{
    
    private GameObject player;//IDK if i need this yet

    public GameObject leftSpeaker;
    public GameObject rightSpeaker;
        
    public float speed = 2;
    public float _health;
    public float _maxHealth;

    public bool inSequence;

    public void Start()
    {
       HalfCircle(rightSpeaker.GetComponent<CirclePattern>(), false);
       rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
       
       HalfCircle(rightSpeaker.GetComponent<CirclePattern>(), true);
       rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
    }
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    public void AILoop()
    {
    }

    //Settings to fire 180 burst all at once single loop. .05 feels good if delayed desired though
    public void HalfCircle(CirclePattern settings, bool delay)
    {
        settings.startAngle = 90;
        settings.endAngle = 180;
        settings.angleStepMultiplierMin = 1;
        settings.angleStepMultiplierMax = 2;
        settings.numberOfLoops = 1;
        settings.loopDelay = 1.5f;
        if (delay)
            settings.ringDelay = .05f;
        settings.shotDelay = 0; 
        settings.numberOfRings = 1;
        settings.ringSize = 15;
    }
    public void takeDamage(float damage)
    {
        if(_health - damage <= 0)
        {
            die();
        }
        _health -= damage;
    }

    public void heal(float amount)
    {
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
    }
    
    
}
