using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenVessel : MonsterClass
{
    public bool isGrounded = false;


    private LayerMask whatIsGround = default;

    private GameObject neilObj = default;

    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;


    private Transform feetPos = default;

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
            if (0 < -rb.position.x + playerRb.position.x && -rb.position.x + playerRb.position.x < 1)
            {
                xCheck = true;
            }
        }
    }

    private void OnEnable()
    {
        //StartCoroutine(Pattern());
        //hnState = new HNIdleState(this);
        //hnState.Action(this);
        Actting();
        //Debug.Log("[BrokenVessel] OnEnable : �� . �� . �� . �� . �� ����!");
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
                StartCoroutine(FireSphere());
                break;
        }
        RandomPT();
    }

    private void RandomPT()
    {
        // ������ ������ ��ŭ ���� �߰�
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
        Debug.Log("[BrokenVessel] Jump : Active");
        float xpos = (playerRb.position - rb.position).x / 2f;
        float ypos = 13f;
        rb.velocity = new Vector2(xpos, ypos);
        //yield return new WaitForSeconds(0.2f);
        //rb.velocity = Vector2.zero;

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
        Debug.Log("[BrokenVessel] GroundDash : Active");

        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), rb.velocity.y);
        rb.velocity = dir;

        yield return new WaitForSeconds(0.5f);
        Actting();
    }

    // ���� �� �뽬����
    IEnumerator JumpDash()
    {
        Debug.Log("[BrokenVessel] JumpDash : Active");
        rb.velocity = Vector2.up * 10f;
        yield return new WaitForSeconds(0.5f);
        Vector2 dir = new Vector2((playerRb.position.x - rb.position.x), 0f);
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


    // ���� �� �÷��̾ ���� �Ʒ����� �� ��ü �߻�
    IEnumerator JumpDown()
    {
        Debug.Log("[BrokenVessel] JumpDown : Active");

        xCheck = false;
        float xpos = (playerRb.position - rb.position).x;
        float ypos = 13f;
        rb.velocity = new Vector2(xpos, ypos);
        //yield return new WaitForSeconds(0.2f);
        //rb.velocity = Vector2.zero;

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (xCheck)
            {
                Debug.Log("[BrokenVessel] JumpDown : Jump!! Down!");
                rb.velocity = Vector2.down * 10f;
                break;
            }
            else if (isGrounded)
            {
                break;
            }
        }
        xCheck = false;
        Actting();
    }

    // ���缭 ��ü �߻�
    IEnumerator FireSphere()
    {
        Debug.Log("[BrokenVessel] FireSphere : Active");
        yield return new WaitForSeconds(3f);
    }

}
