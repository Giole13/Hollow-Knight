using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmManager : MonoBehaviour
{
    public List<GameObject> charmList;
    public List<GameObject> equipList;
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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            nowCharm = charmList[charmNum].GetComponent<Charm>();
            if (!nowCharm.equip)
            {
                nowCharm.equip = true;
                CostChange(nowCharm, true);

                equipList.Add(charmList[charmNum]);

                charmList[charmNum].FindChildObj("Image").transform.position =
                    equipObj.transform.position;

                equipObj.transform.position += Vector3.right * 15f;
            }
            else if (nowCharm.equip)
            {
                nowCharm.equip = false;
                CostChange(nowCharm, false);

                equipList.Remove(charmList[charmNum]);

                charmList[charmNum].FindChildObj("Image").transform.position =
                    charmList[charmNum].transform.position;
                equipObj.transform.position += Vector3.left * 15f;
            }
        }
    }   // Update()

    private void CostChange(Charm charm_, bool changes_)
    {
        for (int i = 0; i < charm_.cost; ++i)
        {
            if (changes_)
            {
                ++currentCost;
                costList[currentCost - 1].FindChildObj("Full").SetActive(true);
            }
            else if (!changes_)
            {
                --currentCost;
                costList[currentCost].FindChildObj("Full").SetActive(false);
            }
        }
    }
}
