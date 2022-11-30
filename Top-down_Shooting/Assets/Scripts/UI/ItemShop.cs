using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] itemObj;
    public string[] itemName;
    public int[] itemPrice;
    public Button[] items;
    public bool[] isBuy;

    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            //items[i].transform.Find("Text_Item_name").GetComponent <Text>().text = itemName[i];
            //items[i].transform.Find("Text_Item_price").GetComponent<Text>().text = itemPrice[i].ToString();
            items[i].GetComponentsInChildren<Text>()[0].text = itemName[i];
            items[i].GetComponentsInChildren<Text>()[1].text = itemPrice[i].ToString();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
