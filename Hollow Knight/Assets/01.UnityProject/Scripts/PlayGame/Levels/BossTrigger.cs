using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BossTrigger : MonoBehaviour
{
    private GameObject bossDoor = default;
    public GameObject bossObj = default;


    private void OnEnable()
    {
        bossDoor = transform.parent.gameObject.FindChildObj("BossDoor");
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
            CameraManager cM_ = GioleFunc.GetRootObj("PlayerCamera").GetComponent<CameraManager>();
            cM_.CSHandler = CameraState.BOSS;
            cM_.BossFightView(gameObject.FindChildObj("Center"));
            // 박스 콜라이더 꺼주기
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(BossAppearance());
        }
    }


    // 보스 등장 코루틴
    IEnumerator BossAppearance()
    {
        PlayerController player_ = GioleFunc.GetRootObj("Player").GetComponent<PlayerController>();
        player_.PlayerVeloCityStop();
        player_.enabled = false;
        bossObj.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        player_.enabled = true;
    }

    // 보스 방 문 꺼주는 함수 만들기 public 으로
    public void BossKill()
    {
        if (bossDoor != null)
        {
            CameraManager cM_ = GioleFunc.GetRootObj("PlayerCamera").GetComponent<CameraManager>();
            cM_.CSHandler = CameraState.NORMAL;
            bossDoor.SetActive(false);
        }
    }


}
