using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SlotData : MonoBehaviour
{
    public GameObject newTap;

    public TextMeshProUGUI newText;

    //protected GameManager gm;

    public int itemCount;

    public ItemData item;

    public Image iconImg;

    public Action onClearItem;

    const int maxCount = 15;

    public ItemInfo info;

    public Image imageComponent;

    //아이템을 소유하고 있는 슬룻인지 아닌지
    public bool initItem { get; private set; } = false;

    public bool IsDirectUse => true;

    // 딕셔너리에서 등급에 해당하는 RGB 값을 가져옵니다.
    Color targetColor;

    //public Sprite sprite { get; set; }

    //private void Awake()
    //{
    //    iconImg = GetComponent<Image>();
    //}
    bool isSlotTap = false;
    void Start()
    {
        if (isSlotTap)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void AddItem(ItemData newitem)
    {
        if (maxCount > itemCount)
        {
            gameObject.SetActive(true);
            isSlotTap = true;
            item = newitem;
            iconImg.sprite = item.icon;
            iconImg.enabled = true;
            Debug.Log("슬룻 AddItem");
            ChangeImageColorWithGrade(newitem.itemgrade, newitem);
            initItem = true;
        }
        else if(maxCount < itemCount)
        {
            newitem = null;
        }
    }
    // 해당 슬롯의 아이템 갯수 업데이트
    public virtual void SetSlotCount(int _count)
    {
       

    }
    public virtual void ClearSloat()
    {
        item = null;
        iconImg.sprite = null;
        //iconImg.enabled = false;
        initItem = false;
        itemCount = 0;
        //text_Count.text = itemCount.ToString();
    }
    public virtual void Use()
    {

    }
    void ChangeImageColorWithGrade(ItemGrade grade, ItemData weaponitem)
    {
        if (weaponitem.gradeColor.TryGetValue(grade, out targetColor))
        {
            // 이미지의 컬러를 변경합니다.
            imageComponent.color = targetColor;
        }
    }
}