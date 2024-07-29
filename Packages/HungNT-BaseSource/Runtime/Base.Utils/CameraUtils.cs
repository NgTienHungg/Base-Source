using UnityEngine;

public static class CameraUtils
{
    private static Camera mainCam;

    private static Camera MainCam
    {
        get
        {
            if (mainCam == null)
            {
                mainCam = Camera.main;
            }

            return mainCam;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        var mouseWorldPos = MainCam.ScreenToWorldPoint(mousePos); // z = -10
        return new Vector3(mouseWorldPos.x, mouseWorldPos.y);
    }

    public static Vector3 WorldToScreenPoint(Vector3 worldPos)
    {
        return MainCam.WorldToScreenPoint(worldPos);
    }
}