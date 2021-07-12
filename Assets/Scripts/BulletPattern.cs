using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    [SerializeField]
    public BulletPool pool;

    private float startAngle = 90f, endAngle = 270f;
    private float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("FirePattern", 0f, 2f);
    }

    void FirePattern()
    {
        float angleStep = (endAngle - startAngle) / pool.poolSize;
        float angle = startAngle;

        for(int i = 0; i < pool.poolSize + 1; i++)
        {
            float xDir = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float yDir = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 move = new Vector2(xDir, yDir);
            Vector2 dir = (move - (Vector2)transform.position).normalized;
            GameObject bullet = pool.FireBulletFromPool(transform.position);
            bullet.GetComponent<BulletBase>().setAngle( (int)Vector2.Angle(dir, Vector2.right) );

            angle += angleStep;
        }
        


    }
}
