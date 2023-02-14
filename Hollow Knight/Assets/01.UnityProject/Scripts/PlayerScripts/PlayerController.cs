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



    // �⺻������ ���� ����
    private void Awake()
    {
        gameObject.SetActive(false);

        // �ν��Ͻ� �ʱ�ȭ
        _PlayerRigidBody = GetComponent<Rigidbody2D>();

        // ���� �ʱ�ȭ
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
            Debug.Log("���濡 ���� �ݹ�!");
            Debug.DrawRay(transform.position, Vector2.right, Color.red);
        }
        else
        {
            Debug.Log("���濡 ���� �� �ݹ�!");
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

        // ���� Ű �Է½�
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            /* Do nothing */
        }
        // ������ ���� �Է�
        else if (0f < _PlayerXMoveFloatValue)
        {
            //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
            transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;

        }
        // ���� ���� �Է�
        else if (0f > _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        }




        //_PlayerXMoveFloatValue = Input.GetAxis("Horizontal");

        //// ���� Ű �Է½�
        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        //{
        //    /* Do nothing */
        //}
        //// ������ ���� �Է�
        //else if (0f < _PlayerXMoveFloatValue)
        //{
        //    //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        //    transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;

        //}
        //// ���� ���� �Է�
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
            Debug.Log("����Ű ����!!");
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
                Debug.Log("õ�忡 ��Ҵ�!");
            }
        }
        else if (!_JumpAllow)
        {
            _PlayerRigidBody.velocity = new Vector2(0f, -_JumpForce);
        }


        //if (Input.GetKey(KeyCode.Z))
        //{
        //    Debug.Log("����!");
        //}
        //else if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    Debug.Log("����Ű ����!");
        //}

    }



    // ���� ����� ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            // ��ֹ��� �΋L������
        }
        else
        {
            // ���� ����� ��
            _JumpAllow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ ����, Ƣ�� ���� ��
        _MaxJumpPosition = transform.position.y + _JumpLimit;
    }

}
