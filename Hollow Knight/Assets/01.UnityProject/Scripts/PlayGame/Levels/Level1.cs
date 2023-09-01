using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SingletonManager.Instance.AllCoinSet(false);
    }
}
