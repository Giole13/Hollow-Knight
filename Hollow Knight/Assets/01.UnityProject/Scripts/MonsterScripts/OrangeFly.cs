using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFly : MonoBehaviour
{


    [SerializeField]
    private float radius;

    [SerializeField]
    private bool isPlayerinCircle = false;


    public LayerMask playerLayer;
    public LayerMask obstaclesLayer;

    public float speed;

    private Rigidbody2D rb;
    private Collider2D playerCd;

    void OnEnable()
    {
        // 인스턴스 초기화
        rb = GetComponent<Rigidbody2D>();

        // 코루틴 실행
        StartCoroutine(PlayerTrace());

        if (speed == 0f)
        {
            speed = 1f;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 기즈모 그려주는 함수
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}

    // 플레이어 추격하는 함수
    IEnumerator PlayerTrace()
    {
        while (true)
        {
            // 플레이어가 원 안에 들어왔다면 true
            if (Physics2D.OverlapCircle(transform.position, radius, playerLayer))
            {
                PlayerDirCheckTargetting();
            }
            //else
            //{
            //    Debug.Log("[PlayerTrace] else: 아무것오 안들어왔어요!");
            //}
            yield return new WaitForSeconds(0.2f);
        }
    }

    // 플레이어에게 레이캐스트를 쏴서 플레이어 정보를 알아오고 추격하는 함수
    private void PlayerDirCheckTargetting()
    {
        // 플레이어 레이어 걸러서 정보 저장
        Collider2D[] hitTargetCollider2DInfo = 
            Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        // 정보를 꺼내오는 로직
        foreach(Collider2D targetInfo in hitTargetCollider2DInfo)
        {
            // 레이 쏠 방향
            Vector2 dir_ = (targetInfo.transform.position - transform.position);
             
            // 레이 사거리
            float distance_ = (transform.position - targetInfo.transform.position).magnitude;

            // 맞은 친구 데이터 저장
            RaycastHit2D hitData = Physics2D.Raycast(transform.position, dir_, radius, playerLayer+obstaclesLayer);

            if(hitData == false)
            {
                /* DO nothing */
            }
            // 광선을 쐈는데 맞았다면 비교해서 플레이어라면?
            else if (hitData.collider.Equals(targetInfo))
            {
                //Debug.DrawRay(transform.position, dir_, Color.red);
                rb.velocity = dir_.normalized * speed;
            }
            else // 아니라면?
            {
                //Debug.DrawRay(transform.position, dir_, Color.green);
                rb.velocity = Vector2.zero;
            }
        }
    }
}
