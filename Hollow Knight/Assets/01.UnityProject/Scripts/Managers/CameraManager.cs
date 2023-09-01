using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 cameraPosition = new Vector3(0, 2, -10);
    private Transform playerTransform = default;
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

    void FixedUpdate()
    {
        switch (cS)
        {
            case CameraState.NORMAL:
                transform.position = Vector3.Lerp(
                    transform.position, playerTransform.position + cameraPosition,
                    Time.deltaTime * CAMERA_MOVE_SPEED);
                break;
        }
    }

    public void BossFightView(GameObject centerObj_)
    {
        transform.position = new Vector3(
            centerObj_.transform.position.x, centerObj_.transform.position.y, -10f);
    }
}
