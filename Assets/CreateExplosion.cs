using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosion : ScaleOverTime
{
    protected Sprite ExplosionSprite;
    public List<string> entityTagsAffected;

    private void Awake()
    {
        ExplosionSprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        base.Update();
        if (!this.scaling)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(entityTagsAffected.Contains(collision.gameObject.tag)){
            collision.gameObject.GetComponent<IEntityStats>().takeDamage(2);
        }
    }
}
