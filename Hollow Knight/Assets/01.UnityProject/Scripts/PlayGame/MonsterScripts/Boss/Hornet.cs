using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : MonsterClass
{
    private LayerMask whatIsGround = default;


    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;
    private Rigidbody2D neilRb = default;


    private Transform feetPos = default;

    private float speed;
    private float jumpForce;
    private float checkRadius = default;




    public bool isGrounded = false;

    private HornetPattern hornetPT;

    //private HornetState hnState = default;


    void Awake()
    {
        // Init Instance
        playerRb = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER).GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").transform;
        whatIsGround = LayerMask.GetMask("Ground");
        neilRb = gameObject.FindChildObj("Neil").GetComponent<Rigidbody2D>();

        // Init Var
        speed = 7f;
        jumpForce = 15f;
        checkRadius = 0.2f;
        hornetPT = HornetPattern.IDLE;

        // Instance Setting
        gameObject.SetActive(false);
        neilRb.gameObject.SetActive(false);
    }


    //public HornetState HNStateHanle
    //{
    //    get
    //    {
    //        return hnState;
    //    }
    //    set
    //    {
    //        hnState = value;
    //    }
    //}

    private void OnEnable()
    {
        //StartCoroutine(Pattern());
        //hnState = new HNIdleState(this);
        //hnState.Action(this);
        Actting();
        Debug.Log("[Hornet] OnEnable : 호넷 등장!");
    }

    private void OnDisable()
    {
        BossTrigger bt = transform.parent.gameObject.FindChildObj("BossTrigger").GetComponent<BossTrigger>();
        bt.BossKill();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private void Actting()
    {
        switch (hornetPT)
        {
            case HornetPattern.IDLE:
                StartCoroutine(BackStep());
                break;
            case HornetPattern.MOVE:
                StartCoroutine(Move());
                break;
            case HornetPattern.BACKSTEP:
                StartCoroutine(BackStep());
                break;
            case HornetPattern.JUMPMOVE:
                StartCoroutine(JumpMove());
                break;
            case HornetPattern.JUMPSPHERE:
                StartCoroutine(JumpSphere());
                break;
            case HornetPattern.DASH:
                StartCoroutine(Dash());
                break;
            case HornetPattern.JUMPDASH:
                StartCoroutine(JumpDash());
                break;
            case HornetPattern.THROW:
                StartCoroutine(Throw());
                break;

        }
        RandomPT();
    }

    private void RandomPT()
    {
        // 패턴의 가짓수 만큼 숫자 추가
        int i = Random.Range(1, 7 + 1);
        hornetPT = (HornetPattern)i;
    }

    IEnumerator BackStep()
    {
        Vector2 back_ = new Vector2((rb.position.x - playerRb.position.x), 0f).normalized;
        rb.velocity = back_ * 7f + new Vector2(0f, 5f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }

    IEnumerator Move()
    {
        int i = Random.Range(0, 1 + 1);
        switch (i)
        {
            case 0:
                rb.velocity = Vector2.left * speed;
                break;
            case 1:
                rb.velocity = Vector2.right * speed;
                break;
        }
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }

    IEnumerator JumpMove()
    {
        int i = Random.Range(0, 1 + 1);
        switch (i)
        {
            case 0:
                rb.velocity = Vector2.right * speed;
                rb.velocity += Vector2.up * jumpForce;
                break;
            case 1:
                rb.velocity = Vector2.left * speed;
                rb.velocity += Vector2.up * jumpForce;
                break;
        }

        yield return new WaitForSeconds(0.5f);
        while (!isGrounded)
        {
            yield return new WaitForSeconds(0.5f);
            if (isGrounded) break;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }


    // 공중에서 360도 범위 공격
    IEnumerator JumpSphere()
    {
        int i = Random.Range(0, 1 + 1);
        switch (i)
        {
            case 0:
                rb.velocity = Vector2.right * speed;
                rb.velocity += Vector2.up * jumpForce;
                break;
            case 1:
                rb.velocity = Vector2.left * speed;
                rb.velocity += Vector2.up * jumpForce;
                break;
        }

        yield return new WaitForSeconds(1f);

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        rb.bodyType = RigidbodyType2D.Dynamic;

        while (!isGrounded)
        {
            yield return new WaitForSeconds(0.5f);
            if (isGrounded) break;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }

    // 대쉬 패턴
    IEnumerator Dash()
    {

        rb.velocity = new Vector2((playerRb.position.x - rb.position.x), 0f).normalized * speed * 2f;

        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }

    // 점프후 플레이어에게 돌진
    IEnumerator JumpDash()
    {
        int i = Random.Range(0, 1 + 1);
        switch (i)
        {
            case 0:
                rb.velocity = Vector2.right * speed;
                rb.velocity += Vector2.up * jumpForce;
                yield return new WaitForSeconds(1f);
                rb.velocity = playerRb.position - rb.position;
                break;
            case 1:
                rb.velocity = Vector2.left * speed;
                rb.velocity += Vector2.up * jumpForce;
                yield return new WaitForSeconds(1f);
                rb.velocity = playerRb.position - rb.position;
                break;
        }

        yield return new WaitForSeconds(0.5f);
        while (!isGrounded)
        {
            yield return new WaitForSeconds(0.5f);
            if (isGrounded) break;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }


    // 창 던지기
    IEnumerator Throw()
    {
        Vector2 neilPoint = new Vector2(playerRb.position.x, transform.position.y);


        bool reverse = false;
        float index = 0.1f;
        neilRb.gameObject.SetActive(true);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            neilRb.position = Vector2.Lerp(transform.position, neilPoint, index);
            if (1 < index)
            {
                reverse = true;
            }

            if (reverse)
            {
                index -= 0.01f;
            }
            else
            {
                index += 0.01f;
            }

            if(0 > index)
            {
                break;
            }
        }
        neilRb.gameObject.SetActive(false);
        
        Actting();

    }

    //private void ChangePT()
    //{
    //    hornetPT = (HornetPattern)Random.Range(1, 6 + 1);
    //}

    //IEnumerator Pattern()
    //{
    //    while (true)
    //    {

    //        switch (hornetPT)
    //        {
    //            case HornetPattern.IDLE:
    //                StartCoroutine(BackStep());
    //                break;
    //            case HornetPattern.DEATH:

    //                break;
    //        }
    //        break;

    //    }
    //}


    //IEnumerator BackStep()
    //{
    //    Vector2 back_ = (playerRb.position - rb.position).normalized;
    //    rb.velocity = back_ * 7f;
    //    ChangePT();
    //    yield return null;
    //}

}
