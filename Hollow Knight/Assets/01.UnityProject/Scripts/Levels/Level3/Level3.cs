using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    // ������ �� ���� ���α�
    private void Awake()
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        foreach (Transform obj_ in transform)
        {
            obj_.gameObject.SetActive(true);
        }
    }

}
