using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private bool setSitAni = false;
    private bool roopSit = true;



    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow) ||
        //        Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    setSitAni = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        //{
        //    PlayerController playerScript = collision.GetComponent<PlayerController>();
        //    StartCoroutine(PlayerSit(playerScript));
        //}
        Debug.Log("[Chair] PlayerSit : Enter!!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.transform.tag.Equals(GioleData.TAG_NAME_PLAYERBODY))
        //{
        //    //PlayerController playerScript = collision.GetComponent<PlayerController>();
        //    //StopCoroutine(PlayerSit(playerScript));
        //}
        Debug.Log("[Chair] PlayerSit : Exit!!");
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("[Chair] PlayerSit : Stay!!");

        bool sitSetBool = false;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            sitSetBool = !sitSetBool;
            Debug.Log("[Chair] PlayerSit : I'm Sited");
        }


        if (sitSetBool)
        {
            Debug.Log("[Chair] PlayerSit : I'm Sited");
        }
        else if (!sitSetBool)
        {
            Debug.Log("[Chair] PlayerSit : I'm not Sited");
        }
    }
    IEnumerator PlayerSit(PlayerController playerSC)
    {
        //while (roopSit)
        //{
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (setSitAni)
            {
                setSitAni = false;
                //playerSC.PlayerSitChair(true);
                //playerSC.enabled = false;
                Debug.Log("[Chair] PlayerSit : I'm Sited");
                break;
            }
        }

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (setSitAni)
            {
                setSitAni = false;
                Debug.Log("[Chair] PlayerSit : I'm not Sited");
                //playerSC.PlayerSitChair(false);
                //playerSC.enabled = true;
                break;
            }

        }
        //}
    }
}
