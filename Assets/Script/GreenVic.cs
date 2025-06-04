using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenVic : MonoBehaviour
{
    public GameObject victoryCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Green")) // Đặt tag cho kim xanh
        {
            Time.timeScale = 0f;

            if (victoryCanvas != null)
                victoryCanvas.SetActive(true);

            UnlockedNewLevel();
        }
    }
    void UnlockedNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
