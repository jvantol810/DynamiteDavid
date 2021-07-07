using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour, IBullet
{
    /*
    [Header("Bullet Properties")]
    
    //Speed: how fast the bullet will travel
    //Duration: how long the bullet will last before clean-up
    public speed = 1f, duration = 10f;
    //Angle: the angle, in degrees, the bullet will be sent on fired.
    public int angle = 0;
    //FiredBy: A string that can be set so we know whether the player fired it, or the boss, or something else.
    public string firedBy = "unknown";
    //BulletSprite: The sprite that the bullet will look like
    public Sprite bulletSprite;
    //FireImmediately: if true, the bullet will be fired when created, if false then it will do nothing until fired manually.
    //CanPierce: if true, clean-up will not occur on hit, if false clean-up will occur on the first hit.
    public bool fireImmediately, canPierce;
    [Header("Internal State")]
    //Fired: set to false initially, and true when bullet is fired.
    private bool fired;
    //TimeLeft: Will be set equal to duration on start, and will begin to decrease when the bullet is fired.
    private float timeLeft;
    */

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
    [SerializeField]
    private bool fired;
    [SerializeField]
    private float timeLeft;
    //FiredBy: A string that can be set so we know whether the player fired it, or the boss, or something else.
    [SerializeField]
    private string firedBy;
    //Fired: set to false initially, and true when bullet is fired.
    //private bool _fired { get; set; }
    //public bool fired { get { return _fired; } }
    //TimeLeft: Will be set equal to duration on start, and will begin to decrease when the bullet is fired.
    //private float _timeLeft { get; set; }
    //public float timeLeft { get { return _timeLeft;  } }




    // Start is called before the first frame update
    void Start()
    {
        //test interface
        //InitializeInterface(1f, 35f, 0);
        /*speed = 5;
        Debug.Log(_speed);
        Debug.Log(speed);
        _speed = 27;
        Debug.Log(_speed);
        Debug.Log(speed);
        */
        //Initialize Internal State
        timeLeft = duration;
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

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            CleanUp();
        }
        if (fired)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(fired)
        {
            //Detect if player, enemy, or something else
            CleanUp();
        }
    }

    public void InitializeInterface(float spd, float dur, int ang, Sprite bsprite, bool fireNow)
    {
        _speed = spd;
        _duration = dur;
        _angle = ang;
        _bulletSprite = bsprite;
        _fireImmediately = fireNow;
    }
    public void InitializeInterface(float spd, float dur, int ang)
    {
        _speed = spd;
        _duration = dur;
        _angle = ang;
    }

    public void Fire()
    {
        fired = true;
    }

    public void CleanUp()
    {
        Destroy(gameObject);
        //If we maintain bullets in a list, replace this with setting the bullet inactive, and anything else you need.
    }

    
}
