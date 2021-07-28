using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Button RetryButton;
    public Button MenuButton;
    private Scene currentScene;
    private bool gameOverActive;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        RetryButton.onClick.AddListener(() => LoadLevel(currentScene.buildIndex));
        MenuButton.onClick.AddListener(() => LoadLevel(0));
        RetryButton.gameObject.SetActive(false);
        MenuButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOverActive)
        {
            GetComponent<RectTransform>().sizeDelta += new Vector2(1f, 1f) * Time.deltaTime * 800f;
        }
    }

    public void ToggleGameOver()
    {
        gameOverActive = true;
        RetryButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
    }

    private void LoadLevel(int levelID)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelID);
    }
}
