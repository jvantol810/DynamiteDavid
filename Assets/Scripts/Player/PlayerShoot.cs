using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : ShootBase
{
    [Header("Player Properties")]
    public GameObject bulletPoolObject;
    public BulletPool bulletPool;
    public GameObject reticle;

    [Header("Internal State")]
    [SerializeField]
    private bool hasReticle = false;
    [SerializeField]
    private int toReticleAngle, flatAngle;
    // Start is called before the first frame update
    void Start()
    {
        onStart();
        InitializeBulletPool();
        FindReticle();
    }

    // Update is called once per frame
    void Update()
    {
        flatAngle = (int)transform.eulerAngles.z - 90;
        //toReticleAngle = (int)Vector3.SignedAngle(transform.position, reticle.transform.position, Vector3.up);
        Vector3 target = transform.position - reticle.transform.position;
        toReticleAngle = flatAngle + 90 - ((int)Vector3.SignedAngle(target, transform.up, Vector3.up) * -1);
        SetShootingEnabled(Input.GetMouseButton(0));
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
