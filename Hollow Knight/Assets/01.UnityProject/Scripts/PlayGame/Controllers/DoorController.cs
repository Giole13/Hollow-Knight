using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject nextDoor = default;

    public bool reverse = false;

    private const float WAITTIME = 0.02f;

    private void Awake()
    {
        // 인스턴스 초기화
        //l2LDoor = GioleFunc.GetRootObj("Level2").FindChildObj("L2LDoor");



        // 인스턴스 설정
    }


    // 다음 맵으로 이동하는 박스를 만났을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            //collision.gameObject.transform.position =
            //    Vector3.MoveTowards(l2LDoor.transform.position,
            //    l2LDoor.transform.position + new Vector3(6f, 0f, 0f), 0.1f);

            //l2LDoor.transform.position +
            //new Vector3(2f, 0f, 0f);

            nextDoor.transform.parent.gameObject.SetActive(true);

            StartCoroutine(MovePlayer(collision));
        }
    }


    IEnumerator MovePlayer(Collider2D player_)
    {
        bool finish = true;
        float distance = 0f;
        if (reverse == false)
        {
            //player_.transform.position = nextDoor.transform.position;
            while (finish)
            {
                player_.transform.position = Vector2.Lerp(
                    new Vector2(nextDoor.transform.position.x + 1f, player_.transform.position.y),
                    new Vector2(nextDoor.transform.position.x + 3f, player_.transform.position.y),
                    distance);
                //player_.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                distance += WAITTIME;
                yield return new WaitForSecondsRealtime(WAITTIME);

                if (1 < distance) finish = false;
            }
        }
        else if (reverse == true)
        {
            //player_.transform.position = nextDoor.transform.position;
            while (finish)
            {
                player_.transform.position = Vector2.Lerp(
                new Vector2(nextDoor.transform.position.x - 1f, player_.transform.position.y),
                new Vector2(nextDoor.transform.position.x + -3f, player_.transform.position.y),
                distance);
                //player_.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                distance += WAITTIME;
                yield return new WaitForSecondsRealtime(WAITTIME);

                if (1 < distance) finish = false;
            }
        }
        transform.parent.gameObject.SetActive(false);
    }
}
