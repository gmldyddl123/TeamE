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

    // 애니메이션에 사용할 변수들
    public float fadeDuration = 2.0f;
    public Vector3 textRiseOffset = new Vector3(0, 60, 0);
    private Queue<string> textQueue = new Queue<string>(); // 메시지를 담을 큐
    private bool isProcessingText = false; // 현재 텍스트 처리 중인지 확인하는 플래그
    public Transform acquiredTextParent; // 획득한 아이템 텍스트들을 위한 부모 트랜스폼
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
    /// 인벤토리 탭
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
        onClearslot?.Invoke(); // 모든 슬롯의 아이템을 클리어합니다.

        // 장착된 아이템 추적
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        // 아이템을 재정렬합니다.
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        // 모든 슬롯의 장착 상태를 해제합니다.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // 장착된 아이템이면 슬롯의 장착 상태를 true로 설정합니다.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // 현재 슬롯의 장착 표시자를 활성화합니다.
                item.CurrentSlot = currentSlot; // 아이템의 현재 슬롯을 업데이트합니다.
            }

            onSwordItemChanged?.Invoke(item); // 필요한 경우 추가 이벤트 처리
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI 업데이트
    }



    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke(); // 모든 슬롯의 아이템을 클리어합니다.

        // 장착된 아이템 추적
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();

        // 모든 슬롯의 장착 상태를 해제합니다.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // 장착된 아이템이면 슬롯의 장착 상태를 true로 설정합니다.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // 현재 슬롯의 장착 표시자를 활성화합니다.
                item.CurrentSlot = currentSlot; // 아이템의 현재 슬롯을 업데이트합니다.
            }

            onSwordItemChanged?.Invoke(item); // 필요한 경우 추가 이벤트 처리
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI 업데이트
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

        // 장착 상태에 따라 eqItems를 정렬합니다.
        eqItems = eqItems.OrderByDescending(item => item.isEquippedItem).ToList();

        // 정렬된 eqItems 리스트의 각 아이템에 대해 이벤트를 호출합니다.
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
        textQueue.Enqueue(item.named + " 흭득");
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
                weaponitem.SetAbilities(); //랜덤한 능력치 설정
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
        if (isProcessingText) yield break; // 이미 처리 중인 경우 코루틴을 중단합니다.
        isProcessingText = true;

        while (textQueue.Count > 0)
        {
            // 텍스트 표시와 페이드를 처리하는 코루틴을 시작합니다.
            yield return StartCoroutine(ShowAndFadeText(textQueue.Dequeue()));
        }

        isProcessingText = false; // 모든 텍스트가 처리되었으므로 플래그를 재설정합니다.
    }

    private IEnumerator ShowAndFadeText(string message)
    {
        DropViewText.text = message; // 텍스트 설정
        DropViewText.color = new Color(DropViewText.color.r, DropViewText.color.g, DropViewText.color.b, 0); // 초기 알파 값을 0으로 설정
        Vector3 originalPosition = DropViewText.rectTransform.localPosition; // 원래 위치 저장

        float elapsedTime = 0;

        // 페이드 인과 함께 텍스트를 위로 이동합니다.
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            DropViewText.color = new Color(DropViewText.color.r, DropViewText.color.g, DropViewText.color.b, alpha);
            DropViewText.rectTransform.localPosition += textRiseOffset * (Time.deltaTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 텍스트를 원래 위치로 다시 설정하고, 알파를 0으로 설정하여 텍스트를 숨깁니다.
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