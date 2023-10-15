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
    // ĳ���� ID�� ���� ���¸� �����ϴ� ��ųʸ�
    public Dictionary<int, bool> characterEquippedStatus = new Dictionary<int, bool>();
    public int weaponLevel;
    public PlayerController playerController;
    public GameObject isEquippedTap;

    public TextMeshProUGUI isEquippedText;

    public TextMeshProUGUI levelText;
    Item_WeaponData weapon;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    /// <summary>
    /// ù��° ĳ���Ͱ� ������ ���� �������� �ƴ���
    /// </summary>
    public bool isOneEquippedSlot = false;
    /// <summary>
    /// �ι�° ĳ���Ͱ� ������ ���� �������� �ƴ���
    /// </summary>
    public bool isTwoEquippedSlot = false;
    public override void AddItem(ItemData newitem)
    {
        if (newitem is Item_WeaponData weaponData)
        {
            // ������ ���� �������� �̺�Ʈ ������ �����մϴ�(���� ���).
            if (weapon != null)
            {
                weapon.onWeaponLevelChanged -= UpdateLevelText;
            }

            weapon = weaponData;
            base.AddItem(newitem);
            newitem.CurrentSlot = this; // �� ������ �������� ���� �������� �����մϴ�.
            UpdateLevelText(weapon); // �ʱ� ���� �ؽ�Ʈ ����
            weapon.onWeaponLevelChanged += UpdateLevelText; // ���ο� ���� �������� �̺�Ʈ�� �����մϴ�.
        }
    }

    // ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �޼���
    private void UpdateLevelText(Item_WeaponData weaponData)
    {
        levelText.text = "LV." + weaponData.level;
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �̺�Ʈ ������ �����մϴ�.
        if (weapon != null)
        {
            weapon.onWeaponLevelChanged -= UpdateLevelText;
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
        // �ٸ� ĳ���Ͱ� �� ������ ����ϰ� ������, �� ĳ������ ���� ���¸� �����մϴ�.
        foreach (var otherCharacterId in characterEquippedStatus.Keys.ToList())
        {
            if (otherCharacterId != characterId)
            {
                characterEquippedStatus[otherCharacterId] = false;
            }
        }

        // ���� ĳ������ ���� ���¸� �����մϴ�.
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
        // ���� ǥ�ø� �����ϴ� ����. ���� ĳ������ ���� ���¿� ���� ǥ�ø� ������Ʈ�ؾ� �մϴ�.
        // ���� ���:
        isEquippedTap.SetActive(GetEquippedStatusForCharacter(playerController.CurrentPickCharacterNum));

        // ���⼭�� ��� ĳ������ ���� ���¸� Ȯ���ϰ�, ������ �ؽ�Ʈ�� �����ؾ� �մϴ�.
        isEquippedText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        foreach (var pair in characterEquippedStatus)
        {
            if (pair.Value) // ������ ���
            {
                isEquippedText.text += (pair.Key == 0 ? "��̳�" : "����") + " ������\n"; // ĳ���� �̸��� �����ؾ� �� ���� �ֽ��ϴ�.
            }
        }
    }

}