using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _PlayerObj = default;

    public bool _DebugMode = false;

    private void Awake()
    {
        // 인스턴스 초기화
        _PlayerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);

        _PlayerObj.SetActive(false);

        if (_DebugMode)
        {
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().OnClickLoadGame();

        }
    }

    // 새로운 게임을 시작했을 때
    public void ActivePlayer()
    {
        _PlayerObj.SetActive(true);
        //_PlayerObj.transform.position = DataManager.Instance.nowPlayer.playerPos.position;
    }

    // 게임을 로드했을 때
    public void ActivePlayer(GameObject player_)
    {
        if (player_ == null || player_ == default)
        {
            _PlayerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
        }
        else
        {
            _PlayerObj = player_;
        }
        _PlayerObj.SetActive(true);
        _PlayerObj.transform.position = DataManager.Instance.nowPlayer.playerPos;
    }
}
