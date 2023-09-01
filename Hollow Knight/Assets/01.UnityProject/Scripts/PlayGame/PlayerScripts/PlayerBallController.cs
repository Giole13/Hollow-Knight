using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.transform.tag)
        {
            case GioleData.TAG_NAME_MONSTER:        // Attack Monster
                collision.gameObject.GetComponent<MonsterClass>().HitMonster(30);
                break;
            default:
                break;
        }
    }
}
