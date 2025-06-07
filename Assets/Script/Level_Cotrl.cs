using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Level_Cotrl : MonoBehaviour
{
    public Button[] buttons;
    public LayoutGroup layoutGroup; // Gán trong Inspector

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Tắt LayoutGroup để không gây lỗi vị trí khi DOTween hoạt động
        if (layoutGroup != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            layoutGroup.enabled = false;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            AnimateButton(buttons[i].GetComponent<RectTransform>());
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level " + levelID;
        SceneManager.LoadScene(levelName);
    }

    private void AnimateButton(RectTransform rectTransform)
    {
        Vector2 originalPos = rectTransform.anchoredPosition;
        float moveAmount = 10f;
        float duration = 1.5f;

        rectTransform.DOAnchorPosY(originalPos.y + moveAmount, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
