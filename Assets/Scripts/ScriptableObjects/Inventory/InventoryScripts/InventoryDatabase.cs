using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Database", menuName = "Inventory System/Database")]
public class InventoryDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        Debug.Log(GetItem);
        for(int i = 0; i < Items.Length; i++)
        {
            Items[i].ID = i;
            GetItem.Add(i, Items[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }
}
