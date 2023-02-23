using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    private GameManager gameManager = default;
    private GameObject currentLevel = default;
    private GameObject gameStartSelectSave = default;
    private GameObject mainMenu = default;

    private void Awake()
    {
        // Init Instance
        gameManager = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEMANAGER).
            GetComponent<GameManager>();
        currentLevel = GioleFunc.GetRootObj("Level1");
        gameStartSelectSave = gameObject.FindChildObj("GameStart_SelectSave");
        mainMenu = gameObject.FindChildObj("MainMenu");


        // Setting Instance
        gameStartSelectSave.SetActive(false);
        mainMenu.SetActive(true);
    }


    // QuitGame
    public void OnClickExitGame()
    {
        GioleFunc.QuitThisGame();
    }

    // StartGame
    public void OnClickLoadGame()
    {
        // 메인 메뉴를 꺼주기
        gameObject.transform.parent.gameObject.SetActive(false);

        // 불러온 데이터를 적용시키기
        currentLevel = DataManager.Instance.nowPlayer.levelObj;

        // ProtoTypeDev : 시작시 레벨 1과 플레이어를 켜주는 로직
        currentLevel.SetActive(true);
        gameManager.ActivePlayer(DataManager.Instance.nowPlayer.playerObj);




    }

    public void OnClickNewGame()
    {
        // 새로운 게임을 만들었을 때
        // ProtoTypeDev : 시작시 레벨 1과 플레이어를 켜주는 로직
        currentLevel.SetActive(true);
        gameManager.ActivePlayer();
        

    }


    public void StartGameSelect()
    {
        mainMenu.SetActive(false);
        gameStartSelectSave.SetActive(true);
    }

    public void StartGameSelectBack()
    {
        mainMenu.SetActive(true);
        gameStartSelectSave.SetActive(false);
    }



}
