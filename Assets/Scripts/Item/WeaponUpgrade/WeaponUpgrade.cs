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
            // ���� ����ġ�� ȹ���ϰ� �������ϴ� ����� �߰��մϴ�.
            ore.count--;
            currentWeapon.Upgrade();
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
