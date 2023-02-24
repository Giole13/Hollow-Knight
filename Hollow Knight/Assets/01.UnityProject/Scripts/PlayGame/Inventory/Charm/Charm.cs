using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : MonoBehaviour
{
    // 비용, 부적의 고유번호, 착용여부, 획득여부
    public int cost;
    public bool equip;
    public bool acquire;
}


//// 공격시 넉백 없음
//public class SteadyBody : Charm
//{
//    private SteadyBody()
//    {
//        cost = 1;
//        number = 1;
//    }
//}


//// 이동속도 증가
//public class SprintMaster : Charm
//{
//    private SprintMaster()
//    {
//        cost = 1;
//        number = 2;
//    }
//}
