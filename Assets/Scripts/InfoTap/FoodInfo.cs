using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodInfo : ItemInfo
{
    public FoodMaking foodMaking;
    protected override void Start()
    {
        base.Start();
    }
    public override void Get(ItemData item)
    {
        itemData = item;
        gameObject.SetActive(true);
        if (item.itemType == ItemType.Food)
        {
            if ((item is Item_FoodItem Material))
            {
                if (Material.foodType == ItemFoodType.Food)
                {
                    itemTypeName = "음식";
                }
                if (Material.foodType == ItemFoodType.FoodMaterial)
                {
                    itemTypeName = "음식 재료";
                }
                level.text = Material.gradeToStars[Material.itemgrade];
                itemsprite.sprite = Material.icon;
                abilitiesName1.text = "HP 회복량\n" + Material.plusHP.ToString();
                abilitiesName2.text = null;
                itemName.text = item.named;
                weponType.text = itemTypeName;
                itemToolTip.text = Material.itemDescription;
                ChangeImageColorWithGrade(Material.itemgrade, Material);
            }
        }
    }
  
    public void MakeFoodSet()
    {
        InvenTap.gameObject.SetActive(false);
        foodMaking.gameObject.SetActive(true);
        foodMaking.FoodGet(itemData);
    }
}
