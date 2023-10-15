using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    public GameObject[] itemPrefabs; // 상자에서 생성될 아이템들
    GameObject lightEffect; // 빛 효과
    GameObject smokeEffect; // 연기 효과

    public float extinctionTime = 2f; // 효과 지속 시간
    public event Action<IInteractable> OnDestroyed;
    private Animator anim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    public bool IsDirectUse => true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // 자식 컴포넌트에서 효과 가져오기
        lightEffect = transform.GetChild(2).gameObject;
        smokeEffect = transform.GetChild(3).gameObject;
    }
    public void Use()
    {
        anim.SetBool(IsOpen, true);
        StartCoroutine(HandleEffects());
    }
    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this); // 이벤트 발생
    }
    IEnumerator HandleEffects()
    {
        lightEffect.SetActive(true); // 빛 효과 활성화
        yield return new WaitForSeconds(extinctionTime); // 지속 시간 동안 대기
        smokeEffect.SetActive(true); // 연기 효과 다시 활성화
        yield return new WaitForSeconds(0.3f); // 지속 시간 동안 대기
        lightEffect.SetActive(false); // 빛 효과 비활성화
        ItemSpawn();
        Destroy(gameObject);
    }

    void ItemSpawn()
    {
        Quaternion rotation = Quaternion.identity; // 회전값 없음

        foreach (var item in itemPrefabs)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + UnityEngine.Random.Range(-1f, 1f),
                2.5f,
                transform.position.z + UnityEngine.Random.Range(-2f, 2f));

            Instantiate(item, spawnPos, rotation);
        }
    }
}

