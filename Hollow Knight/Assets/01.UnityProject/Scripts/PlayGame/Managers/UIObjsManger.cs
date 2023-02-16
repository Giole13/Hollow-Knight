using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjsManger : MonoBehaviour
{
    // UI 의 전체를 담당하는 UIManager 스크립트

    private GameObject original_hp = default;

    void Start()
    {
        original_hp = gameObject.FindChildObj("PlayerHp 1");

        for(int x = 2; x <= 5; x++)
        {
            MakeHpIcon(x);
        }

    }


    void Update()
    {

    }



    /// <summary>
    /// 플레이어의 체력을 만드는 함수
    /// </summary>
    private void MakeHpIcon(int index)
    {
        GameObject hp_ = Instantiate(original_hp, original_hp.transform.parent);

        Vector2 nextPosition = hp_.GetRect().anchoredPosition;
        hp_.AddAnchoredPos(new Vector2(50f, 0f));
        hp_.name = "PlayerHp " + index;
        original_hp = hp_;
    }


}
