using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject[] slotObjText;
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
                slotObjText[i].SetTmpText($"\t    �� ����");
                removeSaveData[i].SetActive(false);
            }
            DataManager.Instance.DataNewInit();
        }
    }

    public void Slot(int number)
    {
        DataManager.Instance.nowSlot = number;

        if (saveFileArray[number])
        {
            DataManager.Instance.LoadData();
            GoGame();
        }
        else
        {
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

    public void GoGame()
    {
        menuBTManager.OnClickLoadGame();
    }
}
