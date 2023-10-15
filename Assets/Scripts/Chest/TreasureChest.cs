using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    public GameObject[] itemPrefabs; // ���ڿ��� ������ �����۵�
    GameObject lightEffect; // �� ȿ��
    GameObject smokeEffect; // ���� ȿ��

    public float extinctionTime = 2f; // ȿ�� ���� �ð�
    public event Action<IInteractable> OnDestroyed;
    private Animator anim;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    public bool IsDirectUse => true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // �ڽ� ������Ʈ���� ȿ�� ��������
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
        OnDestroyed?.Invoke(this); // �̺�Ʈ �߻�
    }
    IEnumerator HandleEffects()
    {
        lightEffect.SetActive(true); // �� ȿ�� Ȱ��ȭ
        yield return new WaitForSeconds(extinctionTime); // ���� �ð� ���� ���
        smokeEffect.SetActive(true); // ���� ȿ�� �ٽ� Ȱ��ȭ
        yield return new WaitForSeconds(0.3f); // ���� �ð� ���� ���
        lightEffect.SetActive(false); // �� ȿ�� ��Ȱ��ȭ
        ItemSpawn();
        Destroy(gameObject);
    }

    void ItemSpawn()
    {
        Quaternion rotation = Quaternion.identity; // ȸ���� ����

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

