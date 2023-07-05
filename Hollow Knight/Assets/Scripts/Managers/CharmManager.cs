using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmManager : MonoBehaviour
{
    // ������ ���� ����Ʈ
    public List<GameObject> charmList;
    // ������ ���� ����Ʈ
    public List<GameObject> equipList;
    // �ڽ�Ʈ ����Ʈ
    public List<GameObject> costList;


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

        // ���� ����, ����
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            nowCharm = charmList[charmNum].GetComponent<Charm>();
            // �������� �ƴѰ��
            if (!nowCharm.equip)
            {
                nowCharm.equip = true;
                CostChange(nowCharm, true);

                equipList.Add(charmList[charmNum]);

                // ������ ��� ĭ ��ġ ����
                charmList[charmNum].FindChildObj("Image").transform.position =
                    equipObj.transform.position;

                equipObj.transform.position += Vector3.right * 15f;
            }
            // ���� ���� ���
            else if (nowCharm.equip)
            {
                nowCharm.equip = false;
                CostChange(nowCharm, false);

                equipList.Remove(charmList[charmNum]);

                // ������ ��� ĭ ��ġ ����
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


    // ������ ��� ���ο� ���� �ڽ�Ʈ ����
    // changes = true ����, false ��� ����
    private void CostChange(Charm charm_, bool changes_)
    {
        for (int i = 0; i < charm_.cost; ++i)
        {
            // ����
            if (changes_)
            {
                ++currentCost;
                costList[currentCost - 1].FindChildObj("Full").SetActive(true);
                //costList[i].FindChildObj("Full").SetActive(true);
            }
            // ����
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
