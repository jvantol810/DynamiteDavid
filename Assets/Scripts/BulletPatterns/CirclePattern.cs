using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePattern : BulletPattern
{
    public int numberOfRings = 1;
    public float ringDelay = 0.2f;
    public int ringSize;
    public void Reset()
    {
        startAngle = 0;
        endAngle = 360;
        angleStepMultiplierMin = 1;
        angleStepMultiplierMax = 1;
        numberOfLoops = 1;
        loopDelay = 0;
        shotDelay = 0f;
        numberOfRings = 2;
        ringDelay = 0.2f;
    }

    public void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            StartCoroutine(Fire());
        }

    }
    public CirclePattern(int bulletsPerRing, int numberOfRings, float ringDelay, int startAngle = 0, int endAngle = 360) : base(startAngle, endAngle, bulletsPerRing)
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
        int ringSize = this.ringSize;
        int angleStep = endAngle / ringSize;
        int angle = startAngle;
        while(loops > 0)
        {
            angle = startAngle;
            rings = numberOfRings;
            while (rings > 0)
            {
                for (int i = 0; i < ringSize; i++)
                {
                    pool.FireBulletFromPool(firePoint.position, angle);
                    if(shotDelay > 0) { yield return new WaitForSeconds(shotDelay); }
                    angle += angleStep * Random.Range(angleStepMultiplierMin, angleStepMultiplierMax); 
                }
                yield return new WaitForSeconds(ringDelay);
                rings--;
            }
            yield return new WaitForSeconds(loopDelay);
            loops--;
        }
        yield return null;
    }

    public void FireFromObject()
    {
        StartCoroutine(Fire());
    }

}
   

