using UnityEngine;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour
{
    public float timeLeft = 10f; // thời gian ban đầu (giây)
    public Text timerText;       // text hiển thị thời gian
    public GameObject loseCanvas; // canvas thua
    public Gameplay_Controller gameplayController;

    private bool isTimeRunning = true;

    void Update()
    {
        if (!isTimeRunning || Time.timeScale == 0) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            isTimeRunning = false;

            if (!gameplayController.IsVictory()) // nếu chưa thắng thì thua
            {
                Time.timeScale = 0f;
                if (loseCanvas != null)
                    loseCanvas.SetActive(true);
            }
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isTimeRunning = false;
    }
}
