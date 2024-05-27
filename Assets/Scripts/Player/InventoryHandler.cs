using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public InventoryObject inventory;
    public DisplayInventory displayInventory;
    private InputHandler inputHandler;

    public static InventoryHandler instance;

    public void Awake()
    {
        inputHandler = GetComponent<InputHandler>();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        ToggleInventory();
        SaveAndLoadInventory();
    }
    private void OnEnable()
    {
        InteractableItem.AddItem += GetItem;
    }
    private void OnDisable()
    {
        InteractableItem.AddItem -= GetItem;
    }
    public void GetItem(Item item)
    {
        if (item != null)
        {
            inventory.AddItem(item, 1);
        }
    }
    private void SaveAndLoadInventory()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            inventory.Load();
            displayInventory.RefreshDisplay();
        }
    }
    private void ToggleInventory()
    {
        if(inputHandler.isInventoryPressed == true)
        {
            displayInventory.EnableInventory();
        }
        else if(inputHandler.isInventoryPressed == false)
        {
            displayInventory.DisableInventory();
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[12];
    }
}
