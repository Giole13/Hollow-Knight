using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : MonoBehaviour
{
    private LayerMask whatIsGround = default;


    private Rigidbody2D playerRb = default;
    private Rigidbody2D rb = default;

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
        hornetPT = HornetPattern.IDLE;


        // Init Var
        speed = 7f;
        jumpForce = 15f;
        checkRadius = 0.2f;

        // Instance Setting
        gameObject.SetActive(false);
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
        Die();
    }

    private void Die()
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

        }
        RandomPT();
    }

    private void RandomPT()
    {
        int i = Random.Range(1, 3 + 1);
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
            if(isGrounded) break;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Actting();
    }


    // 공중에서 360도 범위 공격
    IEnumerator JumpSphere()
    {

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
