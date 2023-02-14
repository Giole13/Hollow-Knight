using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _PlayerObj = default;

    public bool _DebugMode = false;

    private void Awake()
    {
        // 플레이어 초기화
        _PlayerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);


        if (_DebugMode)
        {
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().OnClickStartGame();
        }
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }




    public void ActivePlayer()
    {
        _PlayerObj.SetActive(true);
    }
}
