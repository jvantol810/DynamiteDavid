using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : BulletBase
{

    public override void onUpdate()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            CleanUp();
        }
        if (fired)
        {
            if(timeLeft > duration / 2f)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(Vector3.up * -speed * Time.deltaTime, Space.Self);
            }
            
        }
    }
}
