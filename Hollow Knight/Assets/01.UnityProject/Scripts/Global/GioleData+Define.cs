
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GioleData
{
    // 핵심 오브젝트 이름 찾는 함수
    public const string OBJ_NAME_PLAYER = "Player";
    public const string OBJ_NAME_GAMEMANAGER = "GameManager";
    public const string OBJ_NAME_UIOBJS = "UIObjs";


    public const string TAG_NAME_PLAYERATTACK = "PlayerAttack";
    public const string TAG_NAME_PLAYERBODY = "Player";
    public const string TAG_NAME_MONSTER = "Monster";

}

public enum PlayerViewDir
{
    UP, DOWN, LEFT, RIGHT, IDLE

}       // PlayerVieDir

public enum HornetPattern
{
    IDLE,
    MOVE, BACKSTEP, JUMPMOVE, JUMPSPHERE, DASH, JUMPDASH, THROW, REST,
    DEATH
}


public enum BrokenVesselPattoern
{
    IDLE,
    MOVE, JUMP, SWINGNEIL , GROUNDDASH ,JUMPDASH, JUMPDOWN , FIRESPHERE, REST,
    DEATH
}
