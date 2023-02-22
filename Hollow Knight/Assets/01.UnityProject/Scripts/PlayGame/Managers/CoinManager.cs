using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // 여기선 코인을 미리 오브젝트 풀링 해놓는 곳

    private List<GameObject> coinList = new List<GameObject>();

    private Rigidbody2D coinRB = default;


    public int coinNum;

    void Awake()
    {
        // Initailize Instance
        GameObject PrefapCoin = gameObject.FindChildObj("Small_Coin");
        GameObject coin_ = default;


        PrefapCoin.SetActive(false);


        for (int i = 1; i < coinNum + 1; ++i)
        {
            coin_ = Instantiate(PrefapCoin, transform);
            coin_.name = "Coin " + i;
            coinList.Add(coin_);
        }


    }


    // 코인 꺼내는 함수
    public void DropCoin(Vector2 centerPos_, int coinNum_)
    {
        int i = 0;
        foreach (GameObject coin in coinList)
        {
            if (!coin.activeSelf)
            {
                coin.SetActive(true);
                coinRB = coin.GetComponent<Rigidbody2D>();
                ++i;
                coin.transform.position = centerPos_;
                StartCoroutine(CoinVelocity(coinRB));
            }
            else
            {
                /* Do nothing */
            }
            if (i == coinNum_) break;

        }
    }


    // 코인에게 힘을 준다.
    IEnumerator CoinVelocity(Rigidbody2D coinRB_)
    {
        int i = Random.Range(-2, 2);
        coinRB.velocity = Vector2.up * 5f + Vector2.right * i;
        yield return new WaitForSeconds(0.1f);
        coinRB.velocity = Vector2.zero;
        yield return null;
    }


    public void AllCoinSetActive(bool set_)
    {
        foreach(GameObject coin in coinList)
        {
            if (coin.activeSelf)
            {
                coin.SetActive(set_);
            }
        }
    }
}
