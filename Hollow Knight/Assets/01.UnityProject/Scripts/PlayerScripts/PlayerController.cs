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

    private bool slashAllow = false;

    private Animator playerAni;

    PlayerViewDir playerView;
    void Start()
    {
        // 인스턴스 초기화
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");
        playerAni = gameObject.FindChildObj("Body").GetComponent<Animator>();


        // 변수 초기화
        speed = 7f;
        jumpForce = 13f;
        checkRadius = 0.3f;
        jumpTime = 0.3f;

        // 인스턴스 설정
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
        // X축 이동방향 로직
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        //위 키를 누를경우
        if (0 < yInput)
        {
            playerView = PlayerViewDir.UP;
            Debug.Log("[PlayerController] PlayerSlashwork : 위로 공격!");
        }
        //아래 키를 누를경우
        else if (0 > yInput)
        {
            playerView = PlayerViewDir.DOWN;
            Debug.Log("[PlayerController] PlayerSlashwork : 아래로 공격!");
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

    // 플레이어 이동, 점프 함수 -> Complation
    private void PlayerMoveAndJumpBehavior()
    {
        // 땅과의 체크를 위해서 만든 로직
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // 만약 X축이 양수나 음수면 스프라이트 회전
        if (xInput > 0)
        {
            // 오른쪽
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (xInput < 0)
        {
            // 왼쪽
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // { 점프 로직
        // 땅에 닿아있고 키를 누를 때 작동 -> 처음 한번 눌렀을 때 플레이어에게 가속도 고정
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // 점프 시간 초기화
            //rb.velocity = Vector2.up * jumpForce;
        }

        // 땅에 닿아있고 키가 눌리고 이을 때 작동 -> 누른 만큼만 가속도 고정
        if (isJumping == true && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                // 설정시간만큼 점프하면 점핑 false
                isJumping = false;
            }
        }

        // 키를 땠을 때 발동 -> 2단 점프를 막는 로직
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }
        // } 점프 로직
    }


    // 플레이어 공격 함수 -> Dev
    private void PlayerSlashwork()
    {
        //switch (playerView)
        //{
        //    case PlayerViewDir.UP:
        //        if (Input.GetKeyDown(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.UP;
        //            Debug.Log("[PlayerController] PlayerSlashwork : 위로 공격!");
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
        //            Debug.Log("[PlayerController] PlayerSlashwork : 아래로 공격!");
        //        }
        //        if (Input.GetKeyUp(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.IDLE;
        //        }
        //        break;
        //    default:
        //        break;
        //}

        if (Input.GetKeyDown(KeyCode.X))
        {
            slashAllow = true;
        }

        //else if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    Debug.Log("[PlayerController] PlayerSlashwork : 위로 공격!");
        //}
        //else if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("[PlayerController] PlayerSlashwork : 아래로 공격!");
        //}

        if (slashAllow)
        {
            slashAllow = false;
            StartCoroutine(PlayerAttackCoroutine());
        }
    }

    IEnumerator PlayerAttackCoroutine()
    {
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
        slashEffect.SetActive(false);
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 무언가와 부딧혔을때
        if (collision.transform.tag.Equals("Monster"))
        {
            UIObjsManger ui_ = GioleFunc.GetRootObj("UIObjs").GetComponent<UIObjsManger>();
            ui_.DamageHpIcon();

            Debug.Log("[PlayerController] 온콜리젼 2D : 으악! 맞았다");
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
