using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _PlayerObj = default;

    public bool _DebugMode = false;

    private void Awake()
    {
        // �ν��Ͻ� �ʱ�ȭ
        _PlayerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);

        _PlayerObj.SetActive(false);

        if (_DebugMode)
        {
            GioleFunc.GetRootObj("Title_Main_Menu").FindChildObj("MenuCanvas").
                GetComponent<MenuButtonManager>().OnClickLoadGame();

        }
    }

    // ���ο� ������ �������� ��
    public void ActivePlayer()
    {
        _PlayerObj.SetActive(true);
        //_PlayerObj.transform.position = DataManager.Instance.nowPlayer.playerPos.position;
    }

    // ������ �ε����� ��
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
