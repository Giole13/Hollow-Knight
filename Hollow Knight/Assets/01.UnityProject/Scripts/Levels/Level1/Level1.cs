using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    // 해당 맵에서 관련있는 다음 맵을 미리 선언
    private GameObject Level2 = default;


    

    // 시작할 때 맵을 꺼두기
    private void Awake()
    {
        gameObject.SetActive(false);
        Level2 = GioleFunc.GetRootObj("Level2");
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
