using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    private GameManager _GameManager = default;
    private GameObject _CurrentLevel = default;


    private void Awake()
    {
        _GameManager = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEMANAGER).
            GetComponent<GameManager>();

        _CurrentLevel = GioleFunc.GetRootObj("Level1");
    }


    void Start()
    {
        
    }




    void Update()
    {
        
    }

    // 게임종료 버튼을 누르면 게임 종료
    public void OnClickExitGame()
    {
        GioleFunc.QuitThisGame();
    }

    // 게임시작 버튼을 누르면 메뉴창을 닫고 게임시작
    public void OnClickStartGame()
    {
        Debug.Log("[MunewButtonManager] OnClickStartGame : 게임 시작이요!");
        gameObject.SetActive(false);

        _CurrentLevel.SetActive(true);
        _GameManager.ActivePlayer();
    }
}
