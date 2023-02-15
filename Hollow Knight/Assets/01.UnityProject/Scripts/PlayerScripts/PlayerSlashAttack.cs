using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashAttack : MonoBehaviour
{
    // 플레이어 기본공격 스크립트
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 공격이 적중했을때
        switch (collision.transform.tag)
        {
            case GioleData.TAG_NAME_MONSTER:        // 몬스터를 공격했을때
                collision.gameObject.SetActive(false);
                break;
        }
    }
}
