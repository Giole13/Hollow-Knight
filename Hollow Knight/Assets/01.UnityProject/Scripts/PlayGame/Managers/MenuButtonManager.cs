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

    // �������� ��ư�� ������ ���� ����
    public void OnClickExitGame()
    {
        GioleFunc.QuitThisGame();
    }

    // ���ӽ��� ��ư�� ������ �޴�â�� �ݰ� ���ӽ���
    public void OnClickStartGame()
    {
        Debug.Log("[MunewButtonManager] OnClickStartGame : ���� �����̿�!");
        gameObject.SetActive(false);

        _CurrentLevel.SetActive(true);
        _GameManager.ActivePlayer();
    }
}
