using UnityEngine;

public enum PinType { Pin1, Pin2 }

public class Pin_Click : MonoBehaviour
{
    public RingControl ringControl;
    public Transform pin2Pivot; // Pivot cho pin2 (tâm xoay mới)
    public PinType pinType;

    private void OnMouseDown()
    {
        if (ringControl == null) return;

        if (pinType == PinType.Pin1 && !RingControl.isDotAttached)
        {
            // Gắn dot 2 vào Pin 2 và thay đổi pivot của kim đen sang pin2
            ringControl.SetPivot(pin2Pivot);
            RingControl.isDotAttached = true;  // Đánh dấu dot đã được gắn

            Debug.Log("Dot 2 đã gắn vào Pin 2.");

            // Xoay kim đen và gắn dot vào pin2
            ringControl.TryRotate();
        }
        else if (pinType == PinType.Pin2 && RingControl.isDotAttached)
        {
            // Khi bấm Pin 2, nếu dot đã gắn, ta sẽ cho phép xoay
            ringControl.TryRotate();
        }
    }
}