using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Box : MonoBehaviour
{

    private GameObject _Level2 = default;

    private void Awake()
    {
        _Level2 = GioleFunc.GetRootObj("Level2");
    }

    // 다음 맵으로 이동하는 박스를 만났을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.localPosition = 
            _Level2.gameObject.FindChildObj("Level1_Box").transform.position + 
            new Vector3(2f, 0f, 0f);
        transform.parent.gameObject.SetActive(false);
        _Level2.SetActive(true);
    }
}
