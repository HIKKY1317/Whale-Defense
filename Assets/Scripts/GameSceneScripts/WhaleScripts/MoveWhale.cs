using System.Collections.Generic;
using UnityEngine;

public class MoveWhale : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    private MapRoute mapRoute;
    private List<Vector2Int> route = new List<Vector2Int>();
    private int currentIndex = 0;
    private float threshold = 0.2f;
    private const float SQRT_3 = 1.732f;
    private WhaleAttributes whaleAttributes;
    private PlayerHpManager playerHpManager;

    void Start()
    {
        playerHpManager = FindFirstObjectByType<PlayerHpManager>();

        whaleAttributes = GetComponent<WhaleAttributes>();
        if (whaleAttributes != null)
        {
            moveSpeed = whaleAttributes.swimSpeed;
        }

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

            float adjustedSpeed = (distance > SQRT_3) ? moveSpeed * 2.0f : moveSpeed;

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
                    playerHpManager.TakeDamage(1);
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