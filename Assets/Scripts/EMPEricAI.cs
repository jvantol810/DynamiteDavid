using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EMPEricAI : MonoBehaviour, IEntityStats
{

    public BossHealthUI bossHealthUI;
    public GameObject player;
    public CirclePattern circlePattern;
    public CirclePattern circlePattern2;

    [Header("Stats")]
    public float speed;
    public int stageDuration;
    public int lightsOutDuration;
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
    public Light2D mainLight;

    private bool midAction = false;
    private bool pulseOnCooldown = false;
    private int nextMove = 0;

    // Start is called before the first frame update
    void Start()
    {
        bossHealthUI.SetMaxHealth(health);
        bossHealthUI.SetCurrentHealth(health);

        player = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer.sprite = spStage1;
        StartCoroutine(pulseWave());
    }

    // Update is called once per frame
    void Update()
    {
        if(!midAction) transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    // public void controlAI() 
    // {
    //     StartCoroutine(pulseWave());
    //     transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    // }

    public IEnumerator pulseWave() {
        while (true) {
            midAction = true;

            StopCoroutine(fireBullets());

            spriteRenderer.sprite = spStage2;
            foreach(Light2D light in stageLights) {
                light.color = Color.yellow;
            }
            yield return new WaitForSeconds(stageDuration);

            spriteRenderer.sprite = spStage3;
            foreach(Light2D light in stageLights) {
                light.color = Color.green;
            }
            yield return new WaitForSeconds(stageDuration);

            StartCoroutine(lightsOut());

            //bullet burst in all directions
            circlePattern.FireFromObject();

            spriteRenderer.sprite = spStage1;
            foreach(Light2D light in stageLights) {
                light.color = Color.red;
            }

            midAction = false;
            StartCoroutine(fireBullets());
            yield return new WaitForSeconds(lightsOutDuration);
        }
    }

    public IEnumerator lightsOut() {
        mainLight.enabled = false;
        yield return new WaitForSeconds(lightsOutDuration);
        pulseOnCooldown = false;
        mainLight.enabled = true;
        yield return null;
    }

    public IEnumerator fireBullets() {
        while (true) {
            circlePattern2.FireFromObject();
            yield return new WaitForSeconds(1);
        }
    }

    public void takeDamage(float amt) 
    {
        if (_health - amt <= 0) {
            die();
        }
        _health -= amt;
        bossHealthUI.SetCurrentHealth(health);
    }

    public void heal(float amt) 
    {
        if(_health + amt > _maxHealth)
            {
                _health = _maxHealth;
            }
            else
            {
                _health += amt;
            }
    }

    public void die() 
    {
        StopAllCoroutines();

        //open exit
        GameObject exit = GameObject.Find("Exit");
        exit.GetComponent<exitManager>().openExit();

        Destroy(gameObject);
    }
}
