using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject playerObj = default;
    private GameObject inventoryUI = default;

    public bool _DebugMode = false;


    private bool activeWindow = false;

    private void Awake()
    {
        // �ν��Ͻ� �ʱ�ȭ
        playerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
        inventoryUI = GioleFunc.GetRootObj(GioleData.OBJ_NAME_INVENTORYUI);


        inventoryUI.SetActive(false);
        playerObj.SetActive(false);

        if (_DebugMode)
        {
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().OnClickNewGame();
        }


    }

    private void Update()
    {

        if (playerObj.activeSelf)
        {
            // â�� ���������� ����
            if (Input.GetKeyDown(KeyCode.Tab) && !activeWindow)
            {
                activeWindow = true;
                inventoryUI.SetActive(true);
                playerObj.GetComponent<PlayerController>().PlayerVeloCityStop();
                playerObj.GetComponent<PlayerController>().enabled = false;
            }
            // â�� ���������� ����
            else if (Input.GetKeyDown(KeyCode.Escape) && activeWindow)
            {
                activeWindow = false;
                inventoryUI.SetActive(false);
                playerObj.GetComponent<PlayerController>().enabled = true;
            }
        }


        // �κ��丮 â�� ������ ��
        //if (activeWindow)
        //{
        //    if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    {

        //    }

        //    if (Input.GetKeyDown(KeyCode.RightArrow))
        //    {

        //    }

        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {

        //    }

        //    if (Input.GetKeyDown(KeyCode.DownArrow))
        //    {

        //    }
        //}
    }






    // ���ο� ������ �������� ��
    public void ActivePlayer()
    {
        playerObj.SetActive(true);
        //_PlayerObj.transform.position = DataManager.Instance.nowPlayer.playerPos.position;
    }

    // ������ �ε����� ��
    public void ActivePlayer(GameObject player_)
    {
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


}
