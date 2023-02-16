using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Box : MonoBehaviour
{
    private GameObject _Level1 = default;

    void Awake()
    {
    }

    private void OnEnable()
    {
        _Level1 = GioleFunc.GetRootObj("Level1");
    }

    // 다음 맵으로 이동하는 박스를 만났을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            collision.gameObject.transform.localPosition =
            _Level1.gameObject.FindChildObj("Level2_Box").transform.position +
            new Vector3(-2f, 0f, 0f);
            transform.parent.gameObject.SetActive(false);
            _Level1.SetActive(true);
        }
    }
}
