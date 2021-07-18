using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthUI : MonoBehaviour
{
    [Header("Public Variables")]
    public float lerpSpeed = 2f;
    //fuseMask: Resized by scale, the fuse is the bar for the health.
    [Header("UI Children")]
    [SerializeField]
    GameObject fuseMask;
    [SerializeField]
    GameObject fuseFlame;

    //playerHealth: The last known current health of the player, expected to be updated when player takes damage.
    //playerMaxHealth: The last known max health of the player, used to calculate the percentage of health.
    [Header("Internal State")]
    [SerializeField]
    float difference;
    [SerializeField]
    float currentBar;
    [SerializeField]
    float playerHealth;
    [SerializeField]
    float playerMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        fuseMask = transform.Find("FuseMask").gameObject;
        fuseFlame = fuseMask.transform.Find("Flame").gameObject;
        fuseFlame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        difference = currentBar - playerHealth;
        currentBar = Mathf.Lerp(currentBar, playerHealth, Time.deltaTime * lerpSpeed * 3);
        if (difference > 0.001f || difference < -0.001f)
        {
            Vector2 current = fuseMask.GetComponent<RectTransform>().sizeDelta;
            Vector2 newSize = new Vector2(128f * (playerHealth / playerMaxHealth), 8f);
            fuseMask.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(current, newSize, Time.deltaTime * lerpSpeed);
            if(difference > 0f)
            {
                fuseFlame.SetActive(true);
            }
        }
        else
        {
            if(fuseFlame.activeInHierarchy)
            {
                fuseFlame.SetActive(false);
            }
        }
    }

    public void SetMaxHealth(float newMax)
    {
        playerMaxHealth = newMax;
        UpdateMask();
    }

    public void SetCurrentHealth(float health)
    {
        playerHealth = health;
        UpdateMask();
    }

    private void UpdateMask()
    {
        //fuseMask.GetComponent<RectTransform>().localScale = new Vector2(playerHealth / playerMaxHealth, 1f);
        //fuseMask.GetComponent<RectTransform>().sizeDelta = new Vector2(128f * (playerHealth / playerMaxHealth), 8f);
    }


}
