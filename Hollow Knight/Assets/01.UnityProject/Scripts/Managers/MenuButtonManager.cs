using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    private GameManager gameManager = default;
    private GameObject currentLevel = default;
    private GameObject gameStartSelectSave = default;
    private GameObject mainMenu = default;
    private GameObject titleObj = default;

    private void Awake()
    {
        // Init Instance
        gameManager = GioleFunc.GetRootObj(GioleData.OBJ_NAME_GAMEMANAGER).
            GetComponent<GameManager>();
        currentLevel = GioleFunc.GetRootObj("Level1");
        gameStartSelectSave = gameObject.FindChildObj("GameStart_SelectSave");
        mainMenu = gameObject.FindChildObj("MainMenu");
        titleObj = GioleFunc.GetRootObj("Title_Main_Menu");

        // Setting Instance
        gameStartSelectSave.SetActive(false);
        mainMenu.SetActive(true);
    }


    // Quit Game
    public void OnClickExitGame()
    {
        GioleFunc.QuitThisGame();
    }

    // Start Game To Load
    public void OnClickLoadGame()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        titleObj.SetActive(false);

        currentLevel = DataManager.Instance.nowPlayer.levelObj;

        currentLevel.SetActive(true);
        gameManager.ActivePlayer(DataManager.Instance.nowPlayer.playerObj);

    }

    public void DebugStart(GameObject level_)
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        titleObj.SetActive(false);

        currentLevel = DataManager.Instance.nowPlayer.levelObj;

        level_.SetActive(true);
        gameManager.ActivePlayer();

    }

    public void OnClickNewGame()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        titleObj.SetActive(false);
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
