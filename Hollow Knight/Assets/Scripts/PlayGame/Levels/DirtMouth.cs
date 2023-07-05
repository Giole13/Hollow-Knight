using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMouth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    
    public void OnTriggerEnter2D()
    {
        gameObject.SetActive(true);
        gameObject.SetTmpText("사라져 가는 흙의 마을");
        StartCoroutine(TitleFade());
    }


    IEnumerator TitleFade()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
        yield return null;
    }

}
