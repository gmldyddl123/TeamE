using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace player
{
    public partial class PlayerController
    {

        //public PlayerStat currentPlayerCharacter;
        //public Item_WeaponData weapon;
        // 이 메소드를 호출하여 플레이어의 현재 무기를 해제합니다.
        public void UnequipCurrentWeapon()
        {
            currentPlayerCharacter.EquipWeapon = null;


            //if (currentPlayerCharacter.equippedWeapon != null)
            //{
                // 현재 장착된 무기의 능력치를 제거합니다.
                //currentPlayerCharacter.attack -= currentPlayerCharacter.equippedWeapon.plusAttack;
                //currentPlayerCharacter.equippedWeapon = null;

                // 만약 무기의 시각적 표현이 있다면, 여기서 제거해야 합니다.
            //}
        }
        // 이 메소드를 호출하여 플레이어에게 새 무기를 장착
        public void EquipWeapon(Item_WeaponData weaponData)
        {

            currentPlayerCharacter.EquipWeapon = weaponData;
            
            //weapon = weaponData;
            // 무기를 장착하는 로직
            //currentPlayerCharacter.equippedWeapon = weaponData;
            //currentPlayerCharacter.attack += weaponData.plusAttack;
        }
    }
}
