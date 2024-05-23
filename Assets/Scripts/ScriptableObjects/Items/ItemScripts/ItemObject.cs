using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    QuestItem
}
public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite UIDisplay;
    public ItemType type;
    [TextArea(10,20)]
    public string description;
}
[System.Serializable]
public class Item
{
    [HideInInspector]
    public string Name;
    public int ID;

    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
    }
}
