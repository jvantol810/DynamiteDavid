using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BoomBoxAI : MonoBehaviour, IEntityStats
{
    
    private GameObject player;//IDK if i need this yet
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;

    [Header("Sprite Stuff")]
    public SpriteRenderer sp;
    public Sprite angrySprite;
    public Sprite deathSprite;
    
    [Header("Firing Positions")]
    public GameObject leftSpeaker;
    public GameObject rightSpeaker;
    public GameObject tape;
    [Header("Bullet pools")]
    public BulletPool leftPool;
    public BulletPool rightPool;
    public BulletPool tapePool;
        
    public float speed = 2;
    public float _health;
    public float _maxHealth;

    private bool inSequence;
    private bool angryState = false;

    public void Start()
    {
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
    }

    public void Update()
    {
        //If under 50% health enrage
        if (health <= (maxHealth * .5f))
            angryState = true;
       
        AILoop();
    }
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    private void AILoop()
    {
        //If already doing a pattern don't do another
        if(inSequence)
            return;
        //If enraged fire from bigger set of patterns
        if(angryState)
            FirePatterns(true);
        
        //Fire from regular patterns
        FirePatterns(false);
    }

    private void FirePatterns(bool angry)
    {
        if (angry)
        {
            sp.sprite = angrySprite;
            leftPool.speed = 6;
            rightPool.speed = 6;
            tapePool.speed = 6;
        }

        StartCoroutine(IncrementSpray());

    }

    //Settings to fire 180 burst all at once single loop. .05 feels good if delayed desired though
    private void SetCircleSettings(CirclePattern settings,bool reverse, bool delay, int minStep, int maxStep, int ringsize, int start=90, int howFar=180)
    {
        
        settings.startAngle =start;
        settings.endAngle = howFar;
        
        if(reverse)
        {
            settings.startAngle = -start;
            settings.endAngle = -howFar;
        }
        
        settings.angleStepMultiplierMin = minStep;
        settings.angleStepMultiplierMax = maxStep;
        settings.numberOfLoops = 1;
        settings.loopDelay = 1.5f;
        settings.shotDelay = 0;
        if (delay)
        {
            settings.shotDelay = .1f;
        }
        settings.ringDelay = 0; 
        settings.numberOfRings = 1;
        settings.ringSize = ringsize;
    }

    private IEnumerator SpeakerRings()
    {
        inSequence = true;
        int loops = 0;
        while (loops < 5)
        {
            SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,15);
            SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(), true, true, 1, 1,15);
            leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
            rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
            loops++;
            yield return new WaitForSeconds(.8f);
        }
        SetCircleSettings(tape.GetComponent<CirclePattern>(),false,false,1,1,180);
        tape.GetComponent<CirclePattern>().FireFromObject();
        inSequence = false;
    }

    private IEnumerator IncrementSpray()
    {
        inSequence = true;
        
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,90,110);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,-90, -110);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(3.5f);
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,110,130);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,-110, -130);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(3.5f);
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,130,150);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,-130, -150);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(3.5f);
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,150,170);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,-150, -170);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
        yield return new WaitForSeconds(3.5f);
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,170,180);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(),false,true,1,1,30,-170, -180);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();
        
        inSequence = false;
    }

    private IEnumerator SentryBarrier()
    {
        // . . . . . 
        //o  . | x | . o
        return null;
    }

    public void takeDamage(float damage)
    {
        if(_health - damage <= 0)
        {
            die();
        }
        _health -= damage;
        BHealthUI.SetCurrentHealth(health);
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
        sp.sprite = deathSprite;
    }
    
    
}
