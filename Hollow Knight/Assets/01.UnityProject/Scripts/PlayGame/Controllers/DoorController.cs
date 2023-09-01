using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorController : MonoBehaviour
{
    public GameObject nextDoor = default;

    public bool reverse = false;
    public bool vertical = false;

    private const float ARRIVALTIME = 0.5f;
    private const float LERPDISTANCE = 1f;
    private float speed;

    private void Awake()
    {
        this.gameObject.layer = 7;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            // 다음 문의 부모 오브젝트 켜주기
            nextDoor.transform.parent.gameObject.SetActive(true);
            StartCoroutine(MovePlayer(collision));
        }
    }

    // 플레이어 이동 로직
    IEnumerator MovePlayer(Collider2D player_)
    {
        Rigidbody2D rb_ = player_.GetComponent<Rigidbody2D>();
        PlayerController playerScript_ = player_.GetComponent<PlayerController>();
        speed = playerScript_.speed;
        playerScript_.enabled = false;

        if (!vertical)
        {
            if (reverse == false)
            {
                player_.transform.position = new Vector2(nextDoor.transform.position.x + 1f, nextDoor.transform.position.y);
                rb_.velocity = Vector2.right * speed;
                yield return new WaitForSecondsRealtime(ARRIVALTIME);
            }
            else if (reverse == true)
            {
                player_.transform.position = new Vector2(nextDoor.transform.position.x - 1f, nextDoor.transform.position.y);
                rb_.velocity = Vector2.left * speed;
                yield return new WaitForSecondsRealtime(ARRIVALTIME);
            }
        }
        else if (vertical)
        {
            // 수직이동이 켜져있고 반대로 설정 되어 있지 않다면
            if (!reverse)
            {
                // 내려가는 로직
                player_.transform.position = new Vector2(nextDoor.transform.position.x, nextDoor.transform.position.y - 1f);
                rb_.velocity = Vector2.down * 8f;
                yield return new WaitForSecondsRealtime(ARRIVALTIME);
            }
            else if (reverse)
            {
                // 올라가는 로직
                player_.transform.position = new Vector2(nextDoor.transform.position.x, nextDoor.transform.position.y + 1f);

                switch (playerScript_.PlayerViewHorizontal)
                {
                    case PlayerViewDir.RIGHT:
                        rb_.velocity = Vector2.up * 13f + Vector2.right * 8;
                        break;
                    case PlayerViewDir.LEFT:
                        rb_.velocity = Vector2.up * 13f + Vector2.left * 8;
                        break;
                }
                yield return new WaitForSecondsRealtime(ARRIVALTIME);
            }
        }

        playerScript_.enabled = true;
        transform.parent.gameObject.SetActive(false);
    }
}
