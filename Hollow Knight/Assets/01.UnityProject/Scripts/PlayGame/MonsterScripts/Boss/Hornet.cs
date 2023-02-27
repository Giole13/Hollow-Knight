using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hornet : MonsterClass
{
    private LayerMask whatIsGround = default;
    private LayerMask playerLayer = default;

    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;
    private Rigidbody2D neilRb = default;

    private SpriteRenderer hnSR = default;

    private Transform feetPos = default;

    private float speed;
    private float jumpForce;
    private float checkRadius = default;

    private float nextTurnTime = 0.5f;
    private GameObject effectObj = default;

    public bool isGrounded = false;
    [SerializeField]
    private HornetPattern hornetPT;

    //private HornetState hnState = default;

    private Animator hnAni;
    private Animator effectAni;

    void Awake()
    {
        // Init Instance
        playerRb = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER).GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").transform;
        whatIsGround = LayerMask.GetMask("Ground");
        neilRb = gameObject.FindChildObj("Neil").GetComponent<Rigidbody2D>();
        hnAni = GetComponent<Animator>();
        playerLayer = LayerMask.GetMask("Player");
        effectObj = gameObject.FindChildObj("Effect");
        hnSR = GetComponent<SpriteRenderer>();
        effectAni = effectObj.GetComponent<Animator>();

        // Init Var
        speed = 7f;
        jumpForce = 15f;
        checkRadius = 0.2f;
        hornetPT = HornetPattern.IDLE;

        // Instance Setting
        gameObject.SetActive(false);
        neilRb.gameObject.SetActive(false);
        effectObj.SetActive(false);
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
        Debug.Log("[Hornet] OnEnable : ȣ�� ����!");
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
        Vector3 back_ = (rb.position - playerRb.position).normalized;

        if (back_.x < 0)
        {
            // �����ʿ� ���� ����
            hnSR.flipX = true;
        }
        else
        {
            // ���ʿ� ���� ����
            hnSR.flipX = false;
        }

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
                StartCoroutine(Dash(back_.x));
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
        // ������ ������ ��ŭ ���� �߰�
        int i = Random.Range(1, 7 + 1);
        hornetPT = (HornetPattern)i;
    }


    IEnumerator BackStep()
    {
        if (hornetPT == HornetPattern.IDLE)
        {
            yield return new WaitForSeconds(2f);
        }
        Vector2 back_ = new Vector2((rb.position.x - playerRb.position.x), 0f).normalized;
        // Back Step Start
        rb.velocity = back_ * 7f + new Vector2(0f, 3f);
        //transform.localScale = new Vector2(back_.x, 1f);
        hnAni.SetBool("BackStep", true);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        while (true)
        {
            if (isGrounded)
            {
                hnAni.SetBool("BackStep", false);
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(nextTurnTime);
        Actting();
    }

    IEnumerator Move()
    {
        int i = Random.Range(0, 1 + 1);
        switch (i)
        {
            case 0:
                rb.velocity = Vector2.right * speed;
                hnSR.flipX = true;
                //transform.localScale = new Vector2(-1f, 1f);
                break;
            case 1:
                rb.velocity = Vector2.left * speed;
                hnSR.flipX = false;
                //transform.localScale = new Vector2(1f, 1f);
                break;
        }
        hnAni.SetBool("Run", true);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        hnAni.SetBool("Run", false);
        yield return new WaitForSeconds(nextTurnTime);
        Actting();
    }

    // �÷��̾ ���� ����
    IEnumerator JumpMove()
    {
        //int i = Random.Range(0, 1 + 1);
        // Jump Start
        hnAni.SetBool("Jump", true);

        Vector2 dir = (playerRb.position - rb.position).normalized;
        rb.velocity = new Vector2(dir.x * speed, jumpForce);


        //switch (i)
        //{
        //    case 0:
        //        rb.velocity = Vector2.right * speed;
        //        rb.velocity += Vector2.up * jumpForce;
        //        transform.localScale = new Vector2(-1f, 1f);
        //        break;
        //    case 1:
        //        rb.velocity = Vector2.left * speed;
        //        rb.velocity += Vector2.up * jumpForce;
        //        transform.localScale = new Vector2(1f, 1f);
        //        break;
        //}

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            // Jump Stop
            if (isGrounded)
            {
                rb.velocity = Vector2.zero;
                hnAni.SetBool("Jump", false);
                break;
            }
        }

        yield return new WaitForSeconds(nextTurnTime);
        Actting();
    }


    // ���߿��� 360�� ���� ����
    IEnumerator JumpSphere()
    {
        // Jump Start

        hnAni.SetBool("Jump", true);
        Vector2 dir = (playerRb.position - rb.position).normalized;
        rb.velocity = new Vector2(dir.x * speed, jumpForce);

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Physics2D.OverlapCircle(transform.position, 3f, playerLayer))
            {
                // �÷��̾� �ĺ� �ϸ� �������
                effectObj.SetActive(true);
                effectAni.SetBool("Sphere", true);
                hnAni.SetBool("SphereAttack", true);
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(2f);
                rb.bodyType = RigidbodyType2D.Dynamic;
                hnAni.SetBool("SphereAttack", false);
                effectAni.SetBool("Sphere", false);
                effectObj.SetActive(false);
                break;
            }
            // ���� �����ϸ� Ż��
            else if (isGrounded)
            {
                rb.velocity = Vector2.zero;
                break;
            }
        }

        hnAni.SetBool("Jump", false);
        yield return new WaitForSeconds(nextTurnTime);
        Actting();
    }

    // Ground �뽬 ����
    IEnumerator Dash(float dir_)
    {
        // Dash Start
        hnAni.SetBool("GroundDash", true);
        yield return new WaitForSeconds(1f);
        // ���⼭ ����
        effectObj.SetActive(true);
        if (dir_ > 0)
        {
            // �����ʿ� ���� �ִٸ�
        }
        else
        {
            // ���ʿ� ���� �ִٸ�
            effectObj.transform.localPosition = new Vector2(transform.position.x+ 4.2f, transform.position.y + 0.5f);
        }
        rb.velocity = new Vector2((playerRb.position.x - rb.position.x), 0f).normalized * speed * 2f;

        yield return new WaitForSeconds(0.8f);
        effectObj.SetActive(false);
        effectObj.transform.localPosition = new Vector2(transform.position.x, transform.position.y);
        // ���⼭ ����
        hnAni.SetBool("GroundDash", false);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.8f);
        Actting();
    }

    // ������ �÷��̾�� ����
    IEnumerator JumpDash()
    {
        // �뽬 ���尡 0.6 ��
        hnAni.SetBool("Jump", true);
        Vector2 dir = (playerRb.position - rb.position).normalized;
        rb.velocity = new Vector2(dir.x * speed, jumpForce);

        // 1�� ���� �� ����
        yield return new WaitForSeconds(1f);
        rb.bodyType = RigidbodyType2D.Static;
        hnAni.SetBool("AirDash", true);
        yield return new WaitForSeconds(1f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = playerRb.position - rb.position;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isGrounded)
            {
                rb.velocity = Vector2.zero;
                hnAni.SetBool("AirDash", false);
                hnAni.SetBool("Jump", false);
                //yield return new WaitForSeconds(0.8f);
                break;
            }
        }

        yield return new WaitForSeconds(nextTurnTime);
        Actting();
    }


    // â ������
    IEnumerator Throw()
    {
        // �� ����Ʈ�� 0.6��
        if (9f > (playerRb.position - rb.position).magnitude)
        {
            RandomPT();
            Actting();
            yield break;
        }
        yield return new WaitForSeconds(0.5f);


        Vector2 neilPoint = default;

        Vector3 back_ = (playerRb.position - rb.position).normalized;
        //transform.localScale = new Vector3(-back_.x, 1f);
        if (back_.x > 0)
        {
            // ������ ������ �� �÷��̾� ����
            neilPoint = new Vector2(rb.position.x + 9f, rb.position.y);
            hnSR.flipX = true;
            neilRb.GetComponent<SpriteRenderer>().flipX = true;
            effectObj.transform.position = new Vector2(rb.position.x + 5f, rb.position.y);
            effectObj.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            // ������� ����
            neilPoint = new Vector2(rb.position.x - 9f, rb.position.y);
            hnSR.flipX = false;
            neilRb.GetComponent<SpriteRenderer>().flipX = false;
            effectObj.transform.position = new Vector2(rb.position.x - 5f, rb.position.y);
            effectObj.GetComponent<SpriteRenderer>().flipX = false;
        }


        bool reverse = false;
        float index = 0.1f;
        // Start Throw Neil
        hnAni.SetBool("Throw", true);
        yield return new WaitForSeconds(1f);
        neilRb.gameObject.SetActive(true);
        bool stopNeil = true;

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
                // ��ǥ�ʿ��� ���ƿ��� ����
                index -= 0.02f;
            }
            else
            {
                // ��ǥ������ ���� ����
                index += 0.02f;
                // ������ ���ٰ� ��� ��ٸ��� ����
                if (index > 0.98f && stopNeil)
                {
                    stopNeil = false;
                    effectObj.SetActive(true);
                    effectAni.SetBool("Neil", true);
                    yield return new WaitForSeconds(0.6f);
                    effectAni.SetBool("Neil", false);
                    effectObj.SetActive(false);

                }
            }

            if (0 > index)
            {
                break;
            }
        }
        // Stop Throw Neil
        effectObj.transform.position = new Vector2(rb.position.x, rb.position.y);
        hnAni.SetBool("Throw", false);
        neilRb.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.8f);

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
