using System.Collections.Generic;
using UnityEngine;

public class MoveRoute : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    private MapRoute mapRoute;
    private List<Vector2Int> route = new List<Vector2Int>();
    private int currentIndex = 0;
    private float threshold = 0.2f;
    private const float SQRT_3 = 1.732f;

    void Start()
    {
        mapRoute = FindFirstObjectByType<MapRoute>();
        if (mapRoute != null)
        {
            route = mapRoute.GetRoute();
            if (route.Count > 0)
            {
                MoveToNextPoint();
            }
        }
    }

    void Update()
    {
        if (route == null || route.Count == 0) return;
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (currentIndex < route.Count)
        {
            Vector3 targetPosition = new Vector3(route[currentIndex].x, 0f, route[currentIndex].y);
            float distance = Vector3.Distance(transform.position, targetPosition);

            float adjustedSpeed = (distance > SQRT_3) ? moveSpeed * 100f : moveSpeed;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, adjustedSpeed * Time.deltaTime);

            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (distance < threshold)
            {
                currentIndex++;
                if (currentIndex >= route.Count)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            currentIndex = route.Count - 1;
        }
    }
}
