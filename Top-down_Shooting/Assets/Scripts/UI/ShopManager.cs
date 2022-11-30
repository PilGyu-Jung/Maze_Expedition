using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform upgradeShop;
    //public RectTransform itemShop;
    //public RectTransform inventory;
    //public RectTransform equipment;

    public static ShopManager instance;
    public bool enteredShop;
    private void Awake()
    {
        if(ShopManager.instance == null)
        {
            instance = FindObjectOfType<ShopManager>();
        }
        enteredShop = false;
    }
    private void Update()
    {

    }
    public void EnterShop()
    {
        enteredShop = true;
        Cursor.visible = true;
        uiGroup.anchoredPosition = Vector3.zero + new Vector3(0, 5, 0);
    }

    public void ExitShop()
    {
        enteredShop = false;
        Cursor.visible = false;
        uiGroup.anchoredPosition = Vector3.down * 1000;

    }
    public void EnterupgradeShop()
    {
        enteredShop = true;
        Cursor.visible = true;
        upgradeShop.anchoredPosition = Vector3.zero + new Vector3(0, 5, 0);
    }

    public void ExitupgradeShop()
    {
        enteredShop = true;
        Cursor.visible = true;
        upgradeShop.anchoredPosition = Vector3.down * 1000;

    }
    //public void EnteritemShop()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    itemShop.anchoredPosition = Vector3.zero + new Vector3(0, 5, 0);
    //}

    //public void ExititemShop()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    itemShop.anchoredPosition = Vector3.down * 1000;

    //}
    //public void Enterinventory()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    inventory.anchoredPosition = Vector3.zero + new Vector3(0, 5, 0);
    //}

    //public void Exitinventory()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    inventory.anchoredPosition = Vector3.down * 1000;

    //}
    //public void Enterequipment()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    equipment.anchoredPosition = Vector3.zero + new Vector3(-280, -1, 0);
    //}

    //public void Exitequipment()
    //{
    //    enteredShop = true;
    //    Cursor.visible = true;
    //    equipment.anchoredPosition = Vector3.down * 1000;

    //}

}
