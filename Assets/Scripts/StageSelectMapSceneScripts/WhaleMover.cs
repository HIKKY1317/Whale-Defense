using UnityEngine;

public class WhaleMover : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private enum MoveState { Idle, Moving, Arrived, Approached }
    private MoveState state = MoveState.Idle;

    private WhaleCameraController whaleCameraController;
    private Transform currentWhale;

    void Start()
    {
        targetPosition = transform.position;
        whaleCameraController = FindFirstObjectByType<WhaleCameraController>(); // カメラスクリプトを取得
    }

    void Update()
    {
        if (isMoving)
        {
            if (currentWhale != null)
            {
                Vector3 direction = (currentWhale.position - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 2f)
            {
                isMoving = false;
                state = MoveState.Arrived;

                if (currentWhale != null)
                {
                    RotateWhaleTowardsPlayer(currentWhale);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("StageWhale"))
                {
                    if (state == MoveState.Arrived && hit.collider.transform == currentWhale)
                    {
                        // 🆕 すでに到着しているクジラをクリック → プレイヤークジラ目線にカメラ移動
                        whaleCameraController.MoveCameraToPlayerView(transform);
                        state = MoveState.Approached;
                    }
                    else if (state != MoveState.Approached && hit.collider.transform != currentWhale)
                    {
                        targetPosition = hit.collider.transform.position;
                        isMoving = true;
                        state = MoveState.Moving;
                        currentWhale = hit.collider.transform;
                    }
                }
            }
        }
    }

    private void RotateWhaleTowardsPlayer(Transform whale)
    {
        Vector3 directionToPlayer = (transform.position - whale.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
        StartCoroutine(SmoothRotate(whale, targetRotation));
    }

    private System.Collections.IEnumerator SmoothRotate(Transform whale, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Quaternion startRotation = whale.rotation;

        while (elapsedTime < duration)
        {
            whale.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        whale.rotation = targetRotation;
    }

    public void ResetCameraPosition()
    {
        whaleCameraController.ResetCameraPosition();
        state = MoveState.Arrived;
    }
}
