using UnityEngine;

public class WhaleCameraController : MonoBehaviour
{
    public Transform cameraTransform; // メインカメラ
    public float moveSpeed = 4f; // カメラの移動速度を上げる
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

    // 🆕 プレイヤークジラ目線にカメラを移動する
    public void MoveCameraToPlayerView(Transform playerWhale)
    {
        if (!isMoving)
        {
            Vector3 offset = -playerWhale.forward * 1.5f + Vector3.up * 0.6f; // より近くする
            targetPosition = playerWhale.position + offset;
            targetRotation = playerWhale.rotation; // プレイヤークジラと同じ向きに
            isMoving = true;
        }
    }

    public void ResetCameraPosition()
    {
        targetPosition = defaultPosition;
        targetRotation = defaultRotation;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f &&
                Quaternion.Angle(cameraTransform.rotation, targetRotation) < 0.5f)
            {
                isMoving = false;
            }
        }
    }
}
