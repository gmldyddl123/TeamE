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
    public UnityEngine.UI.Image imageComponent; // Image 컴포넌트를 할당할 변수
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
                itemTypeName = "검";
            }
            if (weaponitem.weaponType == WeaponType.Bow)
            {
                itemTypeName = "활";
            }
            if (weaponitem.weaponType == WeaponType.Generals)
            {
                itemTypeName = "창";
            }
            if (weaponitem.weaponType == WeaponType.MagicBook)
            {
                itemTypeName = "법구";
            }
            //float plusAttack = weponitem.plusAttack;
            Debug.Log($"{weaponitem.gradeToStars[weaponitem.itemgrade]}");
            level.text = weaponitem.gradeToStars[weaponitem.itemgrade];
            itemsprite.sprite = weaponitem.icon;
            abilitiesName1.text = "공격력\n" + weaponitem.plusAttack.ToString();
            abilitiesName2.text = "방어력\n" + weaponitem.plusDef.ToString();
            itemName.text = item.named;
            weponType.text = itemTypeName;
            itemToolTip.text = weaponitem.itemDescription;
            ChangeImageColorWithGrade(weaponitem.itemgrade ,weaponitem);
        }  //장비 관련 정보 설정
        if (item is Item_UnMaterial unMaterial)
        {
            if (unMaterial.unMaterialType == ItemUnMaterialType.Ore)
            {
                itemTypeName = "광석";
            }
            if (unMaterial.unMaterialType == ItemUnMaterialType.Artifact_EXP)
            {
                itemTypeName = "성유물 포션";
            }
            //float plusAttack = weponitem.plusAttack;
            Debug.Log($"{unMaterial.gradeToStars[unMaterial.itemgrade]}");
            level.text = unMaterial.gradeToStars[unMaterial.itemgrade];
            itemsprite.sprite = unMaterial.icon;
            abilitiesName1.text = "체력 회복\n" + unMaterial.value.ToString();
            itemName.text = item.named;
            weponType.text = itemTypeName;
            itemToolTip.text = unMaterial.itemDescription;
            ChangeImageColorWithGrade(unMaterial.itemgrade, unMaterial);
        }
    }
    void ChangeImageColorWithGrade(ItemGrade grade, ItemData item)
    {
        // 딕셔너리에서 등급에 해당하는 RGB 값을 가져옵니다.
        Color targetColor;
        if (item.gradeColor.TryGetValue(grade, out targetColor))
        {
            // 이미지의 컬러를 변경합니다.
            imageComponent.color = targetColor;
        }
    }
    
    public void Used()
    {
        // 선택한 슬롯의 아이템 정보가 null이 아닌 경우에만 아이템을 장비합니다.
        if (itemData != null)
        {
            // 기존에 장착된 슬롯을 찾습니다.
            WeaponSlot equippedSlot = FindSlotWithEquippedItem();
            if (equippedSlot != null)
            {
                equippedSlot.isEquippedSlot = false;
                InventorUi.instance.ChangeEquipWeapon();
                Debug.Log($"기존 아이템이 {equippedSlot.name} 슬롯에서 해제되었습니다.");
            }

            // 슬롯의 인덱스를 GameManager로 전달합니다.
            GameManager.instance.EquipWeapon((Item_WeaponData)itemData);

            // 현재 아이템을 담은 슬롯을 찾아서 장비를 장착 상태로 변경합니다.
            WeaponSlot currentSlot = FindSlotWithItem(itemData);
            if (currentSlot != null)
            {
                currentSlot.isEquippedSlot = true;
                InventorUi.instance.ChangeEquipWeapon();
                Debug.Log($"아이템이 {currentSlot.name} 슬롯에 장착되었습니다.");

                // 아이템이 장착되었으므로 인벤토리의 슬롯을 재정렬합니다.
                // 슬롯의 상태에 따라 장착된 슬롯을 맨 앞으로 옮깁니다.
                // currentSlot.transform.SetAsFirstSibling();
            }
        }
    }
    WeaponSlot FindSlotWithItem(ItemData item)
    {
        // 슬롯들을 찾아서 장착된 아이템과 일치하는 슬롯을 반환합니다.
        WeaponSlot[] allSlots = FindObjectsOfType<WeaponSlot>();
        foreach (WeaponSlot slot in allSlots)
        {
            if (slot.item == item)
            {
                return slot;
            }
        }
        return null; // 일치하는 슬롯이 없을 경우 null을 반환합니다.
    }
    WeaponSlot FindSlotWithEquippedItem()
    {
        // 모든 슬롯을 순회하며 장착된 아이템이 있는 슬롯을 찾아 반환합니다.
        WeaponSlot[] allSlots = FindObjectsOfType<WeaponSlot>();
        foreach (WeaponSlot slot in allSlots)
        {
            if (slot.isEquippedSlot)
            {
                return slot;
            }
        }
        return null; // 장착된 슬롯이 없을 경우 null을 반환합니다.
    }
}