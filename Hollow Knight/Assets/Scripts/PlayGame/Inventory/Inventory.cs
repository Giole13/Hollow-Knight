using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private GameObject playerObj = default;

    private void Awake()
    {
        playerObj = GioleFunc.GetRootObj("Player");
        gameObject.SetActive(false);
    }

    void Update()
    {
        //Debug.Log("[Inventory] Update!!");

        //if (playerObj.activeSelf)
        //{
        //    Debug.Log("[Inventory] Update : KeyDown Tab");
        //    // â�� ���������� ����
        //    if (Input.GetKeyDown(KeyCode.Tab) && !activeWindow)
        //    {
        //        activeWindow = true;
        //        gameObject.SetActive(true);
        //    }
        //    // â�� ���������� ����
        //    else if (Input.GetKeyDown(KeyCode.Tab) && activeWindow)
        //    {
        //        activeWindow = false;
        //        gameObject.SetActive(false);

        //    }
        //}
    }
}
