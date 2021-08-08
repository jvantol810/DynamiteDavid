using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BoomBoxAI : MonoBehaviour, IEntityStats
{
    
    public DialogueUI bossDialogue;
    public BossHealthUI BHealthUI;

    [Header("Sprite Stuff")]
    public SpriteRenderer sp;
    public Sprite angrySprite;
    public Sprite deathSprite;
    
    [Header("Positions")]
    public GameObject leftSpeaker;
    public GameObject rightSpeaker;
    public GameObject tape;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
    [Header("Bullet pools")]
    public BulletPool leftPool;
    public BulletPool rightPool;
    public BulletPool tapePool;
    [Header("Stats")]    
    public float speed = 2;
    public float _health;
    public float _maxHealth;
    
    private int num;
    private bool inSequence;
    private bool angryState = false;
    private bool spoken = false;
    private bool dead = false;

    public void Start()
    {
        BHealthUI.SetMaxHealth(health);
        BHealthUI.SetCurrentHealth(health);
    }

    public void Update()
    {
        //If under 50% health enrage
        if (health <= (maxHealth * .5f))
        {
            angryState = true;
            sp.sprite = angrySprite;
            leftPool.speed = 6;
            rightPool.speed = 6;
            tapePool.speed = 6;
            if (spoken == false)
            {
                bossDialogue.StartDialogue();
                spoken = true;
            }
        }

        AILoop();
    }
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    private void AILoop()
    {
        //If already doing a pattern don't do another
        if(inSequence)
            return;
        
        //Fire from regular patterns
        FirePatterns();
    }

    private void FirePatterns()
    {
    //    if (angryState)
    //    {
            //Special patterns if time
    //    }
        
        //Choose a pattern at random
        num = Random.Range(1,4);
        switch (num)
        {
            case 1:
                StartCoroutine(SpeakerRings());
                break;
            
            case 2:
                StartCoroutine(IncrementSpray());
                break;
            
            case 3:
                StartCoroutine(SentryBarrier());
                break;
        }

    }

    
    private void SetCircleSettings(CirclePattern settings,bool reverse, bool delay, int minStep, int maxStep, int ringsize, int start=90, int howFar=180, float shotDelay=.1f)
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
            settings.shotDelay = shotDelay;
        }
        settings.ringDelay = 0; 
        settings.numberOfRings = 1;
        settings.ringSize = ringsize;
    }

    private IEnumerator SpeakerRings()
    {
        if(inSequence){yield break;}
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
        if(inSequence){yield break;}
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
        if(inSequence){yield break;}
        inSequence = true;
        
        SetCircleSettings(leftSpeaker.GetComponent<CirclePattern>(),false,true,1,1,90,180,0, .3f);
        SetCircleSettings(rightSpeaker.GetComponent<CirclePattern>(), false, true, 1, 1,90, 180,0, .3f);
        leftSpeaker.GetComponent<CirclePattern>().FireFromObject();
        rightSpeaker.GetComponent<CirclePattern>().FireFromObject();

        leftSpawn.GetComponent<SpawnPrefab>().Spawn();
        rightSpawn.GetComponent<SpawnPrefab>().Spawn();
        yield return new WaitForSeconds(15f);

        inSequence = false;
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
        if (dead) return;
        StopAllCoroutines();
        sp.sprite = deathSprite;
        GameObject exit = GameObject.Find("Exit");
        exit.GetComponent<exitManager>().openExit();
        this.enabled = false;
        dead = true;
    }
    
    
}
