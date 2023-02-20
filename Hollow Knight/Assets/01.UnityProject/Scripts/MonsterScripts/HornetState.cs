using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HornetState
{
    public void Action(Hornet hn);

}

//public class HNIdleState : HornetState
//{
//    public HNIdleState(Hornet hn_)
//    {
//        Action(hn_);
//    }

//    public void Action(Hornet hn)
//    {
//        //int randPT = Random.Range(0, 6+1);
//        int randPT = 0;
//        switch (randPT)
//        {
//            case 0:
//                hn.HNStateHanle = new HNBackStepState(hn);
//                break;
//        }

//    }
//}


//public class HNBackStepState : HornetState
//{
//    public HNBackStepState(Hornet hn_)
//    {
//        Action(hn_);
//    }

//    public void Action(Hornet hn)
//    {
//        // 백스텝 하는 로직
//        hn.BackStep();
//        //hn.HNStateHanle = new HNIdleState(hn);
//    }
//}