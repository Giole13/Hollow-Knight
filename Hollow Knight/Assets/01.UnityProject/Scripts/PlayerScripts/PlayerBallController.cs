using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    transform.GetComponent<Rigidbody2D>().velocity = Vector3.right * 10f;

    //}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("[PlayerBallController] OntriggerEnter2D : I'm Hit!");
        Debug.Log($"[PlayerBallController] {collision.name}");
        switch (collision.transform.tag)
        {
            case GioleData.TAG_NAME_MONSTER:        // Attack Monster
                collision.gameObject.GetComponent<MonsterClass>().HitMonster(30);
                break;
            default:
                break;
        }
    }

    //IEnumerator Move()
    //{
    //    yield return null;
    //}
}
