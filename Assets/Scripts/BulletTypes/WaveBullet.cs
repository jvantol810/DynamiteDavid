using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBullet : BulletBase
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
            transform.localScale += new Vector3(1f, 1f, 0f) * Time.deltaTime * speed;
        }
    }

    public override void onStart()
    {
        
        //Check for debug mode
        if (!testingEnabled) //Disable debug sprites if not testing.
        {
            directionArrow.SetActive(false);
        }
        //Initialize Internal State
        if (duration != 0)
        {
            timeLeft = duration;
        }

        transform.Rotate(new Vector3(0f, 0f, angle));
        if (fireImmediately)
        {
            Fire();
        }
        else
        {
            fired = false;
        }
    }

    public override void onTriggerEnter2D(Collider2D collidedWith)
    {
        if (fired)
        {
            if (tagsAffected.Contains(collidedWith.gameObject.tag))
            {
                //Detect if player, enemy, or something else
                DealDamage(collidedWith.gameObject, damage);
                CleanUp();
            }

        }
    }

    public override void CleanUp()
    {

        gameObject.SetActive(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        //If we maintain bullets in a list, replace this with setting the bullet inactive, and anything else you need.
    }
}
