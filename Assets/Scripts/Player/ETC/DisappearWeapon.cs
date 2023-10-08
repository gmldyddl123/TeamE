using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisappearWeapon : MonoBehaviour
{
    public float timer = 3.0f;
    private void OnEnable()
    {
        StartCoroutine(timerDisapper());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator timerDisapper()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
