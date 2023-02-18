using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;
    private float yInput;


    public float speed;
    public float jumpForce;


    private bool isGrounded;

    private Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;


    private float jumpTimeCounter;
    private bool isJumping;

    public float jumpTime;

    private GameObject slashEffect = default;

    private bool slashAllow;

    private Animator playerAni;

    PlayerViewDir playerView;
    void Start()
    {
        // ?¥í???? ???\
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");
        playerAni = gameObject.FindChildObj("Body").GetComponent<Animator>();


        // ???? ????
        speed = 7f;
        jumpForce = 13f;
        checkRadius = 0.3f;
        jumpTime = 0.3f;
        slashAllow = true;

        // ?¥í???? ????
        slashEffect.SetActive(false);
    }


    private void FixedUpdate()
    {
        InputKeyValue();
    }

    private void Update()
    {
        PlayerMoveAndJumpBehavior();

        PlayerSlashwork();

    }

    private void InputKeyValue()
    {
        // X?? ??????? ????
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        //?? ??? ???????
        if (0 < yInput)
        {
            playerView = PlayerViewDir.UP;
            Debug.Log("[PlayerController] PlayerSlashwork : ???? ????!");
        }
        //??? ??? ???????
        else if (0 > yInput)
        {
            playerView = PlayerViewDir.DOWN;
            Debug.Log("[PlayerController] PlayerSlashwork : ????? ????!");
        }
        else if (0 == yInput)
        {
            playerView = PlayerViewDir.IDLE;
        }


        if (xInput != 0f)
        {
            playerAni.SetBool("Run", true);
        }
        else
        {
            playerAni.SetBool("Run", false);
        }
    }

    // ?¡À???? ???, ???? ??? -> Complation
    private void PlayerMoveAndJumpBehavior()
    {
        // ?????? ???? ????? ???? ????
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // ???? X???? ????? ?????? ????????? ???
        if (xInput > 0)
        {
            // ??????
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (xInput < 0)
        {
            // ????
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // { ???? ????
        // ???? ?????? ??? ???? ?? ??? -> ??? ??? ?????? ?? ?¡À?????? ????? ????
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // ???? ?©£? ????
            //rb.velocity = Vector2.up * jumpForce;
        }

        // ???? ?????? ??? ?????? ???? ?? ??? -> ???? ????? ????? ????
        if (isJumping == true && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                // ?????©£???? ??????? ???? false
                isJumping = false;
            }
        }

        // ??? ???? ?? ??? -> 2?? ?????? ???? ????
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }
        // } ???? ????
    }


    // ?¡À???? ???? ??? -> Dev
    private void PlayerSlashwork()
    {
        //switch (playerView)
        //{
        //    case PlayerViewDir.UP:
        //        if (Input.GetKeyDown(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.UP;
        //            Debug.Log("[PlayerController] PlayerSlashwork : ???? ????!");
        //        }
        //        if (Input.GetKeyUp(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.IDLE;
        //        }
        //        break;
        //    case PlayerViewDir.DOWN:
        //        if (Input.GetKeyDown(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.DOWN;
        //            Debug.Log("[PlayerController] PlayerSlashwork : ????? ????!");
        //        }
        //        if (Input.GetKeyUp(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.IDLE;
        //        }
        //        break;
        //    default:
        //        break;
        //}

        if (Input.GetKeyDown(KeyCode.X) && slashAllow)
        {
            StartCoroutine(PlayerAttackCoroutine());

        }

        //else if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    Debug.Log("[PlayerController] PlayerSlashwork : ???? ????!");
        //}
        //else if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("[PlayerController] PlayerSlashwork : ????? ????!");
        //}


    }

    IEnumerator PlayerAttackCoroutine()
    {
        slashAllow = false;
        slashEffect.SetActive(true);
        switch (playerView)
        {
            case PlayerViewDir.UP:
                slashEffect.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                slashEffect.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                break;
            case PlayerViewDir.DOWN:
                slashEffect.transform.localPosition = new Vector3(0f, -1.5f, 0f);
                slashEffect.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                break;
            case PlayerViewDir.IDLE:
                slashEffect.transform.localPosition = new Vector3(-1.5f, 0f, 0f);
                slashEffect.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
        }
        yield return new WaitForSeconds(0.5f);
        slashAllow = true;
        slashEffect.SetActive(false);
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ?¡À???? ????? ?¥å???????
        if (collision.transform.tag.Equals("Monster"))
        {
            UIObjsManger ui_ = GioleFunc.GetRootObj("UIObjs").GetComponent<UIObjsManger>();
            ui_.DamageHpIcon();

            Debug.Log("[PlayerController] ??????? 2D : ????! ?¨ú??");
            //StartCoroutine(TimeDelay());
        }
    }


    IEnumerator TimeDelay()
    {

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
    }

}
