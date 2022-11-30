using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellBtn;

    public bool isInventorySlot = false;
    //public GameObject Store;

    private void Awake()
    {
        SetSellBtnInteractable(false);
    }

    void SetSellBtnInteractable(bool b)
    {
        if(sellBtn != null)
        {
            sellBtn.interactable = b;
        }
    }
    public void SetItem(ItemProperty item)
    {
        this.item = item;

        if(item == null)
        {
            image.enabled = false;
            SetSellBtnInteractable(false);

            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;

            gameObject.name = item.itemName;
            if (isInventorySlot == false)
            {
                gameObject.GetComponentsInChildren<Text>()[0].text = item.itemName;
                gameObject.GetComponentsInChildren<Text>()[1].text = item.itemPrice.ToString();
            }
            image.sprite = item.sprite;
            SetSellBtnInteractable(true);
        }
    }

    public void OnClickSellBtn()
    {
        SetItem(null);
    }
}
