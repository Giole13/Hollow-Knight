using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_LAGACY : MonoBehaviour
{
    private float jumpForce = 0f;
    private float moveForce = 0f;
    private float jumpLimit = 0f;


    private bool jumpAllow = false;

    private float maxJumpPosition = 0f;

    private float playerXMoveFloatValue = 0f;


    private bool slashAllow = false;

    private bool _RightMove = false;
    private bool _LeftMove = false;

    Rigidbody2D playerRigidBody = default;

    GameObject slashEffect = default;

    // 기본적으로 꺼져 있음
    private void Awake()
    {

        // 인스턴스 초기화
        playerRigidBody = GetComponent<Rigidbody2D>();
        slashEffect = gameObject.FindChildObj("SlashEffect");

        // 변수 초기화
        jumpForce = 7f;
        moveForce = 4f;
        jumpLimit = 2f;
        slashAllow = false;

        // 오브젝트 비활성화
        gameObject.SetActive(false);
        slashEffect.SetActive(false);
    }


    void Start()
    {

    }

    void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);

        //Debug.DrawRay(transform.position, Vector2.right);

        //if (hit.collider == null)
        //{
        //    Debug.Log("전방에 레이 격발!");
        //    Debug.DrawRay(transform.position, Vector2.right, Color.red);
        //}
        //else
        //{
        //    Debug.Log("전방에 레이 미 격발!");
        //    Debug.DrawRay(transform.position, Vector2.right, Color.green);
        //}



        PlayerJump();

        PlayerXMove();

        PlayerAttack();

    }

    private void FixedUpdate()
    {

        //    if (_RightMove)
        //    {
        //        _RightMove = false;
        //        _PlayerRigidBody.velocity = new Vector2(_MoveForce, _PlayerRigidBody.velocity.y);
        //    }
        //    else if (_LeftMove)
        //    {
        //        _LeftMove = false;
        //        _PlayerRigidBody.velocity = new Vector2(-1f * _MoveForce, _PlayerRigidBody.velocity.y);
        //    }
        //
    }


    // 플레이어 이동 동작
    private void PlayerXMove()
    {
        playerXMoveFloatValue = Input.GetAxis("Horizontal");

        // 양쪽 키 입력시
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            /* Do nothing */
        }
        // 오른쪽 방향 입력
        else if (0f < playerXMoveFloatValue)
        {
            //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
            transform.position += new Vector3(moveForce, 0f, 0f) * Time.deltaTime;
            gameObject.transform.localScale = new Vector3(-1f, 1f,1f);

        }
        // 왼쪽 방향 입력
        else if (0f > playerXMoveFloatValue)
        {
            transform.position += new Vector3(moveForce * -1, 0f, 0f) * Time.deltaTime;
            gameObject.transform.localScale = new Vector3(1f, 1f,1f);
        }




        //_PlayerXMoveFloatValue = Input.GetAxis("Horizontal");

        //// 양쪽 키 입력시
        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        //{
        //    /* Do nothing */
        //}
        //// 오른쪽 방향 입력
        //else if (0f < _PlayerXMoveFloatValue)
        //{
        //    //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        //    transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;

        //}
        //// 왼쪽 방향 입력
        //else if (0f > _PlayerXMoveFloatValue)
        //{
        //    transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        //}

    }

    // 플레이어 점프 동작
    private void PlayerJump()
    {

        if (Input.GetKeyUp(KeyCode.Z))
        {
            playerRigidBody.velocity = Vector2.zero;
            jumpAllow = false;
            Debug.Log("점프키 땠다!!");
        }

        if (jumpAllow)
        {
            if (Input.GetButton("Jump") && jumpAllow)
            {
                //transform.position += new Vector3(0f, _JumpForce, 0f) * Time.deltaTime;
                playerRigidBody.velocity = new Vector2(0f, jumpForce);
            }

            if (maxJumpPosition < transform.position.y)
            {
                playerRigidBody.velocity = Vector2.zero;
                jumpAllow = false;
                Debug.Log("천장에 닿았다!");
            }
        }
        else if (!jumpAllow)
        {
            playerRigidBody.velocity = new Vector2(0f, -jumpForce);
        }


        //if (Input.GetKey(KeyCode.Z))
        //{
        //    Debug.Log("점프!");
        //}
        //else if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    Debug.Log("점프키 해제!");
        //}

    }

    // 플레이어 공격 동작
    private void PlayerAttack()
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



    // 땅과 닿았을 때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            // 장애물과 부딯쳤을때
        }
        else
        {
            // 땅에 닿았을 때
            jumpAllow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 땅에서 점프, 튀어 나갈 때
        maxJumpPosition = transform.position.y + jumpLimit;
    }

}
