using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryPrefab;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int Y_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMNS;
    public int NUMBER_OF_ROWS;

    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }
    private void Update()
    {
        UpdateSlots();
    }
    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.ID].UIDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                //_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot> ();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    public void RefreshDisplay()
    {
        foreach (var item in itemsDisplayed)
        {
            Destroy(item.Key);
        }
        itemsDisplayed.Clear();
        CreateSlots();
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        Image itemImage = obj.transform.GetChild(0).GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.color = new Color(0.7f, 0.7f, 0.7f, itemImage.color.a); // Darken the color
        }
    }
    public void OnExit(GameObject obj)
    {
        Image itemImage = obj.transform.GetChild(0).GetComponentInChildren<Image>();
        if (itemImage != null)
        {
            itemImage.color = new Color(1f, 1f, 1f, itemImage.color.a); // Reset the color
        }
    }
    Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMNS)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMNS)), 0f);
    }
    public void EnableInventory()
    {
        this.gameObject.SetActive(true);
    }
    public void DisableInventory()
    {
        this.gameObject.SetActive(false);
    }
}
