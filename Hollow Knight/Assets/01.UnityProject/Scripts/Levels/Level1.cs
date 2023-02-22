using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    // 시작할 때 맵을 꺼두기
    private void Awake()
    {
        gameObject.SetActive(false);
    }


    
    private void OnEnable()
    {

        // 켜질 때마다 모든레벨에 있는 모든 코인들을 꺼준다.
        SingletonManager.Instance.AllCoinSet(false);
    }

}
