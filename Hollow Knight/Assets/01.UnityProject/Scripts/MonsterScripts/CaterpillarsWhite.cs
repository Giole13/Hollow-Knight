using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarsWhite : MonoBehaviour
{
    // �Ͼ�� �ֹ��� ��ũ��Ʈ

    public float speed = 0f;

    private Rigidbody2D rb = default;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("�� �¾ҵ� Ʈ����");
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("�� �¾ҵ� �ݸ��� ��");
    //    switch (collision.transform.tag)
    //    {
    //        case GioleData.TAG_NAME_PLAYERATTACK:   // �÷��̾� ���� �浹
    //            gameObject.SetActive(false);
    //            break;
    //        case GioleData.TAG_NAME_PLAYERBODY:     // �÷��̾� �ٵ� �浹
    //            collision.gameObject.SetActive(false);
    //            break;
    //    }
    //}
}
