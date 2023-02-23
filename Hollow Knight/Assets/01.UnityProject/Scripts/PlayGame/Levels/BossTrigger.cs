using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // �÷��̾ ���⿡ ������ �ߵ�
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            bossDoor.SetActive(true);
            // �ڽ� �ݶ��̴� ���ֱ�
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(BossAppearance());
        }
    }


    // ���� ���� �ڷ�ƾ
    IEnumerator BossAppearance()
    {
        PlayerController player_ = GioleFunc.GetRootObj("Player").GetComponent<PlayerController>();
        player_.PlayerVeloCityStop();
        player_.enabled = false;
        yield return new WaitForSecondsRealtime(2f);
        bossObj.SetActive(true);
        player_.enabled = true;
    }

    // ���� �� �� ���ִ� �Լ� ����� public ����
    public void BossKill()
    {
        if (bossDoor != null)
        {
            bossDoor.SetActive(false);
        }
    }


}
