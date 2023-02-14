using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _JumpForce = 0f;
    public float _MoveForce = 0f;

    public float _JumpLimit = 0f;

    private PlayerState _PlayerState = default;

    private bool _Grounded = false;

    private float _StartJumpPosition = 0f;


    private float _PlayerYJumpFloatValue = 0f;
    private float _PlayerXMoveFloatValue = 0f;

    //public PlayerState PlayerStateController
    //{
    //    get
    //    {
    //        return _PlayerState;
    //    }
    //    set
    //    {
    //        _PlayerState = value;
    //    }
    //}

    // �⺻������ ���� ����
    private void Awake()
    {
        gameObject.SetActive(false);
    }


    void Start()
    {
        //_PlayerState = new IdleState();
    }

    void Update()
    {
        //PlayerXMove();
        //PlayerJump();
        //_PlayerState.Switching(this);

        PlayerXMove();

    }



    private void PlayerXMove()
    {
        _PlayerXMoveFloatValue = Input.GetAxis("Horizontal");

        Debug.Log(_PlayerXMoveFloatValue);

        // ���� Ű �Է½�
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            /* Do nothing */
        }
        // ������ ���� �Է�
        else if (0f < _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        }
        // ���� ���� �Է�
        else if (0f > _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        }

        // { LAGACY
        //if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        //{
        //    /* Do nothing */
        //    Debug.Log("���� ���� -> ����");
        //    return;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        // } LAGACY
        //}
    }

    
    private void PlayerJump()
    {
        _PlayerYJumpFloatValue = Input.GetAxis("Jump");

        Debug.Log(_PlayerXMoveFloatValue);

        if (0f < _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        }
        // ���� ���� �Է�
        else if(0f > _PlayerXMoveFloatValue)
        {
            transform.position += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        }

        //if (_Grounded)
        //{
        //    // ��������
        //    if (Input.GetKey(KeyCode.Z))
        //    {
        //        transform.position += new Vector3(0f, _JumpForce, 0f) * Time.deltaTime;
        //    }
        //    else if (Input.GetKeyUp(KeyCode.Z))
        //    {
        //        _Grounded = false;
        //    }
        //    if (_StartJumpPosition + _JumpLimit <= transform.position.y)
        //    {
        //        Debug.Log("���� ����!");
        //        _Grounded = false;
        //    }
        //}
    }


    //// ������ ����Ű�� �������
    //public void RightArrow()
    //{
    //    transform.localPosition += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
    //}

    //// ���� ����Ű�� �������
    //public void LeftArrow()
    //{
    //    transform.localPosition += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
    //}


    //// Z Ű�� �������
    //public void ClickOnZ()
    //{
    //    transform.localPosition += new Vector3(0f, _JumpForce, 0f) * Time.deltaTime;
    //}


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
            _Grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ ����, Ƣ�� ���� ��
        _StartJumpPosition = transform.position.y;
    }

}
