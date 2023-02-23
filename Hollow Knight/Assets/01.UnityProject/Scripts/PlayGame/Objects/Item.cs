using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool setPickUpBool = true;
    private bool roopSit = false;

    PlayerController playerCTR = default;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("[Item] OnCollisionEnter2D : Collision On!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("[Item] OntriggerEnter2D : trigger On!");
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            roopSit = true;
            playerCTR = collision.GetComponent<PlayerController>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("[Item] OntriggerEnter2D : trigger Off!");
        if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        {
            roopSit = false;
            
        }
    }



    private void Update()
    {
        // Player in Item Collider2D
        if (roopSit && Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerCTR.PlayerVeloCityStop();
            // Pick Up Active
            if (setPickUpBool)
            {
                setPickUpBool = false;
                playerCTR.PlayerPickUpItem(true);
                playerCTR.enabled = false;
            }
            // Pick Up Deactive
            else if (!setPickUpBool)
            {
                StartCoroutine(PickUp());
            }
        }
    }


    IEnumerator PickUp()
    {
        setPickUpBool = true;
        playerCTR.PlayerPickUpItem(false);
        yield return new WaitForSeconds(0.5f);
        playerCTR.enabled = true;
    }
}
