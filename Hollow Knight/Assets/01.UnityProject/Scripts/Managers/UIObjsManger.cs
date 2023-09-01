using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjsManger : MonoBehaviour
{
    private GameObject originalHp = default;
    private GameObject coinNumObj = default;
    private Stack<GameObject> hpStack = default;
    private int coinInt = 0;

    void Start()
    {
        coinNumObj = gameObject.FindChildObj("CoinNum");
        hpStack = new Stack<GameObject>();
        originalHp = gameObject.FindChildObj("PlayerHp_");
        originalHp.SetActive(false);

        for (int x = 1; x <= 5; x++)
        {
            MakeHpIcon(x);
        }
    }

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

    public void DamageHpIcon()
    {
        GameObject hp_ = hpStack.Pop();
        Animator hp_Ani_ = hp_.transform.GetComponent<Animator>();
        hp_Ani_.SetBool("Damage", true);
    }

    public void CoinNumPlus(string coinName)
    {
        int i = 1;
        switch (coinName)
        {
            case "Small":
                i = 1;
                break;
            case "Middle":
                i = 5;
                break;
            case "Big":
                i = 15;
                break;
            default:
                break;
        }
        coinInt += i;
        coinNumObj.SetTmpText($"{coinInt}");
    }

    public int GetCoin()
    {
        return coinInt;
    }




}
