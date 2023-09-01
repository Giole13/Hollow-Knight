using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMouth : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D()
    {
        gameObject.SetActive(true);
        gameObject.SetTmpText("����� ���� ���� ����");
        StartCoroutine(TitleFade());
    }

    IEnumerator TitleFade()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
        yield return null;
    }

}
