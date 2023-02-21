using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenVessel : MonoBehaviour
{
    public bool isGrounded = false;

    private float jumpForce;


    private LayerMask whatIsGround = default;

    private GameObject neilObj = default;

    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;


    private Transform feetPos = default;

    private float speed;
    private float checkRadius = default;




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
        neilObj.SetActive(false);

        // Script Setting
        //this.enabled = true;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private void OnEnable()
    {
        //StartCoroutine(Pattern());
        //hnState = new HNIdleState(this);
        //hnState.Action(this);
        Actting();
        Debug.Log("[BrokenVessel] OnEnable : 부 . 서 . 진 . 그 . 릇 등장!");
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
                //case HornetPattern.BACKSTEP:
                //    StartCoroutine(BackStep());
                //    break;
                //case HornetPattern.JUMPMOVE:
                //    StartCoroutine(JumpMove());
                //    break;
                //case HornetPattern.JUMPSPHERE:
                //    StartCoroutine(JumpSphere());
                //    break;
                //case HornetPattern.DASH:
                //    StartCoroutine(Dash());
                //    break;
                //case HornetPattern.JUMPDASH:
                //    StartCoroutine(JumpDash());
                //    break;
                //case HornetPattern.THROW:
                //    StartCoroutine(Throw());
                //    break;

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
            if (isGrounded)
            {
                break;
            }
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
        Debug.Log("[BrokenVessel] GroundDash : Ground!! dash!!");
        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), rb.position.y);
        rb.velocity = dir;

        yield return new WaitForSeconds(0.5f);
        Actting();
    }

    IEnumerator JumpDash()
    {
        rb.velocity = Vector2.up * 10f;
        yield return new WaitForSeconds(0.5f);
        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), rb.position.y);
        rb.velocity = dir;
        yield return new WaitForSeconds(0.5f);
        Actting();
    }

    IEnumerator JumpDown()
    {
        float xpos = (playerRb.position - rb.position).x / 2f;
        float ypos = 13f;
        rb.velocity = new Vector2(xpos, ypos);

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (rb.position.x == playerRb.position.x)
            {
                rb.velocity = Vector2.down * 10f;
                break;
            }
            else if (isGrounded)
            {
                break;
            }
        }
        Actting();
    }

    // 멈춰서 구체 발사
    //IEnumerator FireShpere()
    //{

    //}

}
