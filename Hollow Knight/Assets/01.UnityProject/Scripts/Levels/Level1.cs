using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    // ������ �� ���� ���α�
    private void Awake()
    {
        gameObject.SetActive(false);
    }


    
    private void OnEnable()
    {

        // ���� ������ ��緹���� �ִ� ��� ���ε��� ���ش�.
        SingletonManager.Instance.AllCoinSet(false);
    }

}
