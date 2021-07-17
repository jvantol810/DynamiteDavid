using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    [SerializeField]
    public BulletPool pool;

    public int startAngle = 0, endAngle = 360;

    public int angleStepMultiplierMin = 1;
    public int angleStepMultiplierMax = 1;
    public int numberOfLoops = 1;
    public float loopDelay;
    public float shotDelay;
    

    public BulletPattern(int startAngle, int endAngle, float shotDelay = 0, int numberOfLoops = 1, int angleStepMultiplierMin = 1, int angleStepMultiplierMax = 1)
    {
        this.startAngle = startAngle;
        this.endAngle = endAngle;
        this.numberOfLoops = numberOfLoops;
        this.angleStepMultiplierMin = angleStepMultiplierMin;
        this.angleStepMultiplierMax = angleStepMultiplierMax;
        this.shotDelay = shotDelay;
    }

    public virtual IEnumerator Fire()
    {
        while(numberOfLoops > 0)
        {
            int angleStep = endAngle / pool.poolSize;
            int angle = startAngle;
            for (int i = 0; i < pool.poolSize + 1; i++)
            {
                pool.angle = angle;
                pool.FireBulletFromPool(transform.position);
                yield return new WaitForSeconds(shotDelay);
                angle += angleStep * Random.Range(angleStepMultiplierMin, angleStepMultiplierMax);
            }
            numberOfLoops--;
        }
    }

}
