using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CaterpillarsWhite : MonsterClass
{
    public float speed = 0f;
    public LayerMask groundLayer;


    private Rigidbody2D rb = default;

    private Vector2 frontCheckLineStart;
    private Vector2 checkLineDistance;
    private float rayDistance = 0f;
    private float moveDir = -1f;

    private void Awake()
    {
        rayDistance = 2f;
        moveDir = -1f;
        checkLineDistance = new Vector2(0f, -1f);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        transform.localScale = new Vector3(-1f, 1f, 1f);

        StartCoroutine(MonsterMove());
    }

    private IEnumerator MonsterMove()
    {
        while (true)
        {
            CheckCliff();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void CheckCliff()
    {
        frontCheckLineStart = rb.position + new Vector2(moveDir, 0.3f);

        if (!Physics2D.Raycast(frontCheckLineStart, Vector2.down, rayDistance))
        {
            ChangeDir();
        }
    }


    private void ChangeDir()
    {
        moveDir *= -1f;
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }
}
