using UnityEngine;

public class DoubleTapPause : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isVisible = false;
    private float lastTapTime = 0f;
    private const float doubleTapThreshold = 0.3f; // Time threshold for detecting a double tap
    private bool fingersWereLifted = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = isVisible;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            float currentTime = Time.time;
            if (fingersWereLifted && currentTime - lastTapTime < doubleTapThreshold)
            {
                ToggleSpriteRenderer();
                fingersWereLifted = false; // Prevent multiple toggles from the same tap
            }
            lastTapTime = currentTime;
        }
        else if (Input.touchCount == 0)
        {
            fingersWereLifted = true; // Reset when fingers are lifted
        }
    }

    private void ToggleSpriteRenderer()
    {
        isVisible = !isVisible;
        spriteRenderer.enabled = isVisible;
    }
}