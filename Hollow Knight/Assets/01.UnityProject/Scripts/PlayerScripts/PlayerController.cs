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

        // �ν��Ͻ� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");

        // ���� �ʱ�ȭ
        speed = 7f;
        jumpForce = 10f;
        checkRadius = 0.3f;
        jumpTime = 0.3f;

        // �ν��Ͻ� ����
        slashEffect.SetActive(false);
    }

    
    private void FixedUpdate()
    {
        // X�� �̵����� ����
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update()
    {
        PlayerMoveAndJumpBehavior();

        PlayerSlashBehavior();
    }

    // �÷��̾� �̵�, ���� �Լ�
    private void PlayerMoveAndJumpBehavior()
    {
        // ������ üũ�� ���ؼ� ���� ����
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // ���� X���� ����� ������ ��������Ʈ ȸ��
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // ���� ����ְ� Ű�� ���� �� �۵� -> ó�� �ѹ� ������ �� �÷��̾�� ���ӵ� ����
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // ���� �ð� �ʱ�ȭ
            rb.velocity = Vector2.up * jumpForce;
        }

        // ���� ����ְ� Ű�� ������ ���� �� �۵� -> ���� ��ŭ�� ���ӵ� ����
        if (isJumping == true && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                // �����ð���ŭ �����ϸ� ���� false
                isJumping = false;
            }
        }

        // Ű�� ���� �� �ߵ� -> 2�� ������ ���� ����
        if (Input.GetKeyUp(KeyCode.Z))
        {
            isJumping = false;
        }
    }


    // �÷��̾� ���� �Լ�
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
