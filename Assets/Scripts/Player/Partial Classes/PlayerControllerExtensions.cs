using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace player
{
    public partial class PlayerController
    {
        public PlayerStat currentPlayerCharacter;
        public Item_WeaponData weapon;
        // �� �޼ҵ带 ȣ���Ͽ� �÷��̾��� ���� ���⸦ �����մϴ�.
        public void UnequipCurrentWeapon()
        {
            //if (currentPlayerCharacter.equippedWeapon != null)
            {
                // ���� ������ ������ �ɷ�ġ�� �����մϴ�.
                //currentPlayerCharacter.attack -= currentPlayerCharacter.equippedWeapon.plusAttack;
                //currentPlayerCharacter.equippedWeapon = null;

                // ���� ������ �ð��� ǥ���� �ִٸ�, ���⼭ �����ؾ� �մϴ�.
            }
        }
        // �� �޼ҵ带 ȣ���Ͽ� �÷��̾�� �� ���⸦ ����
        public void EquipWeapon(Item_WeaponData weaponData)
        {
            //weapon = weaponData;
            // ���⸦ �����ϴ� ����
            //currentPlayerCharacter.equippedWeapon = weaponData;
            //currentPlayerCharacter.attack += weaponData.plusAttack;
        }
    }
}
