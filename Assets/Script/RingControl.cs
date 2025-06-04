using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public List<Piece> pieces = new List<Piece>();
    private bool isRotating = false;
    public Transform greenNeedle;

    public float rotationDuration = 0.3f;
    public void TryRotate()
    {
        if (isRotating) return;

        RotateClockwise();
    }

    void RotateClockwise()
    {
        isRotating = true;

        List<bool> tempFills = new List<bool>();
        foreach (var piece in pieces)
            tempFills.Add(piece.isFilled);

        for (int i = 0; i < pieces.Count; i++)
        {
            int nextIndex = (i + 1) % pieces.Count;
            pieces[nextIndex].SetFilled(tempFills[i]);
        }

        // Xoay theo chiều kim đồng hồ (âm độ)
        transform.DORotate(transform.eulerAngles + new Vector3(0, 0, -360f / pieces.Count), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isRotating = false;

            });
    }
}
