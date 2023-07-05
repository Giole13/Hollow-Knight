using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CaterpillarsWhite : MonsterClass
{
    // �Ͼ�� �ֹ��� ��ũ��Ʈ

    public float speed = 0f;

    private Rigidbody2D rb = default;

    private Vector2 frontCheckLineStart;
    private Vector2 checkLineDistance;

    public LayerMask groundLayer;


    private float rayDistance = 0f;
    private float moveDir = -1f;

    void OnEnable()
    {
        // �ν��Ͻ� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();

        // ���� �ʱ�ȭ
        checkLineDistance = new Vector2(0f, -1f);
        rayDistance = 2f;


        // �ڷ�ƾ ����
        StartCoroutine(MonsterMove());

        // �ʱⰪ ����
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        transform.localScale = new Vector3(-1f, 1f, 1f);
        moveDir = -1f;
        //transform.position = transform.parent.gameObject.FindChildObj("Caterpillars_White_StartPos").
        //    transform.position;

        // ���� ���� ����
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


    // �������� üũ�ϴ� �Լ�
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

    // ������������ �´ٸ� ������ �ٲٴ� �Լ�
    private void ChangeDir()
    {
        // ���� ����ĳ��Ʈ ������ �ٲ��ִ� �Լ�
        moveDir *= -1f;
        // ���ν�Ƽ ���� �ٲٴ� ����
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        // ���ý����� ������ �ִ� ����
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }


}
