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
        playerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
        inventoryUI = GioleFunc.GetRootObj(GioleData.OBJ_NAME_INVENTORYUI);


        inventoryUI.SetActive(false);
        playerObj.SetActive(false);

        if (debugMode)
        {
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().DebugStart(debugLevel);
        }
    }

    private void Update()
    {
        if (playerObj.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !activeWindow)
            {
                activeWindow = true;
                inventoryUI.SetActive(true);
                playerObj.GetComponent<PlayerController>().PlayerVeloCityStop();
                playerObj.GetComponent<PlayerController>().enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && activeWindow)
            {
                activeWindow = false;
                inventoryUI.SetActive(false);
                playerObj.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    public void ActivePlayer()
    {
        playerObj.SetActive(true);
        GioleFunc.GetRootObj("PlayerCamera").SetActive(true);
    }

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

    public void GameEnding()
    {
        GioleFunc.GetRootObj("PlayerCamera").SetActive(false);
        playerObj.SetActive(false);

        GameObject title = GioleFunc.GetRootObj("Title_Main_Menu");
        title.SetActive(true);
    }
}
