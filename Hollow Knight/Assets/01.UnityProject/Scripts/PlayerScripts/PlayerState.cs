using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// 플레이어 상태를 관리하는 인터페이스
public interface PlayerState
{
    public void Switching(PlayerController player_);
}


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




