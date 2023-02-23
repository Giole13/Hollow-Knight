using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFly : MonsterClass
{


    [SerializeField]
    private float radius;

    [SerializeField]
    // private bool isPlayerinCircle = false;


    public LayerMask playerLayer;
    public LayerMask obstaclesLayer;

    public float speed;

    private Rigidbody2D rb;
    private Collider2D playerCd;

    void OnEnable()
    {
        // �ν��Ͻ� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();

        // �ڷ�ƾ ����
        StartCoroutine(PlayerTrace());

        if (speed == 0f)
        {
            speed = 1f;
        }

    }


  

    IEnumerator PlayerTrace()
    {
        while (true)
        {
            // �÷��̾ �� �ȿ� ���Դٸ� true
            if (Physics2D.OverlapCircle(transform.position, radius, playerLayer))
            {
                PlayerDirCheckTargetting();
            }
            //else
            //{
            //    Debug.Log("[PlayerTrace] else: �ƹ��Ϳ� �ȵ��Ծ��!");
            //}
            yield return new WaitForSeconds(0.2f);
        }
    }

    // �÷��̾�� ����ĳ��Ʈ�� ���� �÷��̾� ������ �˾ƿ��� �߰��ϴ� �Լ�
    private void PlayerDirCheckTargetting()
    {
        // �÷��̾� ���̾� �ɷ��� ���� ����
        Collider2D[] hitTargetCollider2DInfo =
            Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        // ������ �������� ����
        foreach (Collider2D targetInfo in hitTargetCollider2DInfo)
        {
            // ���� �� ����
            Vector2 dir_ = (targetInfo.transform.position - transform.position);

            // ���� ��Ÿ�
            float distance_ = (transform.position - targetInfo.transform.position).magnitude;

            // ���� ģ�� ������ ����
            RaycastHit2D hitData = Physics2D.Raycast(transform.position, dir_, radius, playerLayer + obstaclesLayer);

            if (hitData == false)
            {
                /* DO nothing */
            }
            // ������ ���µ� �¾Ҵٸ� ���ؼ� �÷��̾���?
            else if (hitData.collider.Equals(targetInfo))
            {
                //Debug.DrawRay(transform.position, dir_, Color.red);
                rb.velocity = dir_.normalized * speed;
            }
            else // �ƴ϶��?
            {
                //Debug.DrawRay(transform.position, dir_, Color.green);
                rb.velocity = Vector2.zero;
            }
        }
    }
}
