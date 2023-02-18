using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject nextDoor = default;

    public bool reverse = false;

    private const float ARRIVALTIME = 0.5f;
    private const float LERPDISTANCE = 1f;

    private void Awake()
    {
        // �ν��Ͻ� �ʱ�ȭ
        //l2LDoor = GioleFunc.GetRootObj("Level2").FindChildObj("L2LDoor");



        // �ν��Ͻ� ����
    }


    // ���� ������ �̵��ϴ� �ڽ��� ������ ��
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
        Rigidbody2D rb_ = player_.GetComponent<Rigidbody2D>();
        PlayerController playerScript_ = player_.GetComponent<PlayerController>();
        playerScript_.enabled = false;
        if (reverse == false)
        {
            player_.transform.position = new Vector2(nextDoor.transform.position.x + 1f, nextDoor.transform.position.y);

            // player_.transform.position = Vector2.Lerp(
            //     new Vector2(nextDoor.transform.position.x + 1f, player_.transform.position.y),
            //     new Vector2(nextDoor.transform.position.x + 3f, player_.transform.position.y),
            //     distance);

            //player_.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;

            // distance += LERPDISTANCE;
            yield return new WaitForSecondsRealtime(ARRIVALTIME);
            rb_.velocity = Vector2.right * 7f;
            // if (1 < distance) finish = false;


        }
        else if (reverse == true)
        {
            //player_.transform.position = nextDoor.transform.position;

            // player_.transform.position = Vector2.Lerp(
            // new Vector2(nextDoor.transform.position.x - 1f, player_.transform.position.y),
            // new Vector2(nextDoor.transform.position.x + -3f, player_.transform.position.y),
            // distance);
            //player_.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;
            // distance += LERPDISTANCE;
            // if (1 < distance) finish = false;

            player_.transform.position = new Vector2(nextDoor.transform.position.x - 1f, nextDoor.transform.position.y);
            rb_.velocity = Vector2.left * 7f;
            yield return new WaitForSecondsRealtime(ARRIVALTIME);
        }
        playerScript_.enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
