using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject playerObj = default;
    private GameObject inventoryUI = default;

    public bool debugMode = false;
    public GameObject debugLevel = default;

    private bool activeWindow = false;

    private void Awake()
    {
        // 인스턴스 초기화
        playerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
        inventoryUI = GioleFunc.GetRootObj(GioleData.OBJ_NAME_INVENTORYUI);


        inventoryUI.SetActive(false);
        playerObj.SetActive(false);

        if (debugMode)
        {
            // 디버그 전용 함수
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().DebugStart(debugLevel);
        }


    }

    private void Update()
    {


        if (playerObj.activeSelf)
        {
            // 창이 꺼져있으면 켜줌
            if (Input.GetKeyDown(KeyCode.Tab) && !activeWindow)
            {
                activeWindow = true;
                inventoryUI.SetActive(true);
                playerObj.GetComponent<PlayerController>().PlayerVeloCityStop();
                playerObj.GetComponent<PlayerController>().enabled = false;
            }
            // 창이 켜져있으면 꺼줌
            else if (Input.GetKeyDown(KeyCode.Escape) && activeWindow)
            {
                activeWindow = false;
                inventoryUI.SetActive(false);
                playerObj.GetComponent<PlayerController>().enabled = true;
            }
        }

    }


    // 새로운 게임을 시작했을 때
    public void ActivePlayer()
    {
        playerObj.SetActive(true);
        GioleFunc.GetRootObj("PlayerCamera").SetActive(true);

        //_PlayerObj.transform.position = DataManager.Instance.nowPlayer.playerPos.position;
    }

    // 게임을 로드했을 때
    public void ActivePlayer(GameObject player_)
    {
        GioleFunc.GetRootObj("PlayerCamera").SetActive(true);
        if (player_ == null || player_ == default)
        {
            playerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
        }
        else
        {
            playerObj = player_;
        }
        playerObj.SetActive(true);
        playerObj.transform.position = DataManager.Instance.nowPlayer.playerPos;
    }

    // 보스를 물리치고 엔딩을 볼때
    public void GameEnding()
    {
        // 엔딩영상을 보고 메인메뉴로 돌아간다.
        GioleFunc.GetRootObj("PlayerCamera").SetActive(false);
        playerObj.SetActive(false);
        

        GameObject title = GioleFunc.GetRootObj("Title_Main_Menu");
        title.SetActive(true);

    }

}
