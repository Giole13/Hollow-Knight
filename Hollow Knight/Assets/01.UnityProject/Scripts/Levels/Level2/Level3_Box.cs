using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Box : MonoBehaviour
{
    // ���� ������ �̵��ϴ� �ڽ��� ������ ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            GameObject _Level3 = GioleFunc.GetRootObj("Level3");
            collision.gameObject.transform.localPosition =
            _Level3.gameObject.FindChildObj("Level2_Box").transform.position +
            new Vector3(2f, 0f, 0f);
            transform.parent.gameObject.SetActive(false);
            _Level3.SetActive(true);
        }
    }
}
