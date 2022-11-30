using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite weaponSprite;
    public Sprite consumableSprite;
    public Sprite coinSprite;
    public Sprite objectSprite;

}
