using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenVessel : MonsterClass
{
    public bool isGrounded = false;

    private float jumpForce;


    private LayerMask whatIsGround = default;

    private GameObject neilObj = default;
    private GameObject shpere = default;

    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;


    private Transform feetPos = default;

    private float speed;
    private float checkRadius = default;

    private bool xCheck = false;


    private BrokenVesselPattoern brokenPT;

    private void Awake()
    {
        // Init Instance
        playerRb = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER).GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        whatIsGround = LayerMask.GetMask("Ground");
        feetPos = gameObject.FindChildObj("FeetPos").transform;
        neilObj = gameObject.FindChildObj("Neil");

        // Init Var
        speed = 7f;
        jumpForce = 15f;
        checkRadius = 0.2f;
        brokenPT = BrokenVesselPattoern.IDLE;


        // Instance Setting
        gameObject.SetActive(false);
        neilObj.SetActive(false);

        // Script Setting
        //this.enabled = true;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (xCheck)
        {
            /* Do nothing */
        }
        else if (!xCheck)
        {
            if( 0 < -rb.position.x + playerRb.position.x && -rb.position.x + playerRb.position.x < 1)
            {
                xCheck= true;
            }
        }
    }

    private void OnEnable()
    {
        //StartCoroutine(Pattern());
        //hnState = new HNIdleState(this);
        //hnState.Action(this);
        Actting();
        //Debug.Log("[BrokenVessel] OnEnable : 부 . 서 . 진 . 그 . 릇 등장!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            collision.GetComponent<PlayerController>().Damage();
        }
    }

    private void OnDisable()
    {
        Die();
    }

    private void Die()
    {
        BossTrigger bt = transform.parent.gameObject.FindChildObj("BossTrigger").GetComponent<BossTrigger>();
        bt.BossKill();
    }

    private void Actting()
    {
        rb.velocity = Vector2.zero;
        switch (brokenPT)
        {
            case BrokenVesselPattoern.IDLE:
                StartCoroutine(Move());
                break;
            case BrokenVesselPattoern.MOVE:
                StartCoroutine(Move());
                break;
            case BrokenVesselPattoern.JUMP:
                StartCoroutine(Jump());
                break;
            case BrokenVesselPattoern.SWINGNEIL:
                StartCoroutine(SwingNeil());
                break;
            case BrokenVesselPattoern.GROUNDDASH:
                StartCoroutine(GroundDash());
                break;
            case BrokenVesselPattoern.JUMPDASH:
                StartCoroutine(JumpDash());
                break;
            case BrokenVesselPattoern.JUMPDOWN:
                StartCoroutine(JumpDown());
                break;
            case BrokenVesselPattoern.FIRESPHERE:
                StartCoroutine(FireShpere());
                break;
        }
        RandomPT();
    }

    private void RandomPT()
    {
        // 패턴의 가짓수 만큼 숫자 추가
        int i = Random.Range(1, 6 + 1);
        brokenPT = (BrokenVesselPattoern)i;
    }


    IEnumerator Move()
    {
        Vector2 dir = (playerRb.position - rb.position).normalized;
        rb.velocity = dir * 2;
        yield return new WaitForSeconds(0.5f);
        Actting();
    }

    IEnumerator Jump()
    {
        float xpos = (playerRb.position - rb.position).x / 2f;
        float ypos = 13f;
        rb.velocity = new Vector2(xpos, ypos);

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isGrounded) break;
        }
        Actting();
    }

    IEnumerator SwingNeil()
    {
        neilObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        neilObj.SetActive(false);
        Actting();
    }

    IEnumerator GroundDash()
    {

        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), rb.position.y);
        rb.velocity = dir;

        yield return new WaitForSeconds(0.5f);
        Actting();
    }

    // 점프 후 대쉬공격
    IEnumerator JumpDash()
    {
        rb.velocity = Vector2.up * 10f;
        yield return new WaitForSeconds(0.5f);
        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), rb.position.y);
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = dir;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.down * 10f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isGrounded) break;
        }
        Actting();
    }


    // 점프 후 플레이어를 향해 아래공격 및 구체 발사
    IEnumerator JumpDown()
    {
        Debug.Log("[BrokenVessel] JumpDown : Active");

        xCheck = false;
        float xpos = (playerRb.position - rb.position).x;
        float ypos = 13f;
        rb.velocity = new Vector2(xpos, ypos);

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (xCheck)
            {
                Debug.Log("[BrokenVessel] JumpDown : Jump!! Down!");
                rb.velocity = Vector2.down * 10f;
                break;
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isGrounded) break;
        }
        xCheck = false;
        Actting();
    }

    // 멈춰서 구체 발사
    IEnumerator FireShpere()
    {
        yield return new WaitForSeconds(3f);
    }

}
