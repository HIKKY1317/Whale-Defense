using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public float buoyancyForce = 9.8f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y <= 0)
        {
            rb.AddForce(Vector3.up * buoyancyForce, ForceMode.Acceleration);
        }
    }
}
