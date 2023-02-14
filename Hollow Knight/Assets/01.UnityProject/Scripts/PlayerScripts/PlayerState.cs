using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// �÷��̾� ���¸� �����ϴ� �������̽�
public interface PlayerState
{
    public void Switching(PlayerController playerState);
}


// �ƹ��� �ൿ�� ���� ���� ��
public class IdleState : PlayerState
{
    public void Switching(PlayerController playerState)
    {
        // ������ ����Ű�� �������
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerState.PlayerStateController = new RightMove();
        }

    }
}

public class RightMove : PlayerState
{
    public void Switching(PlayerController playerState)
    {
        transform.localPosition += new Vector3(_MoveForce, 0f, 0f) * Time.deltaTime;

    }
}
