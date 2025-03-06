using UnityEngine;

public class WhaleCameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 1.5f;
    public float rotationSpeed = 3f;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isMoving = false;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // 初期位置と向きを保存
        defaultPosition = cameraTransform.position;
        defaultRotation = cameraTransform.rotation;
    }

    public void MoveCameraToWhale(Transform whaleTransform)
    {
        if (!isMoving)
        {
            Vector3 forwardDirection = whaleTransform.forward.normalized;

            Vector3 offset = -forwardDirection * 5f + Vector3.up * 2f;
            targetPosition = whaleTransform.position + offset;

            targetRotation = Quaternion.LookRotation(whaleTransform.position - targetPosition, Vector3.up);

            isMoving = true;
        }
    }

    public void ResetCameraPosition()
    {
        // カメラを初期位置に戻す
        targetPosition = defaultPosition;
        targetRotation = defaultRotation;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            // カメラの位置を滑らかに移動 (Lerpを使用)
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, moveSpeed * Time.deltaTime);

            // カメラの回転を滑らかに調整 (Slerpを使用)
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // ある程度目標に近づいたら移動を停止
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.05f &&
                Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1f)
            {
                isMoving = false;
            }
        }
    }
}
