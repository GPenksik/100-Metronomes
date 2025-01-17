using UnityEngine;

public static class InputChecker
{
    public static bool IsTouchBegan()
    {
        if (Time.timeScale == 1f)
        {
            // Check for mouse/touch input OR clap detection
            return (Input.GetMouseButtonDown(0) ||
                   (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                   (AudioInputDetector.Instance != null && AudioInputDetector.Instance.WasClapped()));
        }
        return false;
    }
}