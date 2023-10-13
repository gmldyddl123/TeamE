using Microsoft.Unity.VisualStudio.Editor;
using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public PlayerController playerController;
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
        if (itemData != null && itemData.itemType == ItemType.Sword)
        {
            WeaponSlot currentSlot = FindSlotWithItem(itemData);
            int currentCharacterId = playerController.CurrentPickCharacterNum;

            // ���� ĳ���Ͱ� �̹� �������� �����ϰ� �ִ��� Ȯ���մϴ�.
            WeaponSlot equippedSlot = FindSlotWithEquippedItem();

            // �̹� ������ �������� �ְ�, ���� �����Ϸ��� �������� �ٸ� ���, ������ ������ �����մϴ�.
            if (equippedSlot != null && equippedSlot.item != itemData)
            {
                equippedSlot.SetEquippedStatusForCharacter(currentCharacterId, false); // ���� ���� ����
                playerController.UnequipCurrentWeapon(); // ���� ���� ����
                Debug.Log($"���� �������� {equippedSlot.name} ���Կ��� �����Ǿ����ϴ�.");
            }
            // �� �������� �����մϴ�.
            currentSlot.SetEquippedStatusForCharacter(currentCharacterId, true);
            playerController.EquipWeapon((Item_WeaponData)itemData); // �� ���� ����
            Debug.Log($"�������� {currentSlot.name} ���Կ� �����Ǿ����ϴ�.");
        }

        else if (itemData.itemType == ItemType.Food)
        {
            if (itemData is Item_FoodItem Material)
            {
                playerController.currentPlayerCharacter.HP += Material.plusHP;
                Inventory.instance.RemoveItem(Material);
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
        // ���� ĳ���Ͱ� ������ �������� �ִ� ������ ã���ϴ�.
        WeaponSlot[] allSlots = FindObjectsOfType<WeaponSlot>();
        int currentCharacterId = playerController.CurrentPickCharacterNum;
        foreach (WeaponSlot slot in allSlots)
        {
            if (slot.IsEquippedByCharacter(currentCharacterId))
            {
                return slot;
            }
        }
        return null; // ������ ������ ���� ��� null�� ��ȯ�մϴ�.
    }
}