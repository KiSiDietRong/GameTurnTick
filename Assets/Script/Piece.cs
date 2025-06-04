using UnityEngine;

public class Piece : MonoBehaviour
{
    public int index; 
    public bool isFilled;
    public SpriteRenderer fillRenderer;

    public void SetFilled(bool filled)
    {
        isFilled = filled;
        fillRenderer.enabled = filled;
    }
}
