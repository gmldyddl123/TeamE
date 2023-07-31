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
            Debug.Log("���⸦ ���׷��̵��߽��ϴ�. ���� ����: " + currentWeapon.level);
        }
        else
        {
            Debug.Log("���⳪ Ore�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
    public void DismantleEquipment(int oreAmount)
    {
        if (currentWeapon != null)
        {
            ore.count += oreAmount; // ������ ȹ���մϴ�.
            Debug.Log("������ ���κ��� ���� " + oreAmount + "���� ȹ���߽��ϴ�.");

            currentWeapon = null;
        }
        else
        {
            Debug.Log("������ ��� �����ϴ�.");
        }
    }
}
