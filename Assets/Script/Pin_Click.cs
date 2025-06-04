using UnityEngine;

public class Pin_Click : MonoBehaviour
{
    public RingControl ringControl;
    private void OnMouseDown()
    {
        if (ringControl != null)
        {
            ringControl.TryRotate();
        }
    }
}
