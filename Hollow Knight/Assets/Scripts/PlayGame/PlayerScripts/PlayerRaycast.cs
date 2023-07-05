using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public LayerMask monsterLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(transform.position, Vector2.right,3f , monsterLayer))
        {
            Debug.DrawRay(transform.position, Vector2.right * 3f, Color.red);
            Debug.Log("[PlayerRaycast] 업데이트 : 광선빔에 맞았따!!");
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.right * 3f, Color.green);
            Debug.Log("[PlayerRaycast] 업데이트 : 아무도 안맞았따!!");
        }
    }
}
