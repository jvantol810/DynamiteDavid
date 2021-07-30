using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class CreateExplosion : MonoBehaviour
{
    public GameObject explosionObject;
    public Light2D light;
    public Vector2 scaleVector = new Vector2(1, 1);
    public Vector2 finalSize = new Vector2(2, 2);
    public List<string> entityTagsAffected;
    public bool exploding = true;
    public float damage;

    private void Start()
    {

    }
    private void Update()
    {
        if (exploding)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(entityTagsAffected.Contains(collision.gameObject.tag)){
            collision.gameObject.GetComponent<IEntityStats>().takeDamage(damage);
        }
    }

    public void Explode()
    {
        if (explosionObject.transform.localScale.x < finalSize.x)
        {
            explosionObject.transform.localScale += new Vector3(scaleVector.x * Time.deltaTime, 0, 0);
        }
        if (explosionObject.transform.localScale.y < finalSize.y)
        {
            explosionObject.transform.localScale += new Vector3(0, scaleVector.y * Time.deltaTime, 0);
        }
        light.pointLightOuterRadius = explosionObject.transform.localScale.x + scaleVector.x;
        if (explosionObject.transform.localScale.x >= finalSize.x && explosionObject.transform.localScale.y >= finalSize.y)
        {
            EndExplosion();
        }
    }
    public virtual void EndExplosion()
    {
        exploding = false;
        gameObject.SetActive(false);
    }


}
