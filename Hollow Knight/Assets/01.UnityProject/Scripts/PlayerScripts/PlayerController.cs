using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _JumpForce = 0f;
    private float _MoveForce = 0f;
    private float _JumpLimit = 0f;


    private bool _JumpAllow = false;

    private float _MaxJumpPosition = 0f;

    private float _PlayerXMoveFloatValue = 0f;




    private bool _RightMove = false;
    private bool _LeftMove = false;

    Rigidbody2D _PlayerRigidBody = default;



    // 기본적으로 꺼져 있음
    private void Awake()
    {
        gameObject.SetActive(false);

        // 인스턴스 초기화
        _PlayerRigidBody = GetComponent<Rigidbody2D>();

        // 변수 초기화
        _JumpForce = 7f;
        _MoveForce = 4f;
        _JumpLimit = 2f;
    }


    void Start()
    {

    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);

        Debug.DrawRay(transform.position, Vector2.right);

        if (hit.collider == null)
        {
            Debug.Log("전방에 레이 격발!");
            Debug.DrawRay(transform.position, Vector2.right, Color.red);
        }
        else
        {
            Debug.Log("전방에 레이 미 격발!");
            Debug.DrawRay(transform.position, Vector2.right, Color.green);
        }



        PlayerJump();

        PlayerXMove();
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


    private void PlayerXMove()
    {
        _PlayerXMoveFloatValue = Input.GetAxis("Horizontal");

        // 양쪽 키 입력시
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            /* Do nothing */
        }
        // 오른쪽 방향 입력
        else if (0f < _PlayerXMoveFloatValue)
        {
            //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
            transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;

        }
        // 왼쪽 방향 입력
        else if (0f > _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
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

    private void PlayerJump()
    {

        if (Input.GetKeyUp(KeyCode.Z))
        {
            _PlayerRigidBody.velocity = Vector2.zero;
            _JumpAllow = false;
            Debug.Log("점프키 땠다!!");
        }

        if (_JumpAllow)
        {
            if (Input.GetButton("Jump") && _JumpAllow)
            {
                //transform.position += new Vector3(0f, _JumpForce, 0f) * Time.deltaTime;
                _PlayerRigidBody.velocity = new Vector2(0f, _JumpForce);
            }

            if (_MaxJumpPosition < transform.position.y)
            {
                _PlayerRigidBody.velocity = Vector2.zero;
                _JumpAllow = false;
                Debug.Log("천장에 닿았다!");
            }
        }
        else if (!_JumpAllow)
        {
            _PlayerRigidBody.velocity = new Vector2(0f, -_JumpForce);
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
            _JumpAllow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 땅에서 점프, 튀어 나갈 때
        _MaxJumpPosition = transform.position.y + _JumpLimit;
    }

}
