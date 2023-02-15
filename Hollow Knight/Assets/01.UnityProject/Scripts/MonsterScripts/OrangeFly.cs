using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFly : MonoBehaviour
{
    private Rigidbody2D rb = default;

    private bool die = false;

    [SerializeField]
    private bool debugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float horizontalViewAngle = 0f;    // 시야각
    [SerializeField] private float viewRadius = 1f;             // 기즈모 원의 크기 -> 탐색할 거리  
    [Range(-180f, 180f)]
    [SerializeField] private float viewRotateZ = 0f;        // 회전각


    [SerializeField] private LayerMask viewTargetMask;      // 적 레이어 -> 플레이어
    [SerializeField] private LayerMask viewObstacleMask;    // 장애물 레이어

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float horizontalViewHalfAngle = 0f;     // 시야각에서 정확히 가운데 라인


    // 입력한 -180~180의 값을 Up Vector 기준 Local Direction으로 변환시켜줌.
    // 회전각이 매개변수임   
    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

    private void OnDrawGizmos()
    {
        if (debugMode)
        {
            horizontalViewHalfAngle = horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;     // 내 위치

            Gizmos.DrawWireSphere(originPos, viewRadius);   // 기즈모의 원을 그려주는 함수

            // 오른쪽 광선 방향
            Vector3 horizontalRightDir = AngleToDirZ(-horizontalViewHalfAngle + viewRotateZ);
            // 왼쪽 광선 방향
            Vector3 horizontalLeftDir = AngleToDirZ(horizontalViewHalfAngle + viewRotateZ);
            // 가운데 광선 방향
            Vector3 lookDir = AngleToDirZ(viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * viewRadius, Color.cyan);   // 왼쪽 라인
            Debug.DrawRay(originPos, lookDir * viewRadius, Color.green);            // 정 가운데
            Debug.DrawRay(originPos, horizontalRightDir * viewRadius, Color.cyan);  // 오른쪽 라인

            FindViewTargets();
        }
    }


    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, viewRadius, viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(viewRotateZ);

            // float angle = Vector3.Angle(lookDir, dir)
            // 아래 두 줄은 위의 코드와 동일하게 동작함. 내부 구현도 동일
            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, viewRadius, viewObstacleMask);
                if (rayHitedTarget)
                {
                    if (debugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);

                    if (debugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }





    private void Awake()
    {
        horizontalViewHalfAngle = horizontalViewAngle * 0.5f; // 시야각에서 가운데 라인
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.up * 1.2f;
        //StartCoroutine(Flying());

    }

    // Update is called once per frames
    void Update()
    {
        //Debug.Log(transform.eulerAngles.z);


    }

    IEnumerator Flying()
    {
        while (die == false)
        {
            rb.velocity = Vector2.up * 1.2f;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
