using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
    public Transform rootSlot;
    public Store store;

    private List<Slot> slots;
    private int playerCoin;

    private void Start()
    {

        slots = new List<Slot>();
        int slotCnt = rootSlot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();

            slots.Add(slot);
        }

        store.onSlotClick += BuyItem;
    }

    private void Update()
    {
        playerCoin = PlayerPrefs.GetInt("PlayerCoin");

    }
    void BuyItem(ItemProperty item)
    {
        Debug.Log("���� ����: "+playerCoin +"������ ����: "+ item.itemPrice);
        if (playerCoin >= item.itemPrice)
        {
            playerCoin -= item.itemPrice;
            var emptySlot = slots.Find(t =>
            {
                return t.item == null || t.item.itemName == string.Empty;
            });

            if (emptySlot != null)
            {
                emptySlot.SetItem(item);
            }
            PlayerPrefs.SetInt("PlayerCoin",playerCoin);
        }
        else
            Debug.Log("������ <"+( item.itemPrice - playerCoin )+"> ����Ʈ �����մϴ�.");
    }
}
