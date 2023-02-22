using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10f;

    }


    //IEnumerator Move()
    //{
    //    yield return null;
    //}
}
