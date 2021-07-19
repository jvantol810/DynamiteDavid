using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : ShootBase
{
    //To get player position
    private GameObject player;
    [Header("Other")]
    //To get current position
    public GameObject parent;
    [Header("Enemy Properties")]
    public GameObject bulletPoolObject;
    public BulletPool bulletPool;
    public GameObject reticle;

    [Header("Internal State")]
    [SerializeField]
    private bool hasReticle = false;
    [SerializeField]
    private int toReticleAngle, flatAngle;
    [SerializeField]
    //If true the object will point toward player when shooting
    private bool rotates = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        onStart();
        InitializeBulletPool();
        FindReticle();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPos = transform.position;
        Vector2 playerPos = player.transform.position;
        toReticleAngle = (int)(Mathf.Atan2(myPos.y - playerPos.y, myPos.x - playerPos.x) * Mathf.Rad2Deg)+90;
        
        if(rotates)
            parent.transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,toReticleAngle)); 
        
        SetShootingEnabled(true);
        if(hasReticle)
        {
            bulletPool.angle = toReticleAngle;
        }
        else
        {
            bulletPool.angle = flatAngle;
            FindReticle();
        }
        onUpdate();
    }
    

    private void InitializeBulletPool()
    {
        bulletPool.poolSize = maxAmmo;
        bulletPoolObject.SetActive(true);
    }

    public override void OnCanShoot()
    {
        bulletPool.FireBulletFromPool(transform.position);
    }

    private void FindReticle()
    {
        reticle = GameObject.FindWithTag("Reticle");
        if (reticle != null)
        {
            hasReticle = true;
        }
    }
}
