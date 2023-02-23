using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject[] slotObjText;        // ������ ���� Text ������Ʈ

    private bool[] saveFileArray = new bool[4];

    private MenuButtonManager menuBTManager = default;

    private void Awake()
    {
        menuBTManager = transform.parent.GetComponent<MenuButtonManager>();
    }


    private void Start()
    {
        for (int i = 0; i < 4; ++i)
        {
            // i ��°�� ������ �ִٸ�
            if (File.Exists(DataManager.Instance.path + i))
            {
                saveFileArray[i] = true;
                DataManager.Instance.nowSlot = i;
                DataManager.Instance.LoadData();
                string areaName_ = DataManager.Instance.nowPlayer.areaName;
                slotObjText[i].SetTmpText($"\t    {areaName_}");
            }
            // i ��°�� ������ ����ִٸ�
            else
            {
                slotObjText[i].SetTmpText($"\t    �� ����");
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
