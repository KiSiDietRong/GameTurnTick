using UnityEngine;

public class BellControl : MonoBehaviour
{
    public GameObject bellBlack;  // Hình chuông đen
    public GameObject bellYellow; // Hình chuông vàng

    private bool isYellowActive = false;

    private void Start()
    {
        SetBellState(false); // Ban đầu là chuông đen
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name); // <-- kiểm tra xem có va chạm không

        if (other.CompareTag("Black"))
        {
            Debug.Log("Triggered by Black bar."); // <-- kiểm tra tag
            ToggleBell();
        }
    }

    void ToggleBell()
    {
        isYellowActive = !isYellowActive;
        SetBellState(isYellowActive);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBellSound();
        }
    }

    void SetBellState(bool yellowActive)
    {
        if (bellBlack != null)
            bellBlack.SetActive(!yellowActive);
        if (bellYellow != null)
            bellYellow.SetActive(yellowActive);
    }

    public bool IsYellowActive()
    {
        return isYellowActive;
    }

}
