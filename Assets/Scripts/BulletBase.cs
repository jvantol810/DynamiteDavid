using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IBullet
{
    public List<string> tagsAffected = new List<string>();
    [Header("Debug Mode")]
    public bool testingEnabled;
    [Header("Children Variables")]
    public GameObject directionArrow;
    public GameObject spriteObject;
    //Speed: how fast the bullet will travel
    [Header("IBullet Properties")]
    [SerializeField]
    float _speed;
    public float speed { get { return _speed;  } set { _speed = speed; } }
    //Duration: how long the bullet will last before clean-up
    [SerializeField]
    private float _duration;
    public float duration { get { return _duration; } set { _duration = duration; } }
    //Angle: the angle, in degrees, the bullet will be sent on fired.
    [SerializeField]
    private int _angle;
    public int angle { get { return _angle; } set { _angle = angle; } }
    //BulletSprite: The sprite that the bullet will look like
    [SerializeField]
    private Sprite _bulletSprite;
    public Sprite bulletSprite { get { return _bulletSprite; } set { _bulletSprite = bulletSprite; } }
    //FireImmediately: if true, the bullet will be fired when created, if false then it will do nothing until fired manually.
    [SerializeField]
    private bool _fireImmediately;
    public bool fireImmediately { get { return _fireImmediately; } set { _fireImmediately = fireImmediately; } }

    [Header("Internal State")]
    //Fired: set to false initially, and true when bullet is fired.
    [SerializeField]
    protected bool fired;
    //TimeLeft: Will be set equal to duration on start, and will begin to decrease when the bullet is fired.
    [SerializeField]
    protected float timeLeft;
    //FiredBy: A string that can be set so we know whether the player fired it, or the boss, or something else.
    [SerializeField]
    protected string firedBy;
    //NoDuration: is set to true on start if the duration value is null
    public bool hasDuration;

    // Start is called before the first frame update
    void Start()
    {
        onStart();
    }

    // Update is called once per frame
    void Update()
    {
        onUpdate();
    }

    public void setAngle(int angle)
    {
        _angle = angle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter2D(collision);
    }

    public void InitializeInterface(float spd, float dur, int ang, Sprite bsprite, bool fireNow)
    {
        _speed = spd;
        _duration = dur;
        _angle = ang;
        _bulletSprite = bsprite;
        _fireImmediately = fireNow;
    }
    public void InitializeInterface(float spd, int ang, Sprite bsprite, bool fireNow)
    {
        _speed = spd;
        _angle = ang;
        _bulletSprite = bsprite;
        _fireImmediately = fireNow;
        _duration = 0;
    }
    public void InitializeInterface(float spd, float dur, int ang)
    {
        _speed = spd;
        _duration = dur;
        _angle = ang;
    }

    public virtual void Fire()
    {
        fired = true;
    }

    public virtual void CleanUp()
    {

       gameObject.SetActive(false);
        
        //If we maintain bullets in a list, replace this with setting the bullet inactive, and anything else you need.
    }
    //Sets the bullet to active and resets its timeLeft
    public virtual void ActivateBullet()
    {
        gameObject.SetActive(true);
        timeLeft = duration;
    }
    public virtual void onUpdate()
    {
        if (_duration != 0)
        {
            if (timeLeft > 0f)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                CleanUp();
            }
        }
        if (fired)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        }
    }

    public virtual void onStart()
    {
        //Load Sprite
        spriteObject.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        //Check for debug mode
        if (!testingEnabled) //Disable debug sprites if not testing.
        {
            directionArrow.SetActive(false);
        }
        //Initialize Internal State
        if (duration != 0)
        {
            timeLeft = duration;
        }

        transform.Rotate(new Vector3(0f, 0f, angle));
        if (fireImmediately)
        {
            Fire();
        }
        else
        {
            fired = false;
        }
    }

    public virtual void onTriggerEnter2D(Collider2D collidedWith)
    {
        if (fired)
        {
            if (tagsAffected.Contains(collidedWith.tag))
            {
                //Detect if player, enemy, or something else
                CleanUp();
            }
            
        }
    }
    
}
