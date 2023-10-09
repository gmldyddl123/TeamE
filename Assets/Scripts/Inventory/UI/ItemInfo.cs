using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemInfo : MonoBehaviour
{
    public GameObject InvenTap;
    public GameObject WeponUpTap;
    public TextMeshProUGUI level;
    public TextMeshProUGUI weponType;
    public UnityEngine.UI.Image itemsprite;
    public TextMeshProUGUI abilitiesName1;
    public TextMeshProUGUI abilitiesName2;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemToolTip;
    public UnityEngine.UI.Image imageComponent; // Image ������Ʈ�� �Ҵ��� ����
    public ItemData itemData;
    public IncludingStatsActor state;
    protected string itemTypeName;
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }
    public virtual void Get(ItemData item)
    {
        itemData = item;
        gameObject.SetActive(true);
        if(item.itemType == ItemType.Sword)
        {
            if (item is Item_WeaponData weaponitem)
            {
                if (weaponitem.weaponType == WeaponType.Sword)
                {
                    itemTypeName = "��";
                }
                if (weaponitem.weaponType == WeaponType.Bow)
                {
                    itemTypeName = "Ȱ";
                }
                if (weaponitem.weaponType == WeaponType.Generals)
                {
                    itemTypeName = "â";
                }
                if (weaponitem.weaponType == WeaponType.MagicBook)
                {
                    itemTypeName = "����";
                }
                //float plusAttack = weponitem.plusAttack;
                Debug.Log($"{weaponitem.gradeToStars[weaponitem.itemgrade]}");
                level.text = weaponitem.gradeToStars[weaponitem.itemgrade];
                itemsprite.sprite = weaponitem.icon;
                abilitiesName1.text = "���ݷ�\n" + weaponitem.plusAttack.ToString();
                abilitiesName2.text = "����\n" + weaponitem.plusDef.ToString();
                itemName.text = item.named;
                weponType.text = itemTypeName;
                itemToolTip.text = weaponitem.itemDescription;
                ChangeImageColorWithGrade(weaponitem.itemgrade, weaponitem);
            }  //��� ���� ���� ����
        }
        if (item.itemType == ItemType.UpMaterial)
        {
            if((item is Item_UpMaterial Material))
            {
                if (Material.unMaterialType == ItemUnMaterialType.Ore)
                {
                    itemTypeName = "����";
                }
                if (Material.unMaterialType == ItemUnMaterialType.Artifact_EXP)
                {
                    itemTypeName = "������ ����";
                }
                //float plusAttack = weponitem.plusAttack;
                Debug.Log($"{Material.gradeToStars[Material.itemgrade]}");
                level.text = Material.gradeToStars[Material.itemgrade];
                itemsprite.sprite = Material.icon;
                abilitiesName1.text = "����ġ ȹ�淮\n" + Material.plusEXP.ToString();
                abilitiesName2.text = null;
                itemName.text = item.named;
                weponType.text = itemTypeName;
                itemToolTip.text = Material.itemDescription;
                ChangeImageColorWithGrade(Material.itemgrade, Material);
            }
        }
        if (item.itemType == ItemType.Food)
        {
            if ((item is Item_FoodItem Material))
            {
                if (Material.foodType == ItemFoodType.Food)
                {
                    itemTypeName = "����";
                }
                if (Material.foodType == ItemFoodType.FoodMaterial)
                {
                    itemTypeName = "���� ���";
                }
                level.text = Material.gradeToStars[Material.itemgrade];
                itemsprite.sprite = Material.icon;
                abilitiesName1.text = "HP ȸ����\n" + Material.plusHP.ToString();
                abilitiesName2.text = null;
                itemName.text = item.named;
                weponType.text = itemTypeName;
                itemToolTip.text = Material.itemDescription;
                ChangeImageColorWithGrade(Material.itemgrade, Material);
            }
        }
        if (item.itemType == ItemType.Important)
        {
            if ((item is Item_UpMaterial Material))
            {
                itemTypeName = "����Ʈ ������";
                //float plusAttack = weponitem.plusAttack;
                Debug.Log($"{Material.gradeToStars[Material.itemgrade]}");
                level.text = Material.gradeToStars[Material.itemgrade];
                itemsprite.sprite = Material.icon;
                itemName.text = item.named;
                weponType.text = itemTypeName;
                itemToolTip.text = Material.itemDescription;
                ChangeImageColorWithGrade(Material.itemgrade, Material);
            }
        }
    }
    public void ChangeImageColorWithGrade(ItemGrade grade, ItemData item)
    {
        // ��ųʸ����� ��޿� �ش��ϴ� RGB ���� �����ɴϴ�.
        Color targetColor;
        if (item.gradeColor.TryGetValue(grade, out targetColor))
        {
            // �̹����� �÷��� �����մϴ�.
            imageComponent.color = targetColor;
        }
    }
    public void Use()
    {
        if (itemData.itemType != ItemType.Sword)
        {
            // ������ ������ ������ ������ null�� �ƴ� ��쿡�� �������� ����մϴ�.
            if (itemData != null)
            {
                // ������ ������ ������ ã���ϴ�.
                WeaponSlot equippedSlot = FindSlotWithEquippedItem();
                if (equippedSlot != null)
                {
                    equippedSlot.isEquippedSlot = false;
                    InventorUi.instance.ChangeEquipWeapon();
                    Debug.Log($"���� �������� {equippedSlot.name} ���Կ��� �����Ǿ����ϴ�.");
                }
                // ������ �ε����� GameManager�� �����մϴ�.
                //GameManager.instance.EquipWeapon((Item_WeaponData)itemData);
                // ���� �������� ���� ������ ã�Ƽ� ��� ���� ���·� �����մϴ�.
                WeaponSlot currentSlot = FindSlotWithItem(itemData);
                if (currentSlot != null)
                {
                    currentSlot.isEquippedSlot = true;
                    InventorUi.instance.ChangeEquipWeapon();
                    Debug.Log($"�������� {currentSlot.name} ���Կ� �����Ǿ����ϴ�.");
                    // �������� �����Ǿ����Ƿ� �κ��丮�� ������ �������մϴ�.
                    // ������ ���¿� ���� ������ ������ �� ������ �ű�ϴ�.
                    // currentSlot.transform.SetAsFirstSibling();
                }
            }
        }
        else if (itemData.itemType != ItemType.Food)
        {
            if ((itemData is Item_FoodItem Material))
            {
                state.HP += Material.plusHP;
            }
        }
    }
    public WeponUp weponUp;
    public void WeponUpSet()
    {
        InvenTap.gameObject.SetActive(false);
        WeponUpTap.gameObject.SetActive(true);
        weponUp.WeponGet(itemData);
    }
    public void GetSellItem(SellItems sellitem)
    {
        InvenTap.gameObject.SetActive(false);
    }
    WeaponSlot FindSlotWithItem(ItemData item)
    {
        // ���Ե��� ã�Ƽ� ������ �����۰� ��ġ�ϴ� ������ ��ȯ�մϴ�.
        WeaponSlot[] allSlots = FindObjectsOfType<WeaponSlot>();
        foreach (WeaponSlot slot in allSlots)
        {
            if (slot.item == item)
            {
                return slot;
            }
        }
        return null; // ��ġ�ϴ� ������ ���� ��� null�� ��ȯ�մϴ�.
    }
    WeaponSlot FindSlotWithEquippedItem()
    {
        // ��� ������ ��ȸ�ϸ� ������ �������� �ִ� ������ ã�� ��ȯ�մϴ�.
        WeaponSlot[] allSlots = FindObjectsOfType<WeaponSlot>();
        foreach (WeaponSlot slot in allSlots)
        {
            if (slot.isEquippedSlot)
            {
                return slot;
            }
        }
        return null; // ������ ������ ���� ��� null�� ��ȯ�մϴ�.
    }
}