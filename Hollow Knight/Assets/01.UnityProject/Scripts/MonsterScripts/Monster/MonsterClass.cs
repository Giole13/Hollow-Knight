using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterClass : MonoBehaviour
{
    public int coinNum;


    public GameObject coinObj;


    private void Awake()
    {
        coinObj = Resources.Load<GameObject>("Prefabs/Coin");
    }



    protected void CoinDrop()
    {

    }




}
