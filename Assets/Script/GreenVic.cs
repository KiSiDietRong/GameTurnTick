using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenVic : MonoBehaviour
{
    public GameObject victoryCanvas;
    public BellControl bellControl;
    public Gameplay_Controller gameplayController;
    public TimeControl countdownTimer;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Green")) return; // Chỉ xử lý nếu va chạm là kim xanh

        // Trường hợp không có chuông, hoặc có chuông nhưng chuông đã vàng
        if (bellControl == null || (bellControl != null && bellControl.IsYellowActive()))
        {
            Time.timeScale = 0f;

            if (victoryCanvas != null)
                victoryCanvas.SetActive(true);

            if (gameplayController != null)
                gameplayController.SetVictory();

            if (countdownTimer != null)
            {
                countdownTimer.StopTimer();
            }

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
