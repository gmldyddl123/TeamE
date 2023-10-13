using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WeaponSlot : SlotData
{
    // 캐릭터 ID와 장착 상태를 저장하는 딕셔너리
    public Dictionary<int, bool> characterEquippedStatus = new Dictionary<int, bool>();
    public int weaponLevel;
    public PlayerController playerController;
    public GameObject isEquippedTap;

    public TextMeshProUGUI isEquippedText;

    public TextMeshProUGUI levelText;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    /// <summary>
    /// 첫번째 캐릭터가 슬롯이 장착 상태인지 아닌지
    /// </summary>
    public bool isOneEquippedSlot = false;
    /// <summary>
    /// 두번째 캐릭터가 슬롯이 장착 상태인지 아닌지
    /// </summary>
    public bool isTwoEquippedSlot = false;
    public override void AddItem(ItemData newitem)
    {
        if(newitem is Item_WeaponData weaponData)
        {
            base.AddItem(newitem);
            newitem.CurrentSlot = this; // 이 슬롯을 아이템의 현재 슬롯으로 설정합니다.
            levelText.text = "LV." + weaponData.level;
        }
    }
    public override void ClearSloat()
    {
        base.ClearSloat();
    }
    public void Set()
    {
        info.Get(item);
        //newTap.gameObject.SetActive(false);
        //newText.text = null;
    }
    public void SetEquippedStatusForCharacter(int characterId, bool status)
    {
        // 다른 캐릭터가 이 슬롯을 사용하고 있으면, 그 캐릭터의 장착 상태를 해제합니다.
        foreach (var otherCharacterId in characterEquippedStatus.Keys.ToList())
        {
            if (otherCharacterId != characterId)
            {
                characterEquippedStatus[otherCharacterId] = false;
            }
        }

        // 현재 캐릭터의 장착 상태를 설정합니다.
        characterEquippedStatus[characterId] = status;
        UpdateEquippedIndicator();
    }

    public bool IsEquippedByCharacter(int characterId)
    {
        return characterEquippedStatus.TryGetValue(characterId, out bool status) && status;
    }

    public bool GetEquippedStatusForCharacter(int characterId)
    {
        return characterEquippedStatus.TryGetValue(characterId, out bool status) && status;
    }

    public void UpdateEquippedIndicator()
    {
        // 장착 표시를 갱신하는 로직. 현재 캐릭터의 장착 상태에 따라 표시를 업데이트해야 합니다.
        // 예를 들면:
        isEquippedTap.SetActive(GetEquippedStatusForCharacter(playerController.CurrentPickCharacterNum));

        // 여기서는 모든 캐릭터의 장착 상태를 확인하고, 적절한 텍스트를 설정해야 합니다.
        isEquippedText.text = ""; // 텍스트 초기화
        foreach (var pair in characterEquippedStatus)
        {
            if (pair.Value) // 장착된 경우
            {
                isEquippedText.text += (pair.Key == 0 ? "루미네" : "엠버") + " 장착중\n"; // 캐릭터 이름을 조정해야 할 수도 있습니다.
            }
        }
    }
    //public void UpdateEquippedIndicator()
    //{
    //    isEquippedTap.SetActive(isEquippedSlot);
    //    if (playerController.CurrentPickCharacterNum == 0)
    //    {
    //        isEquippedText.text = "루미네 장착중".ToString();
    //    }
    //    else if (playerController.CurrentPickCharacterNum == 1)
    //    {
    //        isEquippedText.text = "엠버 장착중".ToString();
    //    }
    //}
}