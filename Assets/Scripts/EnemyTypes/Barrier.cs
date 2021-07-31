using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Barrier : MonoBehaviour, IEntityStats
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Light2D light2D;

    /*ENTITY STATS INTERFACE DATA MEMBERS*/
    [SerializeField]
    public float _health;
    [SerializeField]
    public float _maxHealth;
    public float health { get { return _health; } set { _health = health; } }
    public float maxHealth { get { return _maxHealth; } set { _maxHealth = maxHealth; } }

    bool canDamage;
    float barrierSize = (1f + 1f / 3f);

    /*ENTITY STATS INTERFACE METHODS*/
    public void takeDamage(float damage)
    {
        if(canDamage)
        {
            if (_health - damage <= 0)
            {
                die();
            }
            _health -= damage;
        }
    }

    public void heal(float amount)
    {

    }

    public void die()
    {
        StartCoroutine(SinkBarrier());
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
    }

    void Awake()
    {
        StartCoroutine(RaiseBarrier());
    }

    void Update()
    {
        if(canDamage)
        {
            if(_health == 0)
            {
                fadeAway();
            }
        }
        else
        {
            if (transform.localScale.x != barrierSize)
            {
                increaseScale();
            }
        }
        
    }

    private void increaseScale()
    {
        transform.localScale = transform.localScale + new Vector3(3.2f, 3.2f, 0f) * Time.deltaTime;
        light2D.intensity += 2.4f * Time.deltaTime;
        if(transform.localScale.x > barrierSize)
        {
            transform.localScale = new Vector2(barrierSize, barrierSize);
        }
        if(light2D.intensity > 1f)
        {
            light2D.intensity = 1f;
        }
    }

    private void fadeAway()
    {
        transform.localScale = transform.localScale + new Vector3(.8f, .8f, .1f) * Time.deltaTime;
        sr.color -= new Color(0f, 0f, 0f, .1f) * Time.deltaTime;
        light2D.intensity -= 2f * Time.deltaTime;
        if (light2D.intensity < 0f)
        {
            light2D.intensity = 0f;
        }
    }

    private IEnumerator RaiseBarrier()
    {
        canDamage = false;
        transform.localScale = new Vector2(.1f, .1f);
        yield return new WaitForSeconds(0.4f);
        canDamage = true;
    }

    private IEnumerator SinkBarrier()
    {
        gameObject.tag = "Untagged";
        yield return new WaitForSeconds(0.4f);
        transform.localScale = new Vector2(.1f, .1f);
        _health = _maxHealth;
        gameObject.tag = "Enemy";
        gameObject.SetActive(false);
    }

    public void AwakeAgain()
    {
        StartCoroutine(RaiseBarrier());
    }
}
