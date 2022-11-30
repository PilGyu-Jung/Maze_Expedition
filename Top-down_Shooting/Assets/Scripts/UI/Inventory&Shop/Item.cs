using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Consumable,
        Coin,
        Object
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType) {
            default:
            case ItemType.Weapon:       return ItemAssets.Instance.weaponSprite;
            case ItemType.Consumable:   return ItemAssets.Instance.consumableSprite;
            case ItemType.Object:       return ItemAssets.Instance.objectSprite;
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
        }
    }

}
