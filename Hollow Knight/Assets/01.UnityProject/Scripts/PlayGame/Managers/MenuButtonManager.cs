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
        // ���� �޴��� ���ֱ�
        gameObject.transform.parent.gameObject.SetActive(false);

        // �ҷ��� �����͸� �����Ű��
        currentLevel = DataManager.Instance.nowPlayer.levelObj;

        // ProtoTypeDev : ���۽� ���� 1�� �÷��̾ ���ִ� ����
        currentLevel.SetActive(true);
        gameManager.ActivePlayer(DataManager.Instance.nowPlayer.playerObj);




    }

    public void OnClickNewGame()
    {
        // ���ο� ������ ������� ��
        // ProtoTypeDev : ���۽� ���� 1�� �÷��̾ ���ִ� ����
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
