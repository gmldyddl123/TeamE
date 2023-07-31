using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponUpgrade : MonoBehaviour
{
    public Item_WeaponData currentWeapon;
    public Item_UnMaterial ore;
    public Inventory inventory;

    public void UpgradeWeapon()
    {
        if (currentWeapon != null && ore != null)
        {
            currentWeapon.Upgrade();
            ore.count--;
            Debug.Log("무기를 업그레이드했습니다. 현재 레벨: " + currentWeapon.level);
        }
        else
        {
            Debug.Log("무기나 Ore가 할당되지 않았습니다.");
        }
    }
    public void DismantleEquipment(int oreAmount)
    {
        if (currentWeapon != null)
        {
            ore.count += oreAmount; // 광석을 획득합니다.
            Debug.Log("분해한 장비로부터 광석 " + oreAmount + "개를 획득했습니다.");

            currentWeapon = null;
        }
        else
        {
            Debug.Log("분해할 장비가 없습니다.");
        }
    }
}
