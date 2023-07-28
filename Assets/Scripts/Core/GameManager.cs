using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴 적용을 위한 static 변수
    public static GameManager instance;

    // 현재 플레이어가 장착 중인 무기 데이터를 저장하는 변수
    public Item_WeaponData currentEquippedWeapon;
    private void Awake()
    {
        instance = this;
    }
    // 장비 아이템을 장착하는 함수
    public void EquipWeapon(Item_WeaponData weapon)
    {
        // 이전에 장착한 무기와 같은 무기인지 확인
        if (currentEquippedWeapon == weapon)
        {
            // 이미 같은 무기를 장착 중이므로 리턴하여 함수 종료
            return;
        }
        // 새로운 무기를 장착하도록 설정
        UnequipWeapon(); // 기존 무기를 해제하는 함수 호출
        weapon.isEquippedItem = true;
        currentEquippedWeapon = weapon;
        Debug.Log($"{weapon.named}이(가) 장착 되었습니다");
        baseDm = dm;
        baseDf = df;
        dm += currentEquippedWeapon.plusAttack;
        df += currentEquippedWeapon.plusDef;

        // TODO: 장비 아이템을 장착하는 로직을 추가하세요.
        // 현재 장착한 무기를 캐릭터에 적용하는 등의 처리를 진행해야 합니다.
        // 이 부분은 당신이 캐릭터와 무기 시스템에 따라서 구현하셔야 합니다.
        // 예를 들어, 캐릭터의 무기 슬롯에 해당 무기를 적용하는 등의 로직을 구현해야 합니다.
    }
    // 기존 무기를 해제하는 함수
    public void UnequipWeapon()
    {
        if (currentEquippedWeapon != null)
        {
            // 기존 무기의 보정치를 제거하고 초기 값으로 되돌립니다.
            //currentEquippedWeapon.isEquipped = false;
            currentEquippedWeapon.isEquippedItem = false;
            dm = baseDm;
            df = baseDf;
            // TODO: 기존 무기를 해제하는 로직을 추가하세요.
            // 예를 들어, 캐릭터의 무기 슬롯에서 기존 무기를 제거하는 등의 처리를 진행해야 합니다.
        }
    }
    public float df = 10;
    public float dm = 10;
    float baseDm;
    float baseDf;
}
