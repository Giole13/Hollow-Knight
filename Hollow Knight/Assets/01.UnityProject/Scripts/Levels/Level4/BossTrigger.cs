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
        // �÷��̾ ���⿡ ������ �ߵ�
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            bossDoor.SetActive(true);
            // �ڽ� �ݶ��̴� ���ֱ�
            BoxCollider2D box2D = GetComponent<BoxCollider2D>();
            box2D.enabled = false;
            StartCoroutine(BossAppearance());
        }
    }


    // ���� ���� �ڷ�ƾ
    IEnumerator BossAppearance()
    {
        PlayerController player_ = GioleFunc.GetRootObj("Player").GetComponent<PlayerController>();
        player_.PlayerVeloCityStop();
        player_.enabled = false;
        hornet.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
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
