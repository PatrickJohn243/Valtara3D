using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableConfig objectDetails;

    [Header("UI Settings")]
    [SerializeField] private string interactText;

    [Header("Items")]
    [SerializeField] private GameObject[] lootPrefabs;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float throwForce = 5f;

    public bool isOpened = false;

    public delegate void ItemSpawnedEventHandler();
    public static event ItemSpawnedEventHandler OnChestOpened;

    public InteractableConfig GetInteractableConfig()
    {
        print(objectDetails.objName);
        return objectDetails;
    }
    public void Interact()
    {
        // Open Chest - animation

        //Spawn Items w/ force and spinning functions using action events
        foreach(GameObject loot in lootPrefabs)
        {
            GameObject instantiatedLoot = Instantiate(loot, spawnLocation.position, Quaternion.identity);
            Rigidbody rb = instantiatedLoot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized;

                rb.AddForce(randomDirection * throwForce, ForceMode.Impulse);

                OnChestOpened?.Invoke();
            } 
        }
        isOpened = true;
        //delete chest after delay using coroutine
        if (isOpened)
        {
            StartCoroutine(DeleteChestAfterOpening());
        }
    }
    IEnumerator DeleteChestAfterOpening()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}