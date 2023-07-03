using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ð� ������ ���ִ� ������Ʈ
/// </summary>
public class Disappear : MonoBehaviour
{
    [SerializeField] float disappearTime;

    void OnEnable()
    {
        StartCoroutine(DisappearCoroutine());
    }

    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(disappearTime);

        gameObject.SetActive(false);
    }
}
