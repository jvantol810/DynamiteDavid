using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float timeTillExplode;
    public Sprite activeSprite;
    public GameObject explosionPrefab;
    public List<string> entityTagsAffected;
    private float explosionTimer;
    private bool active = false;
    private Animator mineAnim;

    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = timeTillExplode;
        mineAnim = GetComponent<Animator>();
        mineAnim.SetBool("active", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entityTagsAffected.Contains(collision.tag) && !active)
        {
            ActivateMine();
        }
    }

    private void ActivateMine()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = activeSprite;
        active = true;
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

    public void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        DestroyMine();
    }

    public void DestroyMine()
    {
        Destroy(gameObject);
    }
}
