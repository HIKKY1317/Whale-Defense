using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float lookSpeed = 2.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Vector3 pos = transform.position;
        pos.y = 2.0f;
        transform.position = pos;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move) * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move.x, 0, move.z);

        transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
    }
}
