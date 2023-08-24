using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ���� ������ ���� static ����
    public static GameManager instance;
    // ���� �÷��̾ ���� ���� ���� �����͸� �����ϴ� ����
    public Item_WeaponData currentEquippedWeapon;
    private void Awake()
    {
        instance = this;
    }
    // ��� �������� �����ϴ� �Լ�
    public void EquipWeapon(Item_WeaponData weapon)
    {
        // ������ ������ ����� ���� �������� Ȯ��
        if (currentEquippedWeapon == weapon)
        {
            // �̹� ���� ���⸦ ���� ���̹Ƿ� �����Ͽ� �Լ� ����
            return;
        }
        // ���ο� ���⸦ �����ϵ��� ����
        UnequipWeapon(); // ���� ���⸦ �����ϴ� �Լ� ȣ��
        weapon.isEquippedItem = true;
        currentEquippedWeapon = weapon;
        Debug.Log($"{weapon.named}��(��) ���� �Ǿ����ϴ�");
        GameManager_baseDm = GameManager_dm;
        GameManager_baseDf = GameManager_df;
        GameManager_dm += currentEquippedWeapon.plusAttack;
        GameManager_df += currentEquippedWeapon.plusDef;

        // TODO: ��� �������� �����ϴ� ������ �߰��ϼ���.
        // ���� ������ ���⸦ ĳ���Ϳ� �����ϴ� ���� ó���� �����ؾ� �մϴ�.
        // �� �κ��� ����� ĳ���Ϳ� ���� �ý��ۿ� ���� �����ϼž� �մϴ�.
        // ���� ���, ĳ������ ���� ���Կ� �ش� ���⸦ �����ϴ� ���� ������ �����ؾ� �մϴ�.
    }
    // ���� ���⸦ �����ϴ� �Լ�
    public void UnequipWeapon()
    {
        if (currentEquippedWeapon != null)
        {
            // ���� ������ ����ġ�� �����ϰ� �ʱ� ������ �ǵ����ϴ�.
            //currentEquippedWeapon.isEquipped = false;
            currentEquippedWeapon.isEquippedItem = false;
            GameManager_dm = GameManager_baseDm;
            GameManager_df = GameManager_baseDf;
            // TODO: ���� ���⸦ �����ϴ� ������ �߰��ϼ���.
            // ���� ���, ĳ������ ���� ���Կ��� ���� ���⸦ �����ϴ� ���� ó���� �����ؾ� �մϴ�.
        }
    }
    public float GameManager_df = 10;
    public float GameManager_dm = 10;
    float GameManager_baseDm;
    float GameManager_baseDf;
}
