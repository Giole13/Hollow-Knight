using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject _PlayerObj = default;

    private void Awake()
    {
        // �÷��̾� �ʱ�ȭ
        _PlayerObj = GioleFunc.GetRootObj(GioleData.OBJ_NAME_PLAYER);
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }




    public void ActivePlayer()
    {
        _PlayerObj.SetActive(true);
    }
}
