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
}
