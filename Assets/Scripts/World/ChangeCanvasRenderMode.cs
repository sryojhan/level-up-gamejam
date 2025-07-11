using UnityEngine;

public class ChangeCanvasScreenPosition : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();

        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;

        canvas.planeDistance = 1;
    }
}
