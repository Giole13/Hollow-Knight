using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// 플레이어 상태를 관리하는 인터페이스
public interface PlayerState
{
    // 바뀔때 실행하는 함수
    public void Action(PlayerController_v2 player_);
}


// 아이들 상태
public class IdleState : PlayerState
{

    public IdleState()
    {
        Debug.Log("난 아이들 상태!");
    }
    // 아이들 상태에서?
    public void Action(PlayerController_v2 player_)
    {
        // 공격 전환
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

        //if(Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    player_.PSHanDle = new UpViewState();
        //}
        
        //if(Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    player_.PSHanDle = new DownViewState();
        //}
    }

}

// 공격 상태
public class AttackState : PlayerState
{
    public AttackState()
    {
        Debug.Log("난 공격 상태!");
    }
    public void Action(PlayerController_v2 player_)
    {
        player_.PlayerAttackDir(player_.PVHandle);

        player_.PSHanDle = new IdleState();
    }
}

// 점프 상태
public class JumpState : PlayerState
{
    public JumpState()
    {
        Debug.Log("난 점프 상태!");
    }
    public void Action(PlayerController_v2 player_)
    {
        player_.PSHanDle = new IdleState();
    }
}


// 움직이는 상태 
public class MoveState : PlayerState
{
    public MoveState()
    {
        Debug.Log("난 움직이는 상태!");
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


//// 위 보는 상태
//public class UpViewState : PlayerState
//{
//    public UpViewState()
//    {
//        Debug.Log("난 위를 보는 상태!");
//    }

//    public void Action(PlayerController_v2 player_)
//    {
//        if (Input.GetKeyUp(KeyCode.X))
//        {
//            Debug.Log("난 위 공격 중이야!");
//        }
//        else if (Input.GetKeyUp(KeyCode.UpArrow))
//        {
//            player_.PSHanDle = new IdleState();
//        }
//    }
//}

//// 아래 보는 상태
//public class DownViewState : PlayerState
//{
//    public DownViewState()
//    {
//        Debug.Log("난 아래를 보는 상태!");
//    }

//    public void Action(PlayerController_v2 player_)
//    {
//        if (Input.GetKeyUp(KeyCode.X))
//        {
//            Debug.Log("난 아래 공격 중이야!");
//        }
//        else if(Input.GetKeyUp(KeyCode.DownArrow))
//        {
//            player_.PSHanDle = new IdleState();
//        }
//    }
//}








// 아무런 행동을 하지 않을 때
//public class IdleState : PlayerState
//{
//    public void Switching(PlayerController player_)
//    {
//        // 오른쪽 방향키를 누를경우
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
//        // 오른쪽 방향키를 올릴경우
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
//        // 왼쪽 방향키를 올릴경우
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
//        // Z키를 땔 경우
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




