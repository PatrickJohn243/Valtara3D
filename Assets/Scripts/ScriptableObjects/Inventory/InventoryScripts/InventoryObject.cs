using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;

    public InventoryDatabase database;
    public Inventory Container;

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.ID)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }
    public void SubtractItem(Item _item, int _amount) //calls when player is delivering items to NPC
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.ID)
            {
                Container.Items[i].SubtractAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.ID, _item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }
    //refactor save and load into a savehandler script
    [ContextMenu("Save")]
    public void Save()
    {
        //JSON SAVE
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();
        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        Debug.Log(File.Exists(string.Concat(Application.persistentDataPath, savePath)));
        if (File.Exists(string.Concat(Application.persistentDataPath,savePath)))
        {
            //JSON LOAD
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();
            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[12];
}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public int amount;
    public Item item;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _ID, Item _item, int _amount)
    {
        ID = _ID;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _ID, Item _item, int _amount)
    {
        ID = _ID;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public void SubtractAmount(int value)
    {
        amount -= value;
    }
}
