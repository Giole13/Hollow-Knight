using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private GameObject bossDoor = default;
    private GameObject hornet = default;

    void Start()
    {
    }


    private void OnEnable()
    {
        bossDoor = transform.parent.gameObject.FindChildObj("BossDoor");
        hornet = GioleFunc.GetRootObj("Level4").FindChildObj("Hornet");
        bossDoor.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 여기에 닿으면 발동
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            bossDoor.SetActive(true);
            // 박스 콜라이더 꺼주기
            BoxCollider2D box2D = GetComponent<BoxCollider2D>();
            box2D.enabled = false;
            StartCoroutine(BossAppearance());
        }
    }


    // 보스 등장 코루틴
    IEnumerator BossAppearance()
    {
        PlayerController player_ = GioleFunc.GetRootObj("Player").GetComponent<PlayerController>();
        player_.PlayerVeloCityStop();
        player_.enabled = false;
        hornet.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        player_.enabled = true;
    }

    // 보스 방 문 꺼주는 함수 만들기 public 으로
    public void BossKill()
    {
        if (bossDoor != null)
        {
            bossDoor.SetActive(false);
        }
    }


}
