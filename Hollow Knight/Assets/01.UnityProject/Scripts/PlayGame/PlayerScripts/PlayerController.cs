
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xInput;
    private float yInput;
    private UIObjsManger uIObjsScript = default;

    private float playerSpeed;

    public float speed
    {
        get { return playerSpeed; }

        private set { playerSpeed = value; }
    }
    

    public float jumpForce;

    [SerializeField]
    private bool isGrounded;

    private Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;


    private float jumpTimeCounter;
    private bool isJumping;
    private bool enEnemy;
    private bool skillCool;
    private int attackPower;
    private int jumpCount;

    public float jumpTime;

    private GameObject slashEffect = default;
    private GameObject ballEffect = default;
    private GameObject playerbody = default;

    private bool slashAllow;

    private Animator playerAni;

    private PlayerViewDir playerViewVertical;
    private PlayerViewDir playerViewHorizontal;


    public PlayerViewDir PlayerViewHorizontal
    {
        get { return playerViewHorizontal; }
    }

    //private void OnEnable()
    //{
    //    GioleFunc.GetRootObj("PlayerCamera").SetActive(true);
    //}

    public void PlayerVeloCityStop()
    {
        rb.velocity = Vector3.zero;
        playerAni.SetBool("Run", false);

    }

    void Start()
    {

        // iniyislize instance
        rb = GetComponent<Rigidbody2D>();
        feetPos = gameObject.FindChildObj("FeetPos").GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        slashEffect = gameObject.FindChildObj("SlashEffect");
        playerAni = gameObject.FindChildObj("Body").GetComponent<Animator>();
        uIObjsScript = GioleFunc.GetRootObj("UIObjs").GetComponent<UIObjsManger>();
        ballEffect = gameObject.FindChildObj("Ball");
        playerbody = gameObject.FindChildObj("Body");

        // initialize variable
        speed = 5f;
        jumpForce = 10f;
        checkRadius = 0.2f;
        jumpTime = 0.3f;
        slashAllow = true;
        enEnemy = true;
        attackPower = 10;
        skillCool = true;


        // instance Setting
        slashEffect.SetActive(false);
        ballEffect.SetActive(false);
    }


    private void FixedUpdate()
    {
        InputKeyValue();

    }

    private void Update()
    {
        PlayerMoveAndJumpBehavior();

        PlayerSlashwork();

        SkillActive();
    }

    private void SkillActive()
    {
        if (Input.GetKeyDown(KeyCode.C) && skillCool)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.A) && skillCool)
        {
            StartCoroutine(BallFire());
        }

    }



    IEnumerator BallFire()
    {
        skillCool = false;
        ballEffect.SetActive(true);
        ballEffect.transform.position = transform.position;
        switch (playerViewHorizontal)
        {
            case PlayerViewDir.RIGHT:
                ballEffect.transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10f;
                ballEffect.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case PlayerViewDir.LEFT:
                ballEffect.transform.GetComponent<Rigidbody2D>().velocity = Vector3.left * 10f;
                ballEffect.transform.localScale = new Vector3(-1f, 1f, 1f);
                break;
        }

        yield return new WaitForSeconds(1f);
        ballEffect.SetActive(false);
        skillCool = true;
    }

    IEnumerator Dash()
    {
        playerAni.SetBool("Dash", true);
        skillCool = false;
        switch (playerViewHorizontal)
        {
            case PlayerViewDir.RIGHT:
                rb.velocity = Vector2.right * 10;
                break;
            case PlayerViewDir.LEFT:
                rb.velocity = Vector2.left * 10;
                break;
        }
        rb.gravityScale = 0f;
        enabled = false;
        yield return new WaitForSeconds(0.5f);
        playerAni.SetBool("Dash", false);
        enabled = true;
        rb.gravityScale = 5f;
        rb.velocity = Vector2.zero;
        skillCool = true;
    }

    // Player Input Key
    private void InputKeyValue()
    {
        // init variable
        yInput = Input.GetAxis("Vertical");
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            xInput = 0f;
        }
        else
        {
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
        }

        if (0 < yInput)
        {
            playerViewVertical = PlayerViewDir.UP;
        }
        else if (0 > yInput)
        {
            playerViewVertical = PlayerViewDir.DOWN;
        }
        else if (0 == yInput)
        {
            playerViewVertical = PlayerViewDir.IDLE;
        }


        if (xInput != 0f && isGrounded)
        {
            playerAni.SetBool("Run", true);
        }
        else
        {
            playerAni.SetBool("Run", false);
        }
    }

    // Playe Move, Jump Fun
    private void PlayerMoveAndJumpBehavior()
    {

        // Grounded Check
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            // Init JumpCount
            jumpCount = 2;
            playerAni.SetBool("Jump", false);
            playerAni.SetBool("Jump_Down", false);
            playerAni.SetBool("Ground", true);
        }
        else if (!isGrounded)
        {
            playerAni.SetBool("Jump", true);
            playerAni.SetBool("Ground", false);
        }


        // Player Right
        if (xInput > 0)
        {
            playerbody.transform.localScale = new Vector3(-1f, 1f, 1f);
            playerViewHorizontal = PlayerViewDir.RIGHT;
        }
        // Player Left
        else if (xInput < 0)
        {
            playerbody.transform.localScale = new Vector3(1f, 1f, 1f);
            playerViewHorizontal = PlayerViewDir.LEFT;
        }



        // GroundJump and Second Jump
        if ((isGrounded == true || jumpCount == 1) && Input.GetKeyDown(KeyCode.Z))
        {
            //// Subtract JumpCount
            //--jumpCount;
            playerAni.SetBool("Jump_Down", false);
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if ((isJumping == true || jumpCount == 1) && Input.GetKey(KeyCode.Z))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                if (jumpCount == 0)
                {
                    isJumping = false;

                }
            }
        }

        if (-1 > rb.velocity.y)
        {
            if (isGrounded)
            {
                //rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
            playerAni.SetBool("Jump", false);
            playerAni.SetBool("Jump_Down", true);
        }


        // 하강속도 제한
        if (-15f > rb.velocity.y)
        {
            rb.velocity = Vector2.down * 15f;
        }

        // Jump key up
        if (Input.GetKeyUp(KeyCode.Z))
        {
            --jumpCount;
            if (jumpCount == 0)
            {
                isJumping = false;
            }
        }
    }


    // Player Attack Fun -> Dev
    private void PlayerSlashwork()
    {
        if (Input.GetKeyDown(KeyCode.X) && slashAllow)
        {
            StartCoroutine(PlayerAttackCoroutine(slashEffect, 0.5f));
        }
    }


    // Player Attack IEnumerator
    IEnumerator PlayerAttackCoroutine(GameObject obj_, float setTime)
    {
        slashAllow = false;
        obj_.SetActive(true);
        playerAni.SetTrigger("Attack");
        switch (playerViewVertical)
        {
            case PlayerViewDir.UP:
                obj_.transform.localPosition = new Vector3(0f, 1.5f, 0f);
                obj_.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
                obj_.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case PlayerViewDir.DOWN:
                obj_.transform.localPosition = new Vector3(0f, -1.5f, 0f);
                obj_.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                obj_.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case PlayerViewDir.IDLE:
                VerticalIdleAttack(obj_);
                break;
        }
        yield return new WaitForSeconds(setTime);
        slashAllow = true;
        obj_.SetActive(false);
    }


    private void VerticalIdleAttack(GameObject obj_)
    {
        switch (playerViewHorizontal)
        {
            case PlayerViewDir.LEFT:
                obj_.transform.localPosition = new Vector3(-1.5f, 0f, 0f);
                obj_.transform.localScale = new Vector3(1f, 1f, 1f);
                obj_.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case PlayerViewDir.RIGHT:
                obj_.transform.localPosition = new Vector3(1.5f, 0f, 0f);
                obj_.transform.localScale = new Vector3(-1f, 1f, 1f);
                obj_.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit Player
        if (collision.transform.tag.Equals("Monster") && enEnemy)
        {
            Damage();
        }
    }

    public void Damage()
    {
        UIObjsManger ui_ = GioleFunc.GetRootObj("UIObjs").GetComponent<UIObjsManger>();
        ui_.DamageHpIcon();

        StartCoroutine(TimeDelay());

    }

    // Player Hit TimeDlay
    IEnumerator TimeDelay()
    {
        // 맞으면 무적
        enEnemy = false;
        // 시간 정지
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.3f);
        // 시간 정지 해제
        Time.timeScale = 1f;
        // 밀려나는 로직
        switch (playerViewHorizontal)
        {
            // 왼쪽을 바라보면 오른쪽으로 밀려남
            case PlayerViewDir.LEFT:
                Debug.Log("[PlayerController] TimeDaly : Left to right push");
                rb.AddForce(Vector2.right * 7f);
                enabled = false;
                break;

            // 오른쪽을 바라모면 왼쪽으로 밀려남
            case PlayerViewDir.RIGHT:
                Debug.Log("[PlayerController] TimeDaly : Right to left push");
                rb.AddForce(Vector2.left * 7f);
                enabled = false;
                break;
        }
        yield return new WaitForSecondsRealtime(0.3f);
        rb.velocity = Vector2.zero;
        enabled = true;

        // 무적 시간
        yield return new WaitForSeconds(2f);
        // 무적 해제
        enEnemy = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case GioleData.TAG_NAME_MONSTER:        // Attack Monster
                collision.gameObject.GetComponent<MonsterClass>().HitMonster(attackPower);z
                break;
            case GioleData.TAG_NAME_COIN:
                uIObjsScript.CoinNumPlus("Small");
                collision.gameObject.SetActive(false);
                break;

        }
    }

    public void PlayerSitChair(bool setAni_)
    {
        playerAni.SetBool("Sit", setAni_);
        if (setAni_)
        {
            //  + y0.2 , rb.Type = Kenetic
            rb.position += Vector2.up * 0.2f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (!setAni_)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }   // PlayerSitChair()

    public void PlayerTalkNPC(bool setAni_)
    {
        playerAni.SetBool("LookUp", setAni_);

    }

    public void PlayerPickUpItem(bool setAni_)
    {
        playerAni.SetBool("PickUp", setAni_);
    }

    public int GetPlayerPresentCoin()
    {
        return uIObjsScript.GetCoin();
    }

}   // PlayerContorller()
