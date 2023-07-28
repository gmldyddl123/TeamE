using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTap : MonoBehaviour
{
    GameObject exUI;
    GameObject eqUI;
    GameObject imUI;
    private void Awake()
    {
        exUI = transform.GetChild(0).gameObject;
        eqUI = transform.GetChild(1).gameObject;
        imUI = transform.GetChild(2).gameObject;
    }

    public void OpenExslotUI()
    {
        exUI.gameObject.SetActive(true);
        eqUI.gameObject.SetActive(false);
        imUI.gameObject.SetActive(false);
    }
    public void OpenEqslotUI()
    {
        exUI.gameObject.SetActive(false);
        eqUI.gameObject.SetActive(true);
        imUI.gameObject.SetActive(false);
    }
    public void OpenImslotUI()
    {
        exUI.gameObject.SetActive(false);
        eqUI.gameObject.SetActive(false);
        imUI.gameObject.SetActive(true);
    }
}
