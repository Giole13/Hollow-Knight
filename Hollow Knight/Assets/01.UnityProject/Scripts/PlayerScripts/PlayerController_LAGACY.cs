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

    // �⺻������ ���� ����
    private void Awake()
    {

        // �ν��Ͻ� �ʱ�ȭ
        playerRigidBody = GetComponent<Rigidbody2D>();
        slashEffect = gameObject.FindChildObj("SlashEffect");

        // ���� �ʱ�ȭ
        jumpForce = 7f;
        moveForce = 4f;
        jumpLimit = 2f;
        slashAllow = false;

        // ������Ʈ ��Ȱ��ȭ
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
        //    Debug.Log("���濡 ���� �ݹ�!");
        //    Debug.DrawRay(transform.position, Vector2.right, Color.red);
        //}
        //else
        //{
        //    Debug.Log("���濡 ���� �� �ݹ�!");
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


    // �÷��̾� �̵� ����
    private void PlayerXMove()
    {
        playerXMoveFloatValue = Input.GetAxis("Horizontal");

        // ���� Ű �Է½�
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            /* Do nothing */
        }
        // ������ ���� �Է�
        else if (0f < playerXMoveFloatValue)
        {
            //transform.position += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
            transform.position += new Vector3(moveForce, 0f, 0f) * Time.deltaTime;
            gameObject.transform.localScale = new Vector3(-1f, 1f,1f);

        }
        // ���� ���� �Է�
        else if (0f > playerXMoveFloatValue)
        {
            transform.position += new Vector3(moveForce * -1, 0f, 0f) * Time.deltaTime;
            gameObject.transform.localScale = new Vector3(1f, 1f,1f);
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

    // �÷��̾� ���� ����
    private void PlayerJump()
    {

        if (Input.GetKeyUp(KeyCode.Z))
        {
            playerRigidBody.velocity = Vector2.zero;
            jumpAllow = false;
            Debug.Log("����Ű ����!!");
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
                Debug.Log("õ�忡 ��Ҵ�!");
            }
        }
        else if (!jumpAllow)
        {
            playerRigidBody.velocity = new Vector2(0f, -jumpForce);
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

    // �÷��̾� ���� ����
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
            jumpAllow = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ ����, Ƣ�� ���� ��
        maxJumpPosition = transform.position.y + jumpLimit;
    }

}
