using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Box : MonoBehaviour
{
    protected DirtMouth dirtMouthTitle;
    private GameObject _Level2 = default;

    private void Awake()
    {
        // 인스턴스 초기화
        _Level2 = GioleFunc.GetRootObj("Level2");
        dirtMouthTitle = GioleFunc.GetRootObj(GioleData.OBJ_NAME_UIOBJS).FindChildObj("DirtMouthTitle").
            GetComponent<DirtMouth>();



        // 인스턴스 설정
    }

    // 다음 맵으로 이동하는 박스를 만났을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {

            collision.gameObject.transform.position =
                _Level2.gameObject.FindChildObj("Level1_Box").transform.position +
                new Vector3(2f, 0f, 0f);

            dirtMouthTitle.PopUpTitle();
            transform.parent.gameObject.SetActive(false);
            _Level2.SetActive(true);
        }
    }
}
