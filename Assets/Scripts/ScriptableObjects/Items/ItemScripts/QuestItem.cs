using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Item", menuName = ("Inventory System/Item/QuestItem"))]
public class QuestItem : ItemObject
{
    private void Awake()
    {
        type = ItemType.QuestItem;
    }
}
