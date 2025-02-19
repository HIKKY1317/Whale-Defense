using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed;

    private bool isMoving = true;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed);

            if(Vector3.Distance(transform.position, endPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
