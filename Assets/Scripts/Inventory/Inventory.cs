using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : TestBase
{
    public ItemData item1;

    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();
    public Item_WeaponData weaponitem;
    PlayerInputAction inputActions;

    public delegate void OnItemChanged(ItemData _item);

    public OnItemChanged onEqItemChanged;
    public OnItemChanged onExItemChanged;

    public Action onClearslot;

    /// <summary>
    /// �κ��丮 ��
    /// </summary>
    public GameObject inventoryTap;
    public GameObject sortTap;

    bool activeInven = false;
    bool onsortTap = false;

    private void Start()
    {
        inventoryTap.SetActive(activeInven);
    }
    void Awake()
    {
        instance = this;
    }
   
   

    public void SortInventoryByGrade()
    {
        //exItems = exItems.OrderBy(item => (int)item.itemgrade).ToList();

        //foreach (ItemData item in exItems)
        //{
        //    onExItemChanged?.Invoke(item);
        //}
    }
    public void SortInventoryByGradeUp()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onEqItemChanged?.Invoke(item);
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
            onEqItemChanged?.Invoke(item);
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
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
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
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }

    public void Add(ItemData addedItem)
    {
        if (addedItem.itemType == ItemType.expendables)
        {
            exItems.Add(addedItem);
            onExItemChanged?.Invoke(addedItem);
        }
        else if (addedItem.itemType == ItemType.equipment)
        {
            eqItems.Add(addedItem);
            onEqItemChanged?.Invoke(addedItem);
            if (addedItem is Item_WeaponData weaponitem)
                weaponitem.SetAbilities(); //������ �ɷ�ġ ����
        }
    }
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
    // ������ �����۵��� �����ϴ� ��ųʸ�
    private Dictionary<Wearingarea, Item_Artifact> equippedArtifacts = new Dictionary<Wearingarea, Item_Artifact>();

    public void EquipArtifact(Item_Artifact artifact)
    {
        if (artifact == null)
        {
            Debug.Log("��ȿ���� ���� �������Դϴ�.");
            return;
        }

        // �������� ���� ������ ������ ���
        if (equippedArtifacts.ContainsKey(artifact.water))
        {
            // �̹� �ش� ������ �������� ����Ǿ� �ִٸ�, ���� ���� �� �ٽ� ���
            UnequipArtifact(artifact.water);
        }

        equippedArtifacts.Add(artifact.water, artifact);
        Debug.Log(artifact.named + "��(��) " + artifact.water + "�� �����Ͽ����ϴ�.");

        // ��Ʈ ������ ȿ�� üũ
        CheckSetEffect();
    }

    public void UnequipArtifact(Wearingarea wearingArea)
    {
        if (equippedArtifacts.ContainsKey(wearingArea))
        {
            Item_Artifact artifact = equippedArtifacts[wearingArea];
            equippedArtifacts.Remove(wearingArea);
            Debug.Log(artifact.named + "��(��) " + wearingArea + "���� �����Ͽ����ϴ�.");

            // ��Ʈ ������ ȿ�� üũ
            CheckSetEffect();
        }
    }

    private void CheckSetEffect()
    {
        // �� ��Ʈ ������ ȿ���� Ȯ���Ͽ� ȿ�� �ߵ�
        // ���� ���, Ư�� ������ �������� ��� ����Ǿ��� �� ȿ���� �ߵ��ϵ��� ����
        if (equippedArtifacts.ContainsKey(Wearingarea.bandana) &&
            equippedArtifacts.ContainsKey(Wearingarea.flower) &&
            equippedArtifacts.ContainsKey(Wearingarea.goblet) &&
            equippedArtifacts.ContainsKey(Wearingarea.watch) &&
            equippedArtifacts.ContainsKey(Wearingarea.Feather))
        {
            // ��Ʈ ������ ȿ�� �ߵ�
            Debug.Log("��� ������ �������� ����Ǿ� ��Ʈ ������ ȿ���� �ߵ��մϴ�!");
        }
    }
}