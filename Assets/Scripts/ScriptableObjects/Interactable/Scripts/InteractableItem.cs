using UnityEngine;
using System.Collections;
using System;

public enum ObjectType
{
    spawnedItem,
    worldItem
}
public class InteractableItem : MonoBehaviour, IInteractable
{  
    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;

    [Header("Item Settings")]
    [SerializeField] private ItemObject obj;
    [SerializeField] private float rotationForce = 5f;
    [SerializeField] private float enableColliderDelayTime = .3f;
    [SerializeField] private ObjectType objectType = ObjectType.spawnedItem;

    private BoxCollider bc;

    public delegate void AddItemToInventory(Item item);
    public static event AddItemToInventory AddItem;


    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
        if(bc != null && objectType == ObjectType.spawnedItem)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
    }
    private void Start()
    {
        if(objectType == ObjectType.spawnedItem)
        {
            ItemSpawned();
        }
    }
    public InteractableConfig GetInteractableConfig() => objectDetails;
    public void Interact()
    {
        //access inventory
        AddItem?.Invoke(new Item(obj));
        //delete item in the world
        Destroy(this.gameObject);
    }
    private void ItemSpawned()
    {
        StartCoroutine(EnableColliderAfterDelay());
        StartCoroutine(SpinItem());
    }
    //enable collider are dropped items behavior only
    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(enableColliderDelayTime);
        bc.enabled = true;
    }
    //refactor this one on a separate code, make this script item manager, handling two types of items, world and dropped items
    private IEnumerator SpinItem()
    {
        float duration = 5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Rotate(Vector3.up * rotationForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(elapsedTime > duration)
        {
            Destroy(this.gameObject);
        }
    }
}
