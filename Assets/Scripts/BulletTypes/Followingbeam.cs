using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followingbeam : BulletBase
{
    [SerializeField]
    private float endFadeInAt;
    [SerializeField]
    private float beginFadeOutAt;
    [SerializeField]
    private float timePast;
    [SerializeField]
    private bool canDealDamage;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Quaternion rotateTo;
    //[SerializeField]
    //private string FadeState;

    [Header("Percentage Variables")]
    public float fadeInEndTime;
    public float fadeOutStartTime;


    private SpriteRenderer sr;

    public override void onStart()
    {
        //Sbeam Initialization:
        canDealDamage = false;
        sr = spriteObject.GetComponent<SpriteRenderer>();
        endFadeInAt = duration * (fadeInEndTime / 100f);
        beginFadeOutAt = duration * (fadeOutStartTime / 100f);

        //find player
        player = GameObject.FindGameObjectWithTag("Player");

        //Load Sprite
        sr.sprite = bulletSprite;
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

    public override void onUpdate()
    {
        if (timeLeft != 0)
        {
            if (timeLeft > 0f)
            {
                timeLeft -= Time.deltaTime;
                timePast += Time.deltaTime;

            }
            else if (timeLeft < 0f)
            {
                timeLeft = 0f;
                timePast = 0f;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
                timePast = 0f;

            }
            UpdateBeam();
        }
        rotateTo = Quaternion.LookRotation(Vector3.forward, -(player.transform.position - transform.position));
        transform.rotation = new Quaternion(0f, 0f, Mathf.Lerp(transform.rotation.z, rotateTo.z, Time.deltaTime * 1.5f), transform.rotation.w);
    }

    public override void onTriggerEnter2D(Collider2D collidedWith)
    {

        //if (fired && collidedWith.gameObject.tag == "Player" && canDealDamage)
        //{
        //Deal damage only!
        //Debug.Log("Hit!");
        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (fired && collision.gameObject.tag == "Player" && canDealDamage)
        {
            //Deal damage only!
            collision.gameObject.GetComponent<PlayerStats>().takeDamage(30 * Time.fixedDeltaTime);
        }
    }

    private void UpdateBeam()
    {
        if (timePast < endFadeInAt)
        {
            sr.color = new Color(.8f, .8f, .8f, timePast / (endFadeInAt * 1.40f));
            transform.localScale = new Vector2(timePast / (endFadeInAt), transform.localScale.y);
            //FadeState = "In";
            canDealDamage = false;
        }
        else if (timePast > beginFadeOutAt)
        {
            float ratio = timeLeft / (duration - beginFadeOutAt);
            sr.color = new Color(1f, 1f, 1f, ratio);
            //FadeState = ratio.ToString();
            canDealDamage = false;
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            //FadeState = "Full";
            canDealDamage = true;
            transform.localScale = new Vector2(1f, transform.localScale.y);
        }

    }

    public override void Fire()
    {
        timePast = 0f;
        timeLeft = duration;
        fired = true;
    }
}
