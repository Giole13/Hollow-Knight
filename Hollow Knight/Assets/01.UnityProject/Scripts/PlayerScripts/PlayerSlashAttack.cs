using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashAttack : MonoBehaviour
{
    // �÷��̾� �⺻���� ��ũ��Ʈ
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ����������
        switch (collision.transform.tag)
        {
            case GioleData.TAG_NAME_MONSTER:        // ���͸� ����������
                collision.gameObject.SetActive(false);
                break;
        }
    }
}
