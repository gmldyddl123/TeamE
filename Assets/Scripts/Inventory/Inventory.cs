using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();

    public delegate void OnItemChanged(ItemData _item);

    public OnItemChanged onSwordItemChanged;
    public OnItemChanged onUpMaterialItemChanged;
    public OnItemChanged onFoodItemChanged;
    public OnItemChanged onImportantItemChanged;
    public OnItemChanged onArtifactItemChanged;

    public Action onClearslot;

    InventoryInputAction actions;

    /// <summary>
    /// 인벤토리 탭
    /// </summary>
    public GameObject inventoryTap;
    public GameObject sortTap;

    public bool activeInven = false;
    bool onsortTap = false;

    private void Start()
    {
        inventoryTap.SetActive(activeInven);
    }
    void Awake()
    {
        instance = this;
        actions = new();
    }
    private void OnEnable()
    {
        actions.OpneInven.Enable();
        actions.OpneInven.Inven.performed += OnInven;
    }
    private void OnDisable()
    {
        actions.OpneInven.Disable();
    }

    private void OnInven(InputAction.CallbackContext _)
    {
        activeInven = !activeInven;
        inventoryTap.SetActive(activeInven);
    }
    public void SortInventoryByGradeUp()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByName()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => item.named).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
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
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }

    public void Add(ItemData item)
    {
        if (item.itemType == ItemType.UpMaterial)
        {
            exItems.Add(item);
            onUpMaterialItemChanged?.Invoke(item);
        }
        else if (item.itemType == ItemType.Sword)
        {
            eqItems.Add(item);
            onSwordItemChanged?.Invoke(item);
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
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
   
    // 착용한 아이템들을 관리하는 딕셔너리
    private Dictionary<Wearingarea, Item_Artifact> equippedArtifacts = new Dictionary<Wearingarea, Item_Artifact>();

    public void EquipArtifact(Item_Artifact artifact)
    {
        if (artifact == null)
        {
            Debug.Log("유효하지 않은 아이템입니다.");
            return;
        }

        // 아이템이 착용 가능한 부위에 등록
        if (equippedArtifacts.ContainsKey(artifact.water))
        {
            // 이미 해당 부위에 아이템이 착용되어 있다면, 먼저 제거 후 다시 등록
            UnequipArtifact(artifact.water);
        }

        equippedArtifacts.Add(artifact.water, artifact);
        Debug.Log(artifact.named + "을(를) " + artifact.water + "에 착용하였습니다.");

        // 세트 아이템 효과 체크
        CheckSetEffect();
    }

    public void UnequipArtifact(Wearingarea wearingArea)
    {
        if (equippedArtifacts.ContainsKey(wearingArea))
        {
            Item_Artifact artifact = equippedArtifacts[wearingArea];
            equippedArtifacts.Remove(wearingArea);
            Debug.Log(artifact.named + "을(를) " + wearingArea + "에서 해제하였습니다.");

            // 세트 아이템 효과 체크
            CheckSetEffect();
        }
    }

    private void CheckSetEffect()
    {
        // 각 세트 아이템 효과를 확인하여 효과 발동
        // 예를 들어, 특정 부위에 아이템이 모두 착용되었을 때 효과를 발동하도록 설정
        if (equippedArtifacts.ContainsKey(Wearingarea.bandana) &&
            equippedArtifacts.ContainsKey(Wearingarea.flower) &&
            equippedArtifacts.ContainsKey(Wearingarea.goblet) &&
            equippedArtifacts.ContainsKey(Wearingarea.watch) &&
            equippedArtifacts.ContainsKey(Wearingarea.Feather))
        {
            // 세트 아이템 효과 발동
            Debug.Log("모든 부위에 아이템이 착용되어 세트 아이템 효과가 발동합니다!");
        }
    }
}