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
        // �ν��Ͻ� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");
        playerAni = gameObject.FindChildObj("Body").GetComponent<Animator>();


        // ���� �ʱ�ȭ
        speed = 7f;
        jumpForce = 13f;
        checkRadius = 0.3f;
        jumpTime = 0.3f;

        // �ν��Ͻ� ����
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
        // X�� �̵����� ����
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        //�� Ű�� �������
        if (0 < yInput)
        {
            playerView = PlayerViewDir.UP;
            Debug.Log("[PlayerController] PlayerSlashwork : ���� ����!");
        }
        //�Ʒ� Ű�� �������
        else if (0 > yInput)
        {
            playerView = PlayerViewDir.DOWN;
            Debug.Log("[PlayerController] PlayerSlashwork : �Ʒ��� ����!");
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

    // �÷��̾� �̵�, ���� �Լ� -> Complation
    private void PlayerMoveAndJumpBehavior()
    {
        // ������ üũ�� ���ؼ� ���� ����
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // ���� X���� ����� ������ ��������Ʈ ȸ��
        if (xInput > 0)
        {
            // ������
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (xInput < 0)
        {
            // ����
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        // { ���� ����
        // ���� ����ְ� Ű�� ���� �� �۵� -> ó�� �ѹ� ������ �� �÷��̾�� ���ӵ� ����
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Z))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // ���� �ð� �ʱ�ȭ
            //rb.velocity = Vector2.up * jumpForce;
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
        // } ���� ����
    }


    // �÷��̾� ���� �Լ� -> Dev
    private void PlayerSlashwork()
    {
        //switch (playerView)
        //{
        //    case PlayerViewDir.UP:
        //        if (Input.GetKeyDown(KeyCode.X))
        //        {
        //            playerView = PlayerViewDir.UP;
        //            Debug.Log("[PlayerController] PlayerSlashwork : ���� ����!");
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
        //            Debug.Log("[PlayerController] PlayerSlashwork : �Ʒ��� ����!");
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
        //    Debug.Log("[PlayerController] PlayerSlashwork : ���� ����!");
        //}
        //else if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    Debug.Log("[PlayerController] PlayerSlashwork : �Ʒ��� ����!");
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
        // �÷��̾ ���𰡿� �ε�������
        if (collision.transform.tag.Equals("Monster"))
        {
            UIObjsManger ui_ = GioleFunc.GetRootObj("UIObjs").GetComponent<UIObjsManger>();
            ui_.DamageHpIcon();

            Debug.Log("[PlayerController] ���ݸ��� 2D : ����! �¾Ҵ�");
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
