using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour
{
    public int coinNum;

    public int maxhp;
    public int currentHp;


    //protected MonsterState monState;

    //private void Awake()
    //{

    //}

    private void Start()
    {
        currentHp = maxhp;
    }



    protected void CoinDrop()
    {
        SingletonManager.Instance.CoinPop(transform.position, coinNum);
    }

    public virtual void HitMonster(int i)
    {
        currentHp -= i;
    }

    private void Update()
    {
        // if ((currentHp <= 0) && (this.gameObject.name == "Hornet"))
        // {
        //     GameManager gm_ = GioleFunc.GetRootObj("GameManager").GetComponent<GameManager>();
        //     gm_.GameEnding();
        // }
        if (currentHp <= 0)
        {
            if (this.gameObject.name == "Hornet") { return; }
            CoinDrop();
            gameObject.SetActive(false);
            //this.enabled = false;
        }
    }


}
