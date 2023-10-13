using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    private int money = 0;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();

    // �ִϸ��̼ǿ� ����� ������
    public float fadeDuration = 2.0f;
    public Vector3 textRiseOffset = new Vector3(0, 60, 0);
    private Queue<string> textQueue = new Queue<string>(); // �޽����� ���� ť
    private bool isProcessingText = false; // ���� �ؽ�Ʈ ó�� ������ Ȯ���ϴ� �÷���
    public Transform acquiredTextParent; // ȹ���� ������ �ؽ�Ʈ���� ���� �θ� Ʈ������
    public delegate void OnItemChanged(ItemData _item);

    public OnItemChanged onSwordUpSlotItemChanged;
    public OnItemChanged onSwordItemChanged;
    public OnItemChanged onUpMaterialItemChanged;
    public OnItemChanged onFoodItemChanged;
    public OnItemChanged onImportantItemChanged;
    public OnItemChanged onArtifactItemChanged;
    public OnItemChanged onFoodItemRemoved;
    public OnItemChanged onExItemRemoved;

    public TextMeshProUGUI DropViewText;

    public Action onClearslot;

    /// <summary>
    /// �κ��丮 ��
    /// </summary>
    public GameObject inventoryTap;

    public GameObject sort;
    public GameObject sortTap;

    public bool activeInven = false;

    public bool onsortTap = false;

    private void Start()
    {
        inventoryTap.SetActive(activeInven);
    }
    void Awake()
    {
        instance = this;
    }

    private void OnInven(InputAction.CallbackContext _)
    {
        activeInven = !activeInven;
        inventoryTap.SetActive(activeInven);
    }
    public void SortInventoryByGradeUp()
    {
        onClearslot?.Invoke(); // ��� ������ �������� Ŭ�����մϴ�.

        // ������ ������ ����
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        // �������� �������մϴ�.
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        // ��� ������ ���� ���¸� �����մϴ�.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // ������ �������̸� ������ ���� ���¸� true�� �����մϴ�.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // ���� ������ ���� ǥ���ڸ� Ȱ��ȭ�մϴ�.
                item.CurrentSlot = currentSlot; // �������� ���� ������ ������Ʈ�մϴ�.
            }

            onSwordItemChanged?.Invoke(item); // �ʿ��� ��� �߰� �̺�Ʈ ó��
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI ������Ʈ
    }



    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke(); // ��� ������ �������� Ŭ�����մϴ�.

        // ������ ������ ����
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();

        // ��� ������ ���� ���¸� �����մϴ�.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // ������ �������̸� ������ ���� ���¸� true�� �����մϴ�.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // ���� ������ ���� ǥ���ڸ� Ȱ��ȭ�մϴ�.
                item.CurrentSlot = currentSlot; // �������� ���� ������ ������Ʈ�մϴ�.
            }

            onSwordItemChanged?.Invoke(item); // �ʿ��� ��� �߰� �̺�Ʈ ó��
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI ������Ʈ
    }
    public void SortInventoryByName()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => item.named).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByEquipped()
    {
        onClearslot?.Invoke();

        // ���� ���¿� ���� eqItems�� �����մϴ�.
        eqItems = eqItems.OrderByDescending(item => item.isEquippedItem).ToList();

        // ���ĵ� eqItems ����Ʈ�� �� �����ۿ� ���� �̺�Ʈ�� ȣ���մϴ�.
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void RemoveItems(int itemId, int count)
    {
        List<ItemData> itemsToRemove = new List<ItemData>();
        int removedCount = 0;

        foreach (ItemData item in exItems)
        {
            if (item.id == itemId)
            {
                removedCount++;
                itemsToRemove.Add(item);
                if (removedCount == count)
                {
                    break;
                }
            }
        }
        foreach (ItemData item in itemsToRemove)
        {
            exItems.Remove(item);
            if(item.itemType == ItemType.Food)
            {
                onFoodItemRemoved?.Invoke(item);
            }
            if (item.itemType == ItemType.UpMaterial)
            {
                onExItemRemoved?.Invoke(item);
            }
        }
    }

    public void Add(ItemData item)
    {
        textQueue.Enqueue(item.named + " ŉ��");
        if (!isProcessingText)
        {
            StartCoroutine(ShowTextProcess());
        }

        if (item.itemType == ItemType.UpMaterial)
        {
            exItems.Add(item);
            onUpMaterialItemChanged?.Invoke(item);
        }
        else if (item.itemType == ItemType.Sword)
        {

            eqItems.Add(item);
            onSwordItemChanged?.Invoke(item);
            onSwordUpSlotItemChanged?.Invoke(item);
            if (item is Item_WeaponData weaponitem)
                weaponitem.SetAbilities(); //������ �ɷ�ġ ����
        }
        if (item.itemType == ItemType.Food)
        {
            exItems.Add(item);
            onFoodItemChanged?.Invoke(item);
        }
        if (item.itemType == ItemType.Important)
        {
            exItems.Add(item);
            onImportantItemChanged?.Invoke(item);
        }
        if (item.itemType == ItemType.Artifact)
        {
            exItems.Add(item);
            onArtifactItemChanged?.Invoke(item);
        }
    }
    private IEnumerator ShowTextProcess()
    {
        if (isProcessingText) yield break; // �̹� ó�� ���� ��� �ڷ�ƾ�� �ߴ��մϴ�.
        isProcessingText = true;

        while (textQueue.Count > 0)
        {
            // �ؽ�Ʈ ǥ�ÿ� ���̵带 ó���ϴ� �ڷ�ƾ�� �����մϴ�.
            yield return StartCoroutine(ShowAndFadeText(textQueue.Dequeue()));
        }

        isProcessingText = false; // ��� �ؽ�Ʈ�� ó���Ǿ����Ƿ� �÷��׸� �缳���մϴ�.
    }

    private IEnumerator ShowAndFadeText(string message)
    {
        DropViewText.text = message; // �ؽ�Ʈ ����
        DropViewText.color = new Color(DropViewText.color.r, DropViewText.color.g, DropViewText.color.b, 0); // �ʱ� ���� ���� 0���� ����
        Vector3 originalPosition = DropViewText.rectTransform.localPosition; // ���� ��ġ ����

        float elapsedTime = 0;

        // ���̵� �ΰ� �Բ� �ؽ�Ʈ�� ���� �̵��մϴ�.
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            DropViewText.color = new Color(DropViewText.color.r, DropViewText.color.g, DropViewText.color.b, alpha);
            DropViewText.rectTransform.localPosition += textRiseOffset * (Time.deltaTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ؽ�Ʈ�� ���� ��ġ�� �ٽ� �����ϰ�, ���ĸ� 0���� �����Ͽ� �ؽ�Ʈ�� ����ϴ�.
        DropViewText.rectTransform.localPosition = originalPosition;
        DropViewText.color = new Color(DropViewText.color.r, DropViewText.color.g, DropViewText.color.b, 0);

    }
    public void RemoveItem(ItemData itemToRemove)
    {
        exItems.Remove(itemToRemove);
        onFoodItemRemoved?.Invoke(itemToRemove);
    }
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
   
    public int GetOneOreCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 10)
            {
                count++;
            }
        }
        return count;
    }
    public int GetTwoOreCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 11)
            {
                count++;
            }
        }
        return count;
    }
    public int GetOneThreesCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 12)
            {
                count++;
            }
        }
        return count;
    }
}