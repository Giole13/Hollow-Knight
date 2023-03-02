using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CaterpillarsWhite : MonsterClass
{
    // 하얀색 애벌레 스크립트

    public float speed = 0f;

    private Rigidbody2D rb = default;

    private Vector2 frontCheckLineStart;
    private Vector2 checkLineDistance;

    public LayerMask groundLayer;


    private float rayDistance = 0f;
    private float moveDir = -1f;

    void OnEnable()
    {
        // 인스턴스 초기화
        rb = GetComponent<Rigidbody2D>();

        // 변수 초기화
        checkLineDistance = new Vector2(0f, -1f);
        rayDistance = 2f;


        // 코루틴 실행
        StartCoroutine(MonsterMove());

        // 초기값 설정
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        transform.localScale = new Vector3(-1f, 1f, 1f);
        moveDir = -1f;
        //transform.position = transform.parent.gameObject.FindChildObj("Caterpillars_White_StartPos").
        //    transform.position;

        // 몬스터 상태 변경
        //monState = MonsterState.ALIVE;
    }




    IEnumerator MonsterMove()
    {
        while (true)
        {
            CheckCliff();
            yield return new WaitForSeconds(0.2f);
        }
    }


    // 낭떠러지 체크하는 함수
    private void CheckCliff()
    {
        //Ray2D ray_ = new Ray2D(rb.position)
        frontCheckLineStart = rb.position + new Vector2(moveDir, 0.3f);
        RaycastHit2D checkCliffLineHit =
            Physics2D.Raycast(frontCheckLineStart, Vector2.down, rayDistance);

        if (!checkCliffLineHit)
        {   
            ChangeDir();
        }



    }

    // 낭떠러지까지 온다면 방향을 바꾸는 함수
    private void ChangeDir()
    {
        // 앞의 레이캐스트 방향을 바꿔주는 함수
        moveDir *= -1f;
        // 벨로시티 방향 바꾸는 로직
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        // 로컬스케일 뒤집어 주는 로직
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }


}
