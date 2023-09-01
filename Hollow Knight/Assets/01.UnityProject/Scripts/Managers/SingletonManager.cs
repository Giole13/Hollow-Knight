using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : GioleSingletone<SingletonManager>
{
    public void CoinPop(Vector2 centerPos_, int coinNum_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.DropCoin(centerPos_, coinNum_);
    }

    public void AllCoinSet(bool set_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.AllCoinSetActive(set_);
    }
}
