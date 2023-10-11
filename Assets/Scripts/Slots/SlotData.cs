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

    //�������� �����ϰ� �ִ� �������� �ƴ���
    public bool initItem { get; private set; } = false;

    public bool IsDirectUse => true;

    // ��ųʸ����� ��޿� �ش��ϴ� RGB ���� �����ɴϴ�.
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
            Debug.Log("���� AddItem");
            ChangeImageColorWithGrade(newitem.itemgrade, newitem);
            initItem = true;
        }
        else if(maxCount < itemCount)
        {
            newitem = null;
        }
    }
    // �ش� ������ ������ ���� ������Ʈ
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
            // �̹����� �÷��� �����մϴ�.
            imageComponent.color = targetColor;
        }
    }
}