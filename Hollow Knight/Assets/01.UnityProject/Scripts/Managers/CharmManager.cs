using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CharmManager : MonoBehaviour
{
    // 보유한 부적 리스트
    public List<GameObject> charmList;
    // 장착한 부적 리스트
    public List<GameObject> equipList;
    // 코스트 리스트
    public List<GameObject> costList;


    private int maxCost;
    private int currentCost;

    private GameObject cursor = default;
    private int charmNum;

    private GameObject equipObj = default;
    private Charm nowCharm;

    private void Awake()
    {
        // Init Instance
        cursor = GioleFunc.GetRootObj(GioleData.OBJ_NAME_INVENTORYUI).FindChildObj("Cursor");
        equipObj = gameObject.FindChildObj("Equip");


        //// Init Cost List
        //foreach (GameObject obj_ in costList)
        //{
        //    obj_.FindChildObj("Full");
        //}

        currentCost = 0;
        maxCost = 3;
        charmNum = 0;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            --charmNum;
            if (charmNum < 0)
            {
                ++charmNum;
            }
            cursor.transform.position = charmList[charmNum].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ++charmNum;
            if (charmNum > charmList.Count - 1)
            {
                --charmNum;
            }
            cursor.transform.position = charmList[charmNum].transform.position;
        }

        // 부적 장착, 해제
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            nowCharm = charmList[charmNum].GetComponent<Charm>();
            // 장착중이 아닌경우
            if (!nowCharm.equip)
            {
                nowCharm.equip = true;
                CostChange(nowCharm, true);

                equipList.Add(charmList[charmNum]);

                // 여분의 장비 칸 위치 변경
                charmList[charmNum].FindChildObj("Image").transform.position =
                    equipObj.transform.position;

                equipObj.transform.position += Vector3.right * 15f;
            }
            // 장착 중인 경우
            else if (nowCharm.equip)
            {
                nowCharm.equip = false;
                CostChange(nowCharm, false);

                equipList.Remove(charmList[charmNum]);

                // 여분의 장비 칸 위치 변경
                charmList[charmNum].FindChildObj("Image").transform.position =
                    charmList[charmNum].transform.position;
                equipObj.transform.position += Vector3.left * 15f;
            }
            //equipList.Add(charmList[charmNum]);
        }




    }   // Update()


    //private void CostSet()
    //{

    //}


    // 부적의 장비 여부에 따라 코스트 변경
    // changes = true 장착, false 장비 해제
    private void CostChange(Charm charm_, bool changes_)
    {
        for (int i = 0; i < charm_.cost; ++i)
        {
            // 장착
            if (changes_)
            {
                ++currentCost;
                costList[currentCost - 1].FindChildObj("Full").SetActive(true);
                //costList[i].FindChildObj("Full").SetActive(true);
            }
            // 해제
            else if (!changes_)
            {
                --currentCost;
                costList[currentCost].FindChildObj("Full").SetActive(false);
                //costList[i].FindChildObj("Full").SetActive(false);
            }
        }




        //int cost_ = charm_.cost;

        //for (int i = 0; i < cost_; ++i)
        //{
        //}
    }
}
