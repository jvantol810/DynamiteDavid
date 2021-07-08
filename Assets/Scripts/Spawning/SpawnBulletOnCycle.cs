using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletOnCycle : SpawnObjectOnCycle
{
    [Header("Bullet Settings")]
    public float duration;
    public float speed;
    public int angle;
    public Sprite sprite;
    public bool fireNow;

    private BulletBase bulletBase;
    public override GameObject Spawn()
    {
        GameObject spawnedBullet = base.Spawn();
        spawnedBullet.GetComponent<BulletBase>().InitializeInterface(speed, duration, angle, sprite, fireNow);
        spawnedBullet.transform.position = transform.position;
        return spawnedBullet;
    }
}
