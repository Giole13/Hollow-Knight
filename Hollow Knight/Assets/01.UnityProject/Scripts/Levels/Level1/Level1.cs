using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    // �ش� �ʿ��� �����ִ� ���� ���� �̸� ����
    private GameObject Level2 = default;


    

    // ������ �� ���� ���α�
    private void Awake()
    {
        gameObject.SetActive(false);
        Level2 = GioleFunc.GetRootObj("Level2");
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
