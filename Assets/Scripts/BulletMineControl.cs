using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMineControl : MineController
{
    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = timeTillExplode;
        mineAnim = GetComponent<Animator>();
        mineAnim.SetBool("active", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            mineAnim.SetBool("active", true);
            if (explosionTimer > 0)
            {
                explosionTimer -= Time.deltaTime;
            }
            else
            {
                Explode();
            }
        }
    }

    public override void Explode()
    {
        GetComponentInChildren<CirclePattern>().FireFromObject();
        DestroyMine();
    }
}
