using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public interface PlayerState
{
    public void Action(PlayerController_v2 player_);
}

public class IdleState : PlayerState
{
    public void Action(PlayerController_v2 player_)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            player_.PSHanDle = new AttackState();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            player_.PSHanDle = new JumpState();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player_.PSHanDle = new MoveState();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player_.PSHanDle = new MoveState();
        }
    }

}

public class AttackState : PlayerState
{
    public AttackState()
    {
        Debug.Log("�� ���� ����!");
    }
    public void Action(PlayerController_v2 player_)
    {
        player_.PlayerAttackDir(player_.PVHandle);

        player_.PSHanDle = new IdleState();
    }
}

// ���� ����
public class JumpState : PlayerState
{
    public JumpState()
    {
        Debug.Log("�� ���� ����!");
    }
    public void Action(PlayerController_v2 player_)
    {
        player_.PSHanDle = new IdleState();
    }
}


// �����̴� ���� 
public class MoveState : PlayerState
{
    public MoveState()
    {
        Debug.Log("�� �����̴� ����!");
    }

    public void Action(PlayerController_v2 player_)
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            player_.PSHanDle = new IdleState();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            player_.PSHanDle = new IdleState();
        }
    }
}


//// �� ���� ����
//public class UpViewState : PlayerState
//{
//    public UpViewState()
//    {
//        Debug.Log("�� ���� ���� ����!");
//    }

//    public void Action(PlayerController_v2 player_)
//    {
//        if (Input.GetKeyUp(KeyCode.X))
//        {
//            Debug.Log("�� �� ���� ���̾�!");
//        }
//        else if (Input.GetKeyUp(KeyCode.UpArrow))
//        {
//            player_.PSHanDle = new IdleState();
//        }
//    }
//}

//// �Ʒ� ���� ����
//public class DownViewState : PlayerState
//{
//    public DownViewState()
//    {
//        Debug.Log("�� �Ʒ��� ���� ����!");
//    }

//    public void Action(PlayerController_v2 player_)
//    {
//        if (Input.GetKeyUp(KeyCode.X))
//        {
//            Debug.Log("�� �Ʒ� ���� ���̾�!");
//        }
//        else if(Input.GetKeyUp(KeyCode.DownArrow))
//        {
//            player_.PSHanDle = new IdleState();
//        }
//    }
//}








// �ƹ��� �ൿ�� ���� ���� ��
//public class IdleState : PlayerState
//{
//    public void Switching(PlayerController player_)
//    {
//        // ������ ����Ű�� �������
//        if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            player_.PlayerStateController = new PlayerRightMove();
//        }
//        else if (Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            player_.PlayerStateController = new PlayerLeftMove();
//        }
//        else if (Input.GetKeyDown(KeyCode.Z))
//        {
//            player_.PlayerStateController = new PlayerJump();
//        }


//    }
//}

//public class PlayerRightMove : PlayerState
//{
//    public void Switching(PlayerController player_)
//    {
//        player_.RightArrow();
//        // ������ ����Ű�� �ø����
//        if (Input.GetKeyUp(KeyCode.RightArrow))
//        {
//            player_.PlayerStateController = new IdleState();
//        }

//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            player_.PlayerStateController = new PlayerJump();
//        }

//    }
//}

//public class PlayerLeftMove : PlayerState
//{
//    public void Switching(PlayerController player_)
//    {
//        player_.LeftArrow();
//        // ���� ����Ű�� �ø����
//        if (Input.GetKeyUp(KeyCode.LeftArrow))
//        {
//            player_.PlayerStateController = new IdleState();
//        }
//    }
//}

//public class PlayerJump : PlayerState
//{
//    public void Switching(PlayerController player_)
//    {
//        player_.ClickOnZ();
//        // ZŰ�� �� ���
//        if (Input.GetKeyUp(KeyCode.Z))
//        {
//            player_.PlayerStateController = new IdleState();
//        }

//        if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            player_.PlayerStateController = new PlayerRightMove();
//        }
//    }
//}




