using UnityEngine;
using UnityEngine.UI;

public class Gameplay_Controller : MonoBehaviour
{
    [SerializeField] private Transform blackBar;
    [SerializeField] private Transform pivot1;
    [SerializeField] private Transform pivot2;
    [SerializeField] private Transform[] circles;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float stopThreshold = 0.012f;

    private Transform currentPivot;
    private bool isRotating = false;
    private bool hasWon = false;

    void Update()
    {
        if (hasWon) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                Transform clickedPivot = null;
                if (hit.collider.transform == pivot1)
                {
                    clickedPivot = pivot1;
                }
                else if (hit.collider.transform == pivot2)
                {
                    clickedPivot = pivot2;
                }

                if (clickedPivot != null && IsPivotInCircle(clickedPivot))
                {
                    currentPivot = clickedPivot;
                    isRotating = true;
                    SoundManager.Instance.PlayRotateTick();
                }
            }
        }

        if (isRotating && currentPivot != null)
        {
            float rotationAmount = rotateSpeed * Time.deltaTime;
            blackBar.RotateAround(currentPivot.position, Vector3.forward, -rotationAmount);
            CheckCircleAlignment();
        }
    }

    private bool IsPivotInCircle(Transform pivot)
    {
        foreach (Transform circle in circles)
        {
            float distance = Vector2.Distance(pivot.position, circle.position);
            if (distance < stopThreshold)
            {
                return true;
            }
        }
        return false;
    }

    private void CheckCircleAlignment()
    {
        foreach (Transform circle in circles)
        {
            float distanceToCircle1 = Vector2.Distance(pivot1.position, circle.position);
            float distanceToCircle2 = Vector2.Distance(pivot2.position, circle.position);

            if (currentPivot != pivot1 && distanceToCircle1 < stopThreshold)
            {
                isRotating = false;
                currentPivot = pivot1;
                SoundManager.Instance.StopRotateTick();
                break;
            }
            else if (currentPivot != pivot2 && distanceToCircle2 < stopThreshold)
            {
                isRotating = false;
                currentPivot = pivot2;
                SoundManager.Instance.StopRotateTick();
                break;
            }
        }
    }
    public void SetVictory()
    {
        hasWon = true;
        isRotating = false;
        SoundManager.Instance.StopRotateTick();
    }
    public bool IsVictory()
    {
        return hasWon;
    }
}
