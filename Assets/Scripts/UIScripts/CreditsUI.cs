using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class CreditsUI : MonoBehaviour
{
    //public GameObject title2;
    [SerializeField]
    private GameObject title;
    private bool fadeInTitle;
    private float titleAlpha = 0;
    [SerializeField]
    private bool scrollUp = false;
    private RectTransform rtransform;
    [SerializeField]
    private float topVal;
    private float scrollSpeed = 20f;
    private float scrollEnd = 1665f;
    private bool finishedCredits = false;


    public Tilemap EMPTiles;
    public Tilemap BOOMTiles;
    private float EMPscrolltrigger = 150f;
    private float EMPscrollender = 300f;
    private float BOOMscrolltrigger = 450f;
    private float BOOMscrollender = 600f;
    // Start is called before the first frame update
    void Start()
    {
        title = transform.Find("Title").gameObject;
        rtransform = GetComponent<RectTransform>();
        StartCoroutine(RunCredits());
    }

    // Update is called once per frame
    void Update()
    {
        if(!finishedCredits)
        {
            if (fadeInTitle && titleAlpha < 1f)
            {
                titleAlpha += Time.deltaTime / 2f;
                title.GetComponent<Text>().color = new Color(1f, 1f, 1f, titleAlpha);
            }

            if (Input.GetMouseButton(0))
            {
                scrollSpeed = 80f;
            }
            else
            {
                scrollSpeed = 20f;
            }

            if (scrollUp && topVal < scrollEnd)
            {
                topVal += Time.deltaTime * scrollSpeed;
                rtransform.anchoredPosition = new Vector2(0, topVal);
            }

            if(topVal > EMPscrolltrigger && topVal < EMPscrollender)
            {
                float ratio = (topVal - EMPscrolltrigger) / (EMPscrollender - EMPscrolltrigger);
                EMPTiles.color = new Color(1f, 1f, 1f, ratio);
            }

            if(topVal > BOOMscrolltrigger && topVal < BOOMscrollender)
            {
                float ratio = (topVal - BOOMscrolltrigger) / (BOOMscrollender - BOOMscrolltrigger);
                EMPTiles.color = new Color(1f, 1f, 1f, 1f - ratio);
                BOOMTiles.color = new Color(1f, 1f, 1f, ratio);
            }

            if(topVal > BOOMscrollender)
            {
                float ratio = (topVal - BOOMscrollender) / (150f);
                BOOMTiles.color = new Color(1f, 1f, 1f, 1f - ratio);
            }

            if (topVal >= scrollEnd)
            {
                StartCoroutine(ReturnToMenu());
            }
        }
        
    }

    private IEnumerator RunCredits()
    {
        yield return new WaitForSeconds(.2f);
        fadeInTitle = true;
        yield return new WaitForSeconds(2f);
        fadeInTitle = false;
        scrollUp = true;
    }

    private IEnumerator ReturnToMenu()
    {
        finishedCredits = true;
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
