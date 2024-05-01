using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
using System;

public class Item : MonoBehaviour, IInteractable
{  
    private BoxCollider bc;

    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;
    //[SerializeField] private string interactText;

    [Header("Item Settings")]
    [SerializeField] private float rotationForce = 5f;
    [SerializeField] private float enableColliderDelayTime = .3f;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
        if(bc != null)
        {
            bc.enabled = false;
        }
    }
    private void OnEnable()
    {
        Chest.OnChestOpened += ItemSpawned;
    }
    private void OnDisable()
    {
        Chest.OnChestOpened -= ItemSpawned;
    }
    public InteractableConfig GetInteractableConfig() => objectDetails;
    public void Interact()
    {
        //access inventory

        //delete item in the world
        print("Got Item");
        Destroy(this.gameObject);
    }
    private void ItemSpawned()
    {
        StartCoroutine(EnableColliderAfterDelay());
        StartCoroutine(SpinItem());
    }
    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(enableColliderDelayTime);
        bc.enabled = true;
    }
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
