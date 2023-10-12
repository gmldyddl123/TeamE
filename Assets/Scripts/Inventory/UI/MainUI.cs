using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Inventory UI;
    public GameObject ImportantTap;
    public GameObject FoodTap;
    public GameObject UpMaterialTap;
    public GameObject WeaponTap;
    Button closeTap;
    Button importantTap;
    Button foodTap;
    Button upMaterialTap;
    Button weaponTap;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        closeTap = child.GetComponent<Button>();
        child = transform.GetChild(1);
        importantTap = child.GetComponent<Button>();
        child = transform.GetChild(2);
        foodTap = child.GetComponent<Button>();
        child = transform.GetChild(3);
        upMaterialTap = child.GetComponent<Button>();
        child = transform.GetChild(4);
        weaponTap = child.GetComponent<Button>();
    }

    public void OpenWeaonTap()
    {
        ImportantTap.gameObject.SetActive(false);
        FoodTap.gameObject.SetActive(false);
        UpMaterialTap.gameObject.SetActive(false);
        WeaponTap.gameObject.SetActive(true);
    }

    public void OepnMaTap()
    {
        ImportantTap.gameObject.SetActive(false);
        FoodTap.gameObject.SetActive(false);
        UpMaterialTap.gameObject.SetActive(true);
        WeaponTap.gameObject.SetActive(false);
    }

    public void OpenFoodTap()
    {
        ImportantTap.gameObject.SetActive(false);
        FoodTap.gameObject.SetActive(true);
        UpMaterialTap.gameObject.SetActive(false);
        WeaponTap.gameObject.SetActive(false);
    }

    public void OpenImTap()
    {
        ImportantTap.gameObject.SetActive(true);
        FoodTap.gameObject.SetActive(false);
        UpMaterialTap.gameObject.SetActive(false);
        WeaponTap.gameObject.SetActive(false);
    }

    public void CloseTap()
    {
        UI.activeInven = false;
    }
}
