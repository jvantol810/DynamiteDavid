using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : BulletPattern
{
    public int numberOfRings = 1;
    public float ringDelay = 0.2f;
    public void Reset()
    {
        startAngle = 0;
        endAngle = 360;
        angleStepMultiplierMin = 1;
        angleStepMultiplierMax = 1;
        numberOfLoops = 1;
        shotDelay = 0f;
        numberOfRings = 2;
        ringDelay = 0.2f;
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(Fire());
            Debug.Log("Fire!");
        }
    }
    public CirclePattern(float shotDelay, int numberOfRings, float ringDelay, int startAngle = 0, int endAngle = 360) : base(startAngle, endAngle, shotDelay)
    {
        this.startAngle = startAngle;
        this.endAngle = endAngle;
        this.numberOfRings = numberOfRings;
        this.ringDelay = ringDelay;
    }

    public override IEnumerator Fire()
    {
        int rings = numberOfRings;
        int loops = numberOfLoops;
        int ringSize = (pool.poolSize / rings) / 2;
        int angleStep = endAngle / ringSize;
        int angle = startAngle;
        float originalPoolSpeed = pool.speed;
        
        while(loops > 0)
        {
            Debug.Log("LOOPING!");
            while (rings > 0)
            {
                for (int i = 0; i < ringSize + 1; i++)
                {
                    BulletBase firedBullet = pool.FireBulletFromPool(transform.position).GetComponent<BulletBase>();
                    
                    yield return new WaitForSeconds(shotDelay);
                    angle += angleStep;
                }
                //pool.SetBulletSpeed(originalPoolSpeed);
                yield return new WaitForSeconds(ringDelay);
                rings--;
            }
            loops--;
        }
        yield return null;
    }
}
