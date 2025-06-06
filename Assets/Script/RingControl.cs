using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public List<Piece> pieces = new List<Piece>();
    private bool isRotating = false;
    public Transform greenNeedle;

    public Transform pivot;
    private Transform currentPivot;
    public GameObject dot2; // Dot 2 cần được điều khiển

    public static bool isDotAttached = false;  // Biến kiểm tra trạng thái dot đã được gắn
    public Transform pin2Pivot;  // Pivot cho Pin 2, gán từ Inspector

    private Collider2D dot2Collider; // Collider cho dot2

    void Start()
    {
        currentPivot = transform; // mặc định là tâm ring
        dot2Collider = dot2.GetComponent<Collider2D>(); // Lấy collider của dot2
    }

    public void SetPivot(Transform newPivot)
    {
        currentPivot = newPivot;  // Thay đổi tâm xoay
    }

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

        // Tính toán sự khác biệt giữa currentPivot và vị trí hiện tại của object
        Vector3 pivotPosition = currentPivot.position;
        Vector3 directionToPivot = transform.position - pivotPosition;
        float angle = -360f / pieces.Count;

        // Xoay đối tượng quanh pivot
        transform.DORotate(transform.eulerAngles + new Vector3(0, 0, angle), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isRotating = false;

                // Nếu đang xoay quanh Pin 2, và dot đã gắn, cập nhật vị trí dot
                if (currentPivot == pin2Pivot && dot2 != null && isDotAttached)
                {
                    // Định vị lại dot vào vị trí Pin 2
                    dot2.transform.position = pin2Pivot.position;

                    // Giả lập việc dot 2 đã dính vào Pin 2 (chỉ khi nó chạm vào pin2)
                    dot2Collider.enabled = false;  // Tắt collider để dot không di chuyển nữa
                    isDotAttached = false;  // Đánh dấu dot đã gắn vào Pin 2
                }
            });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Khi dot 2 va chạm với pin2, dừng lại và đính dot2 vào pin2
        if (other.CompareTag("Pin 2") && dot2 != null && !isDotAttached)
        {
            isDotAttached = true;
            dot2.transform.position = pin2Pivot.position;  // Dừng lại ở vị trí pin2
            dot2Collider.enabled = false;  // Tắt collider để dot không di chuyển nữa
            Debug.Log("Dot 2 đã dính vào Pin 2");
        }
    }
}
