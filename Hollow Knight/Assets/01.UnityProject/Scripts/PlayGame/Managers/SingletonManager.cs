using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : GioleSingletone<SingletonManager>
{




    // ���⼭ ���� �ҷ����� �Լ� ȣ��
    public void CoinPop(Vector2 centerPos_, int coinNum_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.DropCoin(centerPos_, coinNum_);
    }


    // ������ ���ְų� ���ִ� �Լ�
    public void AllCoinSet(bool set_)
    {
        CoinManager coinManager = GioleFunc.GetRootObj("CoinManager").GetComponent<CoinManager>();
        coinManager.AllCoinSetActive(set_);
    }


}
