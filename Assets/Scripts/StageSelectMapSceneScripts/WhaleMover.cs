using UnityEngine;

public class WhaleMover : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private enum MoveState { Idle, Moving, Arrived, Approached }
    private MoveState state = MoveState.Idle;

    private WhaleCameraController whaleCameraController;
    private Transform currentWhale;  // 現在のターゲットとなっているクジラ

    void Start()
    {
        targetPosition = transform.position;
        whaleCameraController = FindFirstObjectByType<WhaleCameraController>(); // カメラスクリプトを取得
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                isMoving = false;
                state = MoveState.Arrived;
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
                    if ((state == MoveState.Idle || state == MoveState.Arrived) && hit.collider.transform != currentWhale)
                    {
                        targetPosition = hit.collider.transform.position;
                        isMoving = true;
                        state = MoveState.Moving;
                        currentWhale = hit.collider.transform;
                    }
                    else if (state == MoveState.Arrived)
                    {
                        whaleCameraController.MoveCameraToWhale(hit.collider.transform);
                        state = MoveState.Approached;
                    }
                }
            }
        }
    }

    // カメラ定点に戻すボタンが押されたときに呼び出すメソッド
    public void ResetCameraPosition()
    {
        // カメラの位置を元に戻す処理（仮の処理）
        whaleCameraController.ResetCameraPosition();

        // プレイヤーの移動状態をArrivedに戻す
        state = MoveState.Arrived;
    }
}
