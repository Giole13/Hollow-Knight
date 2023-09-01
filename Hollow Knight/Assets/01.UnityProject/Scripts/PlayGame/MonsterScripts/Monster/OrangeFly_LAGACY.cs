using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFly_LAGACY : MonoBehaviour
{
    private Rigidbody2D rb = default;

    private bool die = false;

    [SerializeField]
    private bool debugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float horizontalViewAngle = 0f;    // �þ߰�
    [SerializeField] private float viewRadius = 1f;             // ����� ���� ũ�� -> Ž���� �Ÿ�  
    [Range(-180f, 180f)]
    [SerializeField] private float viewRotateZ = 0f;        // ȸ����


    [SerializeField] private LayerMask viewTargetMask;      // �� ���̾� -> �÷��̾�
    [SerializeField] private LayerMask viewObstacleMask;    // ��ֹ� ���̾�

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float horizontalViewHalfAngle = 0f;     // �þ߰����� ��Ȯ�� ��� ����

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

            Vector3 originPos = transform.position;     // �� ��ġ

            Gizmos.DrawWireSphere(originPos, viewRadius);   // ������� ���� �׷��ִ� �Լ�

            // ������ ���� ����
            Vector3 horizontalRightDir = AngleToDirZ(-horizontalViewHalfAngle + viewRotateZ);
            // ���� ���� ����
            Vector3 horizontalLeftDir = AngleToDirZ(horizontalViewHalfAngle + viewRotateZ);
            // ��� ���� ����
            Vector3 lookDir = AngleToDirZ(viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * viewRadius, Color.cyan);   // ���� ����
            Debug.DrawRay(originPos, lookDir * viewRadius, Color.green);            // �� ���
            Debug.DrawRay(originPos, horizontalRightDir * viewRadius, Color.cyan);  // ������ ����

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
        horizontalViewHalfAngle = horizontalViewAngle * 0.5f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Flying()
    {
        while (die == false)
        {
            rb.velocity = Vector2.up * 1.2f;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
