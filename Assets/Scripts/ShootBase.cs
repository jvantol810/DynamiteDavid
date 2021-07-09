using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBase : MonoBehaviour
{
    //Player has limited ammo, must reload to refill, otherwise unlimited.
    //Temporary power up that changes how bullets fire and work (Player Exclusive)
    //Bullet Pools (If useful, include for efficiency)
    [Header("Shooter Properties")]
    public int maxAmmo;
    public float reloadTime;
    public float powerUpTime;
    public GameObject Bullet;


    [Header("Internal State")]
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private bool reloading;
    [SerializeField]
    private bool hasPowerUp;
    [SerializeField]
    private float reloadCooldown;
    [SerializeField]
    private float powerUpTimer;
    

    //private GameObject[] ammoPool;
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

    public virtual void onStart()
    {
        reloading = false;
        currentAmmo = maxAmmo;
    }

    public virtual void onUpdate()
    {
        if(reloading)
        {
            if(reloadCooldown > 0f)
            {
                reloadCooldown -= Time.deltaTime;
            }
            else
            {
                reloading = true;
            }
        }
        if(hasPowerUp)
        {
            //
        }
    }

    
}
