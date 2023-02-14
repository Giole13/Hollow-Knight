using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInput;

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


    void Start()
    {

        // 인스턴스 초기화
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");

        // 변수 초기화
        speed = 7f;
        jumpForce = 10f;
        checkRadius = 0.3f;
        jumpTime = 0.3f;

        // 인스턴스 설정
        slashEffect.SetActive(false);
    }

    
    private void FixedUpdate()
    {
        // X축 이동방향 로직
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        PlayerMoveAndJumpBehavior();

        PlayerSlashBehavior();
    }

    // 플레이어 이동, 점프 함수
    private void PlayerMoveAndJumpBehavior()
    {
        // 땅과의 체크를 위해서 만든 로직
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // 만약 X축이 양수나 음수면 스프라이트 회전
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // 땅에 닿아있고 키를 누를 때 작동 -> 처음 한번 눌렀을 때 플레이어에게 가속도 고정
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // 점프 시간 초기화
            rb.velocity = Vector2.up * jumpForce;
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
    }


    // 플레이어 공격 함수
    private void PlayerSlashBehavior()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            slashAllow = true;
        }

        if (slashAllow)
        {
            slashAllow = false;
            StartCoroutine(PlayerAttackCoroutine());
        }
    }

    IEnumerator PlayerAttackCoroutine()
    {
        slashEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        slashEffect.SetActive(false);
    }
}
