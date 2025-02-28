using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraAngleRadiansDegrees = 75f;

    private float verticalFov = 60f;
    private float screenWidth = 1920f;
    private float screenHeight = 1080f;

    public void SetCameraPosition(char[,] map)
    {
        if (map == null) return;

        int cols = map.GetLength(0);
        int rows = map.GetLength(1);

        float leftMargin = 200f / screenWidth * rows;
        float rightMargin = 200f / screenWidth * rows;
        float upperMargin = 200f / screenHeight * cols;
        float bottomMargin = 300f / screenHeight * cols;

        float xMapCenter = (cols - upperMargin + bottomMargin) / 2f;
        float zMapCenter = (rows - leftMargin + rightMargin) / 2f;

        float mapHeight = cols + bottomMargin + upperMargin;
        float mapWidth = rows + rightMargin + leftMargin;

        float distance = (mapHeight / 2f) / (Mathf.Cos(Mathf.Deg2Rad * (90f - cameraAngleRadiansDegrees)) - Mathf.Sin(Mathf.Deg2Rad * (90f - cameraAngleRadiansDegrees))) * Mathf.Cos(Mathf.Deg2Rad * (90f - cameraAngleRadiansDegrees));

        float horizontalFov = 2f * Mathf.Atan(Mathf.Tan(Mathf.Deg2Rad * verticalFov / 2f) * (screenWidth / screenHeight));

        float distanceHorizontal = (mapWidth / 2f) / Mathf.Tan(horizontalFov / 2f);

        float finalDistance = Mathf.Max(distance, distanceHorizontal);

        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        float xOffset = finalDistance * Mathf.Tan(Mathf.Deg2Rad * (90f - cameraAngleRadiansDegrees));

        mainCamera.transform.position = new Vector3(xMapCenter + xOffset, finalDistance, zMapCenter);
        mainCamera.transform.rotation = Quaternion.Euler(cameraAngleRadiansDegrees, -90f, 0f);
    }
}
