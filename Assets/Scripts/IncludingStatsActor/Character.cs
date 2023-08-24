using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    IEnumerator disappearObject;
    float disappearTime = 3.0f;
    bool disappearActive = false;

    private void Awake()
    {
        disappearObject = DisappearObject();
    }

    private void OnEnable()
    {
        disappearActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!disappearActive)
        {
            StartCoroutine(disappearObject);
        }
    }

    IEnumerator DisappearObject()
    {
        disappearActive = true;
        yield return new WaitForSeconds(disappearTime);
        gameObject.SetActive(false);
    }
}
