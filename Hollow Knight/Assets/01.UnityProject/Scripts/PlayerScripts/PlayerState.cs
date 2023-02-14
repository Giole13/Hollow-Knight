using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// 플레이어 상태를 관리하는 인터페이스
public interface PlayerState
{
    public void Switching(PlayerController playerState);
}


// 아무런 행동을 하지 않을 때
public class IdleState : PlayerState
{
    public void Switching(PlayerController playerState)
    {
        // 오른쪽 방향키를 누를경우
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
