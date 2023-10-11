using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearWeapon : MonoBehaviour
{
    public float timer = 3.0f;
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(timerDisapper());
    }

    IEnumerator timerDisapper()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
