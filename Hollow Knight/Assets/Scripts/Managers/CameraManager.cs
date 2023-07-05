using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 2f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;


    Vector3 cameraPosition = new Vector3(0, 2, -10);

    Transform playerTransform = default;

    private const float CAMERA_MOVE_SPEED = 7f;

    private CameraState cS = default;


    public CameraState CSHandler
    {
        get
        {
            return cS;
        }
        set
        {
            cS = value;
        }
    }


    private void Awake()
    {
        playerTransform = GioleFunc.GetRootObj("Player").transform;
        gameObject.SetActive(false);


    }




    void Update()
    {

        switch (cS)
        {
            // �Ϲ� ����
            case CameraState.NORMAL:
                Vector3 targetPosition = target.position + offset;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                // transform.position = playerTransform.position + cameraPosition;
                // transform.position = Vector3.Lerp(
                //     transform.position, playerTransform.position + cameraPosition,
                //     Time.deltaTime * CAMERA_MOVE_SPEED);
                break;
            // ���� ����
            case CameraState.BOSS:
                /* Do Nothing */
                break;

        }
        //transform.position = playerTransform.position + cameraPosition;
    }


    // �������� �� ī�޶� ���� ����
    public void BossFightView(GameObject centerObj_)
    {
        transform.position = new Vector3(
            centerObj_.transform.position.x, centerObj_.transform.position.y, -10f);
    }
}
