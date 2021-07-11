using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBase : MonoBehaviour
{
    //Player has limited ammo, must reload to refill, otherwise unlimited.
    //Temporary power up that changes how bullets fire and work (Player Exclusive)
    //Bullet Pools (If useful, include for efficiency)
    [Header("Shooter Properties")]
    //MaxAmmo: the amount of bullets that will be fired before reloading.
    public int maxAmmo;
    //ReloadTime: the amount of time it takes to finish reloading.
    public float reloadTime;
    //GapTime: the amount of time between successive shots.
    public float gapTime;
    //PowerUpTime: the amount of time a power up lasts.
    public float powerUpTime;
    //Bullet: the type of bullet being shot.
    public GameObject Bullet;


    [Header("Internal State")]
    //CurrentAmmo: the amount of shots left before reload.
    [SerializeField]
    private int currentAmmo;
    //ShootingEnabled: if true, will attempt to fire shots and handle relevant cooldowns.
    [SerializeField]
    private bool shootingEnabled;
    //Reloading: while true, cannot fire. Set to true when currentAmmo reaches 0.
    [SerializeField]
    private bool reloading;
    //Shot: while true, cannot fire. Set to true after firing.
    [SerializeField]
    private bool shot;
    //HasPowerUp: while true, player is currently expected to be using a power up.
    [SerializeField]
    private bool hasPowerUp;
    /*
    [SerializeField]
    private float reloadCooldown;
    [SerializeField]
    private float shotCooldown;
    [SerializeField]
    private float powerUpTimer;
    */

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
        //HandleCooldowns();
    }

    public virtual void onStart()
    {
        reloading = false;
        shot = false;
        currentAmmo = maxAmmo;
    }

    public virtual void onUpdate()
    {
        if(shootingEnabled)
        {
            if(!shot && !reloading)
            {
                //Fire Bullet, need to incorporate bullet pools and etc.
                currentAmmo -= 1;
                OnCanShoot();
                if (currentAmmo <= 0f)
                {
                    //reloadCooldown = reloadTime;
                    //reloading = true;
                    StartCoroutine(PerformReloadCooldown());
                    OnReloadEvent();
                }
                //shotCooldown = gapTime;
                //shot = true;
                StartCoroutine(PerformShotCooldown());
                OnShotEvent();
            }
            
        }
    }

    /*
    private void HandleCooldowns()
    {
        if (reloading)
        {
            if (reloadCooldown > 0f)
            {
                reloadCooldown -= Time.deltaTime;
            }
            else
            {
                reloading = false;
            }
        }
        if (shot)
        {
            if (shotCooldown > 0f)
            {
                shotCooldown -= Time.deltaTime;
            }
            else
            {
                shot = false;
            }
        }
        if (hasPowerUp)
        {
            if (powerUpTimer > 0f)
            {
                powerUpTimer -= Time.deltaTime;
            }
            else
            {
                hasPowerUp = false;
            }
        }
    }
    */

    public virtual void OnReloadEvent()
    {

    }
    public virtual void OnShotEvent()
    {

    }

    public virtual void OnCanShoot()
    {

    }
    private IEnumerator PerformShotCooldown()
    {
        shot = true;
        yield return new WaitForSeconds(gapTime);
        shot = false;
    }

    private IEnumerator PerformReloadCooldown()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;
    }

    private IEnumerator PerformPowerUpTime()
    {
        hasPowerUp = true;
        yield return new WaitForSeconds(powerUpTime);
        hasPowerUp = false;
    }

    public void SetShootingEnabled(bool var)
    {
        shootingEnabled = var;
    }
    
}
