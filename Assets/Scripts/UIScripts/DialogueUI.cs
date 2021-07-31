using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueUI : MonoBehaviour
{
    [Header("Properties")]
    public DialogueObject[] dialogueLines;
    public GameObject boss;

    [Header("Internal State")]
    [SerializeField]
    int linesToRead;
    [SerializeField]
    int linesPointer = 0;
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private bool opening;
    [SerializeField]
    private bool closing;
    [Header("HP Based State")]
    [SerializeField]
    bool readingHP;
    [SerializeField]
    float health;
    [SerializeField]
    float healthTarget;
    
    private float UIspeed = 1f;
    Vector2 openedPosition = new Vector2(0f, 15f);
    Vector2 closedPosition = new Vector2(0f, -120f);

    public Text dialogueText;
    public Text dialogueSpeaker;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDialogue();
        //StartCoroutine(OnReadStateReached());
    }

    // Update is called once per frame
    void Update()
    {
        HandleUIAnimation();
    }

    public IEnumerator OpenUI()
    {
        while (closing)
        {
            yield return new WaitForSeconds(.1f);
            Debug.Log("Yielded for close");
        }
        opening = true;
        yield return new WaitForSeconds(UIspeed);
        isActive = true;
        opening = false;
    }

    public IEnumerator CloseUI()
    {
        while (opening)
        {
            yield return new WaitForSeconds(.1f);
            Debug.Log("Yielded for open");
        }
        opening = false;
        closing = true;
        yield return new WaitForSeconds(UIspeed);
        isActive = false;
        closing = false;
    }

    private void HandleUIAnimation()
    {
        if (opening)
        {
            Vector2 currentPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            //gameObject.GetComponent<RectTransform>().localPosition = new Vector2(0f, Mathf.Lerp(currentPos, 20f, Time.deltaTime));
            //transform.position = Vector2.Lerp(currentPos, openedPosition, Time.deltaTime);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentPos, openedPosition, Time.deltaTime * 6f);
        }
        else if (closing)
        {
            Vector2 currentPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            //gameObject.GetComponent<RectTransform>().localPosition = new Vector2(0f, Mathf.Lerp(-80f, 20f, Time.deltaTime));
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(currentPos, closedPosition, Time.deltaTime * 6f);
        }
    }

    private void InitializeDialogue()
    {
        linesToRead = dialogueLines.Length;
        linesPointer = 0;
        if(linesToRead > 0)
        {
            SetReadState();
        }
    }

    private void SetReadState()
    {
        if(dialogueLines[linesPointer].runByHP)
        {

        }
        //If you have any other ways to determine when to display dialogue, handle it here!
    }

    private IEnumerator OnReadStateReached()
    {
        if(linesPointer <= linesToRead)
        {
            yield return new WaitForSeconds(1f);
            dialogueSpeaker.text = dialogueLines[linesPointer].speaker;
            dialogueText.text = dialogueLines[linesPointer].line;
            if (!isActive)
            {
                StartCoroutine(OpenUI());
                yield return new WaitForSeconds(UIspeed);
            }
            yield return new WaitForSeconds(dialogueLines[linesPointer].duration);

            if (dialogueLines[linesPointer].lastLine)
            {
                linesPointer += 1;
                StartCoroutine(CloseUI());
            }
            else
            {
                linesPointer += 1;
                StartCoroutine(OnReadStateReached());
            }
        }
        else
        {
            StartCoroutine(CloseUI());
        }
        
    }

    public void StartDialogue()
    {
        StartCoroutine(OnReadStateReached());
    }

}
