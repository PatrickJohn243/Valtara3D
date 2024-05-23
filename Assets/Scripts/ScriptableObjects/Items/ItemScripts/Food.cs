using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Inventory System/Item/Food")]
public class Food : ItemObject
{
    [Header("Item Effect")]
    public int restoreHealthValue;
    public int restoreManaValue;

    //implement a cooldown reduction
    //implement strenght effects, etc.
    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
