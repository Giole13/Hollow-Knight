using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quirrel : MonoBehaviour
{
    bool setTalkBool = true;
    private bool roopSit = false;

    PlayerController playerCTR = default;


    private void Awake()
    {
        // Set Layer
        this.gameObject.layer = 7;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            roopSit = true;
            playerCTR = collision.GetComponent<PlayerController>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            roopSit = false;
        }
    }



    private void Update()
    {
        // Player in Sit Collider2D
        if (roopSit && Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerCTR.PlayerVeloCityStop();
            // talk Active
            if (setTalkBool)
            {
                setTalkBool = false;
                playerCTR.PlayerTalkNPC(true);
                playerCTR.enabled = false;
            }
            // talk Deactive
            else if (!setTalkBool)
            {
                StartCoroutine(TalkNPC());
            }
        }
    }


    IEnumerator TalkNPC()
    {
        setTalkBool = true;
        playerCTR.PlayerTalkNPC(false);
        yield return new WaitForSeconds(0.5f);
        playerCTR.enabled = true;
    }
}
