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

    public void HitMonster(int i)
    {
        currentHp -= i;
    }

    private void Update()
    {
        if(currentHp <= 0)
        {
            CoinDrop();
            gameObject.SetActive(false);
            //this.enabled = false;
        }
    }


}
