using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour
{
    public Image fadePanel;

    public void Level()
    {
        Time.timeScale = 1f; 
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("Level");
        });
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            fadePanel.gameObject.SetActive(true);
            fadePanel.DOFade(1f, 1f).OnComplete(() =>
            {
                SceneManager.LoadScene(nextSceneIndex);
            });
        }
    }
    }
