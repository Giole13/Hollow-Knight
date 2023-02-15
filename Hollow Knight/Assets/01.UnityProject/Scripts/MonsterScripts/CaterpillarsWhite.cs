using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarsWhite : MonoBehaviour
{
    // 하얀색 애벌레 스크립트

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
    //    Debug.Log("앙 맞았따 트리거");
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("앙 맞았따 콜리젼 터");
    //    switch (collision.transform.tag)
    //    {
    //        case GioleData.TAG_NAME_PLAYERATTACK:   // 플레이어 공격 충돌
    //            gameObject.SetActive(false);
    //            break;
    //        case GioleData.TAG_NAME_PLAYERBODY:     // 플레이어 바디 충돌
    //            collision.gameObject.SetActive(false);
    //            break;
    //    }
    //}
}
