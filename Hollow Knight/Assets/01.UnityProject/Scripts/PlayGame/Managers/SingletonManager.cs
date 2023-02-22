using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : GioleSingletone<SingletonManager>
{




    // 여기서 코인 불러오는 함수 호출
    public void CoinPop(Vector2 centerPos_, int coinNum_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.DropCoin(centerPos_, coinNum_);
    }


    // 코인을 꺼주거나 켜주는 함수
    public void AllCoinSet(bool set_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.AllCoinSetActive(set_);
    }


}
