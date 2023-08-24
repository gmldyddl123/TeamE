using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpInfo : MonoBehaviour
{
    TextMeshProUGUI itemName;
    UnityEngine.UI.Image itemImg;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        itemName = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(1);
        itemImg = child.GetComponent<UnityEngine.UI.Image>();
    }

    void get(ItemData item)
    {
        itemName.text = item.name;
        itemImg.sprite = item.icon;
    }
}
