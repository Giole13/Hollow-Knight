using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFly : MonsterClass
{
    [SerializeField] private float radius;

    public float speed;
    public LayerMask obstaclesLayer;
    public LayerMask playerLayer;



    private Rigidbody2D rb;
    private Collider2D targetInfo;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(PlayerTrace());
        if (speed == 0f) { speed = 1f; }

    }

    private IEnumerator PlayerTrace()
    {
        while (true)
        {
            if (targetInfo = Physics2D.OverlapCircle(transform.position, radius, playerLayer))
            {
                PlayerDirCheckTargetting();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void PlayerDirCheckTargetting()
    {
        Vector2 dir_ = targetInfo.transform.position - transform.position;

        RaycastHit2D hitData = Physics2D.Raycast(transform.position, dir_,
                                            radius, playerLayer + obstaclesLayer);

        if (hitData.collider == targetInfo)
        {
            rb.velocity = dir_.normalized * speed;
        }
    }
}
