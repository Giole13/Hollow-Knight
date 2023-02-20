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

    // ???????? ????? ?????? ???? ????
    public void OnClickExitGame()
    {
        GioleFunc.QuitThisGame();
    }

    // ??????? ????? ?????? ?????? ??? ???????
    public void OnClickStartGame()
    {
        gameObject.transform.parent.gameObject.SetActive(false);

        _CurrentLevel.SetActive(true);
        _GameManager.ActivePlayer();
    }
}
