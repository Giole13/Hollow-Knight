using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _JumpForce = 0f;
    public float _MoveForce = 0f;

    private PlayerState _PlayerState = default;


    public PlayerState PlayerStateController
    {
        get
        {
            return _PlayerState;
        }
        set
        {
            _PlayerState = value;
        }
    }






    // 기본적으로 꺼져 있음
    private void Awake()
    {
        gameObject.SetActive(false);
    }


    void Start()
    {
        _PlayerState = new IdleState();
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += new Vector3(_MoveForce * -1, 0f, 0f) * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Z))
        {
            transform.localPosition += new Vector3(0f, _JumpForce, 0f) * Time.deltaTime;
        }

        _PlayerState.Switching(this);

    }



}
