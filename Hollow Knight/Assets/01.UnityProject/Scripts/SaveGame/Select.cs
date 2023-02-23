using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Select : MonoBehaviour
{
    public GameObject[] slotObjText;        // 슬릇의 정보 Text 오브젝트

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
            // i 번째에 슬릇이 있다면
            if (File.Exists(DataManager.Instance.path + i))
            {
                saveFileArray[i] = true;
                DataManager.Instance.nowSlot = i;
                DataManager.Instance.LoadData();
                string areaName_ = DataManager.Instance.nowPlayer.areaName;
                slotObjText[i].SetTmpText($"\t    {areaName_}");
            }
            // i 번째에 슬릇이 비어있다면
            else
            {
                slotObjText[i].SetTmpText($"\t    새 게임");
            }
            DataManager.Instance.DataNewInit();
        }
    }

    // 슬롯이 3개인데 어떻게 알맞게 불러오는가?
    public void Slot(int number)
    {
        // 현재 번호 저장
        DataManager.Instance.nowSlot = number;

        // 현재 번호에 데이터 파일이 true 라면
        if (saveFileArray[number])
        {
            // 2. 저장된 데이터가 있을 때 => 불러오기 해서 게임씬으로 넘어감.
            DataManager.Instance.LoadData();
            GoGame();
        }
        else
        {
            // 1. 저장된 데이터가 없을 때
            Create();
        }
    }

    public void Create()
    {
        menuBTManager.OnClickNewGame();
    }

    /// <summary>
    /// 인 게임으로 넘어가는 함수
    /// </summary>
    public void GoGame()
    {
        menuBTManager.OnClickLoadGame();
    }
}
