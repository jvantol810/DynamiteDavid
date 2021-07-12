using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public int poolSize;
    
    [SerializeField]
    private GameObject bulletPrefab;
    
    [Header("Bullet Settings")]
    public float duration;
    public float speed;
    public int angle;
    public Sprite sprite;
    public bool fireNow;

    private int bulletIndex = 0;
    private List<GameObject> pool;

    
    public BulletPool(int size, GameObject bulletPrefab, float speed, float duration, int angle, Sprite sprite, bool fireNow)
    {
        this.poolSize = size;
        this.bulletPrefab = bulletPrefab;
        this.speed = speed;
        this.duration = duration;
        this.angle = angle;
        this.sprite = sprite;
        this.fireNow = fireNow;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            FireBulletFromPool();
        }
    }
    //Instantiate pool of bullets and fill it before the first frame update
    private void Awake()
    {
        pool = new List<GameObject>();
        FillPool();
    }

    //Fill pool with bullets
    void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab);
            BulletBase bulletBase = newBullet.GetComponent<BulletBase>();
            bulletBase.InitializeInterface(speed, duration, angle, sprite, fireNow);
            pool.Add(newBullet);
            pool[i].SetActive(false);
        }
    }

    //Set the next bullet in the list to be active, teleport it to the firePoint (if any was passed in), and increment the bulletIndex
    public GameObject FireBulletFromPool(Vector2 firePoint = new Vector2())
    {
        GameObject firedBullet = pool[bulletIndex];
        if (firedBullet.activeSelf == false)
        {
            firedBullet.GetComponent<BulletBase>().ActivateBullet();
        }
        firedBullet.transform.position = firePoint;
        if (bulletIndex+1 >= pool.Count)
        {
            bulletIndex = 0;
        }
        else
        {
            bulletIndex++;
        }
        return firedBullet;
    }

}
