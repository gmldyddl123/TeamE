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
    public TextMeshProUGUI level;
    public TextMeshProUGUI weponType;
    public UnityEngine.UI.Image itemsprite;
    public TextMeshProUGUI abilitiesName1;
    public TextMeshProUGUI abilitiesName2;
    TextMeshProUGUI itemName;
    public TextMeshProUGUI itemToolTip;
    public UnityEngine.UI.Image imageComponent; // Image ������Ʈ�� �Ҵ��� ����
    public ItemData itemData;

    string itemTypeName;
    private void Awake()
    {
        itemName = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }
    public void Get(ItemData item)
    {
        itemData = item;
        gameObject.SetActive(true);
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
            abilitiesName1.text = "���ݷ�\n" + weaponitem.plusAttacked.ToString();
            abilitiesName2.text = "����\n" + weaponitem.plusDefed.ToString();
            itemName.text = item.named;
            weponType.text = itemTypeName;
            itemToolTip.text = weaponitem.itemDescription;
            ChangeImageColorWithGrade(weaponitem.itemgrade ,weaponitem);
        }  //��� ���� ���� ����
        if (item is Item_UnMaterial unMaterial)
        {
            if (unMaterial.unMaterialType == ItemUnMaterialType.Ore)
            {
                itemTypeName = "����";
            }
            if (unMaterial.unMaterialType == ItemUnMaterialType.Artifact_EXP)
            {
                itemTypeName = "������ ����";
            }
            //float plusAttack = weponitem.plusAttack;
            Debug.Log($"{unMaterial.gradeToStars[unMaterial.itemgrade]}");
            level.text = unMaterial.gradeToStars[unMaterial.itemgrade];
            itemsprite.sprite = unMaterial.icon;
            abilitiesName1.text = "ü�� ȸ��\n" + unMaterial.value.ToString();
            itemName.text = item.named;
            weponType.text = itemTypeName;
            itemToolTip.text = unMaterial.itemDescription;
            ChangeImageColorWithGrade(unMaterial.itemgrade, unMaterial);
        }
    }
    void ChangeImageColorWithGrade(ItemGrade grade, ItemData item)
    {
        // ��ųʸ����� ��޿� �ش��ϴ� RGB ���� �����ɴϴ�.
        Color targetColor;
        if (item.gradeColor.TryGetValue(grade, out targetColor))
        {
            // �̹����� �÷��� �����մϴ�.
            imageComponent.color = targetColor;
        }
    }
    
    public void Used()
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
            GameManager.instance.EquipWeapon((Item_WeaponData)itemData);

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