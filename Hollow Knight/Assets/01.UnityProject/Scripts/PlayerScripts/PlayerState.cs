using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// �÷��̾� ���¸� �����ϴ� �������̽�
public interface PlayerState
{
    public void Switching(PlayerController player_);
}


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




