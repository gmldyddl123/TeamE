using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponUpgrade : MonoBehaviour
{
    public Item_WeaponData currentWeapon;
    public Item_UpMaterial ore;
    public Inventory inventory;
    public void GainWeaponExp(float exp)
    {
        if (currentWeapon != null)
        {
            currentWeapon.exp += exp;
            // 무기 경험치를 획득하고 레벨업하는 기능을 추가합니다.
            ore.count--;
            currentWeapon.Upgrade();
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
