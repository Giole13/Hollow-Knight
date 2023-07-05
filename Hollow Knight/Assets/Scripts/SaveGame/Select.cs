using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject[] slotObjText;        // ������ ���� Text ������Ʈ
    public GameObject[] removeSaveData;

    private bool[] saveFileArray = new bool[4];

    private MenuButtonManager menuBTManager = default;

    private void Awake()
    {
        menuBTManager = transform.parent.GetComponent<MenuButtonManager>();
    }


    private void OnEnable()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (File.Exists(DataManager.Instance.path + i))
            {
                saveFileArray[i] = true;
                DataManager.Instance.nowSlot = i;
                DataManager.Instance.LoadData();
                string areaName_ = DataManager.Instance.nowPlayer.areaName;
                slotObjText[i].SetTmpText($"\t    {areaName_}");
            }
            else
            {
                slotObjText[i].SetTmpText($"\t    새 게임");
                removeSaveData[i].SetActive(false);
            }
            DataManager.Instance.DataNewInit();
        }
    }


    // ������ 3���ε� ��� �˸°� �ҷ����°�?
    public void Slot(int number)
    {
        // ���� ��ȣ ����
        DataManager.Instance.nowSlot = number;

        // ���� ��ȣ�� ������ ������ true ���
        if (saveFileArray[number])
        {
            // 2. ����� �����Ͱ� ���� �� => �ҷ����� �ؼ� ���Ӿ����� �Ѿ.
            DataManager.Instance.LoadData();
            GoGame();
        }
        else
        {
            // 1. ����� �����Ͱ� ���� ��
            Create();
        }
    }

    public void RemoveData(int number)
    {
        DataManager.Instance.nowSlot = number;
        DataManager.Instance.RemoveData();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }


    public void Create()
    {
        menuBTManager.OnClickNewGame();
    }

    /// <summary>
    /// �� �������� �Ѿ�� �Լ�
    /// </summary>
    public void GoGame()
    {
        menuBTManager.OnClickLoadGame();
    }
}
