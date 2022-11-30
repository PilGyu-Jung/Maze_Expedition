using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyItemInventory : MonoBehaviour
{
    public ItemBuffer itemBuffer;

    public Transform rootSlot_InvenToEquip;
    public Transform rootSlot_EquipNumkey;

    private List<Image> images_InvenToEquip;
    private List<Slot> slots_EquipNumkey;

    private int slotCnt;
    GameObject item;
    string itemName;


    //private int slotCnt2;


    // Start is called before the first frame update
    void Start()
    {
        slotCnt = rootSlot_InvenToEquip.childCount;
        //slotCnt2 = rootSlot_EquipNumkey.childCount;
        //Debug.Log(slotCnt);
        //Debug.Log(slotCnt2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < slotCnt; i++)
        {
            if (rootSlot_InvenToEquip.GetChild(i).childCount >= 2)
            {
                item = rootSlot_InvenToEquip.GetChild(i).GetChild(1).gameObject;
                itemName = item.GetComponent<Image>().sprite.name;

                if (itemBuffer.items.Find(x=>x.itemName == itemName) != null)
                {
                    slots_EquipNumkey[i].SetItem(itemBuffer.items.Find(x => x.itemName == itemName));
                }
                else
                    Debug.Log("Can't find the Item in ItemBuffer!");
            }
        }
        //Debug.Log(itemName);
    }

    //ItemProperty nameFindItem(string name)
    //{
    //    return itemBuffer.items.Find(x => x.itemName == name);

    //    //for (int i = 0; i < itemBuffer.items.Count; i++)
    //    //{
    //    //    if (name == itemBuffer.items[i].itemName)
    //    //        itemProperty = itemBuffer.items[i];
    //    //    else
    //    //        return null;
    //    //}
    //    //return 
    //    //    itemProperty;
    //}
}
