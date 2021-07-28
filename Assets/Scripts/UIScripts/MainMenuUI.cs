using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField]
    private GameObject screen1;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button levelSelectButton;
    [SerializeField]
    private GameObject screen2;
    [SerializeField]
    private Button level1Button;
    [SerializeField]
    private Button level2Button;
    [SerializeField]
    private Button level3Button;
    [SerializeField]
    private Button level4Button;
    [SerializeField]
    private Button backButton;
    // Start is called before the first frame update
    void Start()
    {
        screen1 = GameObject.Find("Screen1");
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        playButton.onClick.AddListener(() => PlayLevel(1));
        levelSelectButton = GameObject.Find("LevelSelectButton").GetComponent<Button>();
        levelSelectButton.onClick.AddListener(ToggleScreen2);
        screen2 = GameObject.Find("Screen2");
        level1Button = GameObject.Find("Level1").GetComponent<Button>();
        level1Button.onClick.AddListener(() => PlayLevel(1));
        level2Button = GameObject.Find("Level2").GetComponent<Button>();
        level2Button.onClick.AddListener(() => PlayLevel(2));
        level3Button = GameObject.Find("Level3").GetComponent<Button>();
        level3Button.onClick.AddListener(() => PlayLevel(3));
        level4Button = GameObject.Find("Level4").GetComponent<Button>();
        level4Button.onClick.AddListener(() => PlayLevel(4));
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(ToggleScreen1);
        screen2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayLevel(int levelID)
    {
        //Debug.Log("Loading Level:" + levelID);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelID);

    }
    private void ToggleScreen1()
    {
        
        screen2.SetActive(false);
        screen1.SetActive(true);
    }

    private void ToggleScreen2()
    {
        screen1.SetActive(false);
        screen2.SetActive(true);
    }
}
