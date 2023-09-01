using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private List<GameObject> coinList = new List<GameObject>();

    private Rigidbody2D coinRB = default;

    private int poolSize = 100;

    void Awake()
    {
        // Initailize Instance
        GameObject PrefapCoin = gameObject.FindChildObj("Small_Coin");
        GameObject coin_;

        PrefapCoin.SetActive(false);
        for (int i = 1; i < poolSize + 1; ++i)
        {
            Debug.Log("코인생성");
            coin_ = Instantiate(PrefapCoin, transform);
            coin_.name = "Coin " + i;
            coinList.Add(coin_);
        }
    }

    public void DropCoin(Vector2 centerPos_, int coinNum_)
    {
        int i = 0;
        foreach (GameObject coin in coinList)
        {
            if (!coin.activeSelf)
            {
                ++i;
                coin.SetActive(true);
                coinRB = coin.GetComponent<Rigidbody2D>();
                coin.transform.position = centerPos_;
                StartCoroutine(CoinVelocity(coinRB));
            }

            if (i == coinNum_) { break; }
        }
    }

    IEnumerator CoinVelocity(Rigidbody2D coinRB_)
    {
        int i = Random.Range(-2, 2);
        float i2 = Random.Range(0f, 1f);
        int i3 = Random.Range(3, 6);
        coinRB.velocity = Vector2.up * (i3 + i2) + Vector2.right * (i + i2);
        yield return new WaitForSeconds(0.1f);
        coinRB.velocity = Vector2.zero;
        yield return null;
    }

    public void AllCoinSetActive(bool set_)
    {
        foreach (GameObject coin in coinList)
        {
            if (coin.activeSelf)
            {
                coin.SetActive(set_);
            }
        }
    }
}
