using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private bool setSitAni = true;
    private bool roopSit = false;

    public string thisareaName;


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
            // Sit Active, Save Data
            if (setSitAni)
            {
                setSitAni = false;
                playerCTR.PlayerSitChair(true);
                playerCTR.enabled = false;
                SaveData();
            }
            // Sit Deactive
            else if (!setSitAni)
            {
                StartCoroutine(GetUpChair());
            }
        }
    }

    IEnumerator GetUpChair()
    {
        setSitAni = true;
        playerCTR.PlayerSitChair(false);
        yield return new WaitForSeconds(0.5f);
        playerCTR.enabled = true;
    }


    // Sited to Saving Data
    private void SaveData()
    {
        // Player Save
        DataManager.Instance.nowPlayer.playerObj =
            playerCTR.gameObject;
        // Level Save
        DataManager.Instance.nowPlayer.levelObj = 
            transform.parent.gameObject;
        // Present Aera Name Save
        DataManager.Instance.nowPlayer.areaName =
            thisareaName;
        // Present Player Coin Save
        DataManager.Instance.nowPlayer.coin =
            playerCTR.GetPlayerPresentCoin();
        // Player Pos Save
        DataManager.Instance.nowPlayer.playerPos =
            playerCTR.transform.position;

        // File Save
        DataManager.Instance.SaveData();
    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("[Chair] PlayerSit : Stay!!");

    //    bool sitSetBool = false;
    //    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
    //    {
    //        sitSetBool = !sitSetBool;
    //        Debug.Log("[Chair] PlayerSit : I'm Sited");
    //    }


    //    if (sitSetBool)
    //    {
    //        Debug.Log("[Chair] PlayerSit : I'm Sited");
    //    }
    //    else if (!sitSetBool)
    //    {
    //        Debug.Log("[Chair] PlayerSit : I'm not Sited");
    //    }
    //}



    //IEnumerator PlayerSit(PlayerController playerSC)
    //{
    //    //while (roopSit)
    //    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        if (setSitAni)
    //        {
    //            setSitAni = false;
    //            //playerSC.PlayerSitChair(true);
    //            //playerSC.enabled = false;
    //            Debug.Log("[Chair] PlayerSit : I'm Sited");
    //            break;
    //        }
    //    }

    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        if (setSitAni)
    //        {
    //            setSitAni = false;
    //            Debug.Log("[Chair] PlayerSit : I'm not Sited");
    //            //playerSC.PlayerSitChair(false);
    //            //playerSC.enabled = true;
    //            break;
    //        }

    //    }
    //    //}
    //}
}
