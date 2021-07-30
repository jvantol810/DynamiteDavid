using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public float timeTillExplode;
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public GameObject explosionPrefab;
    public List<string> entityTagsAffected;
    [HideInInspector]
    public float explosionTimer;
    public bool active = false;
    protected Animator mineAnim;

    // Start is called before the first frame update

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
    }
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

    public void ActivateMine()
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

    public virtual void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        DestroyMine();
    }

    public void DestroyMine()
    {
        Destroy(gameObject);
    }
}
