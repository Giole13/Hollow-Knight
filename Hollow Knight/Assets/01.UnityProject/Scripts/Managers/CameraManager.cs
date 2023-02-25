using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 cameraPosition = new Vector3(0, 1, -10);

    Transform playerTransform = default;

    private const float CAMERA_MOVE_SPEED = 10f;

    private void Awake()
    {
        playerTransform = GioleFunc.GetRootObj("Player").transform;
        gameObject.SetActive(false);
    }






    void FixedUpdate()
    {
        //transform.position = playerTransform.position + cameraPosition;
        transform.position = Vector3.Lerp(
            transform.position, playerTransform.position + cameraPosition,
            Time.deltaTime * CAMERA_MOVE_SPEED);
    }
}
