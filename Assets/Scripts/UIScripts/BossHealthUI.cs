using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [Header("Public Variables")]
    public float lerpSpeed = 2f;
    //fuseMask: Resized by scale, the fuse is the bar for the health.
    [Header("UI Children")]
    [SerializeField]
    GameObject bar;
    [SerializeField]
    GameObject label;

    //playerHealth: The last known current health of the player, expected to be updated when player takes damage.
    //playerMaxHealth: The last known max health of the player, used to calculate the percentage of health.
    [Header("Internal State")]
    [SerializeField]
    float difference;
    [SerializeField]
    float currentBar;
    [SerializeField]
    float bossHealth;
    [SerializeField]
    float bossMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("BossBar").gameObject;
        label = transform.Find("Label").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        difference = currentBar - bossHealth;
        currentBar = Mathf.Lerp(currentBar, bossHealth, Time.deltaTime * lerpSpeed * 3);
        if (difference > 0.001f || difference < -0.001f)
        {
            Vector2 current = bar.GetComponent<RectTransform>().sizeDelta;
            Vector2 newSize = new Vector2(128f * (bossHealth / bossMaxHealth), 8f);
            bar.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(current, newSize, Time.deltaTime * lerpSpeed);
        }
    }

    public void SetMaxHealth(float newMax)
    {
        bossMaxHealth = newMax;
    }

    public void SetCurrentHealth(float health)
    {
        bossHealth = health;
    }

    public void SetLabel(string labelTo)
    {
        label.GetComponent<Text>().text = labelTo;
    }
}
