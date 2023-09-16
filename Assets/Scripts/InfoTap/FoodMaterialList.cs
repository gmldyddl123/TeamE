using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaterialList : MonoBehaviour
{
    GameObject materialListSlot1;
    GameObject materialListSlot2;
    GameObject materialListSlot3;
    public Image itemicon1;
    public Image itemicon2;
    public Image itemicon3;
    public TextMeshProUGUI foodMateriakCount1;
    public TextMeshProUGUI foodMateriakCount2;
    public TextMeshProUGUI foodMateriakCount3;

    private void Awake()
    {
        materialListSlot1 = transform.GetChild(0).gameObject;
        materialListSlot2 = transform.GetChild(1).gameObject;
        materialListSlot3 = transform.GetChild(2).gameObject;
    }

    private void SetSlot(GameObject slot, Image itemIcon, TextMeshProUGUI slotText, Ingredient ingredient)
    {
        if (ingredient != null && ingredient.foodMaterialItem != null)
        {
            slot.SetActive(true);
            itemIcon.sprite = ingredient.foodMaterialItem.icon; // sprite วาด็
            slotText.text = ingredient.quantity.ToString();
        }
        else
        {
            slot.SetActive(false);
            itemIcon.sprite = null;
        }
    }
    public void DisplayMaterials(Item_FoodItem foodItem)
    {
        SetSlot(materialListSlot1, itemicon1, foodMateriakCount1, (foodItem.requiredIngredients.Count > 0) ? foodItem.requiredIngredients[0] : null);
        SetSlot(materialListSlot2, itemicon2, foodMateriakCount2, (foodItem.requiredIngredients.Count > 1) ? foodItem.requiredIngredients[1] : null);
        SetSlot(materialListSlot3, itemicon3, foodMateriakCount3, (foodItem.requiredIngredients.Count > 2) ? foodItem.requiredIngredients[2] : null);
    }
    public void Get(ItemData item)
    {
        if (item is Item_FoodItem foodItem)
        {
            DisplayMaterials(foodItem);
        }
    }
}
