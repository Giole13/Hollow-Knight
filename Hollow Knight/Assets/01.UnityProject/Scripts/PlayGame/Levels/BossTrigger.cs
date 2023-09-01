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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            bossDoor.SetActive(true);
            CameraManager cM_ = GioleFunc.GetRootObj("PlayerCamera").GetComponent<CameraManager>();
            cM_.CSHandler = CameraState.BOSS;
            cM_.BossFightView(gameObject.FindChildObj("Center"));
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(BossAppearance());
        }
    }

    IEnumerator BossAppearance()
    {
        PlayerController player_ = GioleFunc.GetRootObj("Player").GetComponent<PlayerController>();
        player_.PlayerVeloCityStop();
        player_.enabled = false;
        bossObj.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        player_.enabled = true;
    }

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
