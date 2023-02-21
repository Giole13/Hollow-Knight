using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjsManger : MonoBehaviour
{
    // UI 의 전체를 담당하는 UIManager 스크립트

    private GameObject originalHp = default;

    private Stack<GameObject> hpStack = default;

    

    void Start()
    {
        
        hpStack = new Stack<GameObject>();

        originalHp = gameObject.FindChildObj("PlayerHp_");
        originalHp.SetActive(false);

        for(int x = 1; x <= 5; x++)
        {
            MakeHpIcon(x);
        }

    }

    /// <summary>
    /// 플레이어의 체력을 만드는 함수
    /// </summary>
    private void MakeHpIcon(int index)
    {
        GameObject hp_ = Instantiate(originalHp, originalHp.transform.parent);
        hpStack.Push(hp_);

        Animator hp_Ani_ = hp_.transform.GetComponent<Animator>();

        Vector2 nextPosition = hp_.GetRect().anchoredPosition;
        hp_.AddAnchoredPos(new Vector2(50f, 0f));
        hp_.name = "PlayerHp " + index;
        hp_.SetActive(true);
        originalHp = hp_;
    }


    /// <summary>
    /// 데미지를 받아서 체력이 깎임
    /// </summary>
    public void DamageHpIcon()
    {
        GameObject hp_ = hpStack.Pop();

        Animator hp_Ani_ = hp_.transform.GetComponent<Animator>();
        hp_Ani_.SetBool("Damage", true);
    }








}
