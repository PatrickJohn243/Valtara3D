using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Chest : MonoBehaviour, IInteractable
{
    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;
    //[SerializeField] private string interactText;

    [Header("Items")]
    [SerializeField] private GameObject[] lootPrefabs;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private float throwForce = 5f;

    [Header("Flags")]
    public bool isOpened = false;
    public bool isItemSpawned = false;
    public bool isAnimating = false;

    //anim
    public GameObject chestCover;

    public InteractableConfig GetInteractableConfig() => objectDetails;
    public void Interact() => StartCoroutine(OpenChest());

    private IEnumerator OpenChest()
    {
        yield return StartCoroutine(ChestAnim());

        SpawnItems();

        StartCoroutine(DeleteChestAfterOpening());
    }
    private void SpawnItems()
    {
        if (!isItemSpawned)
        {
            foreach (GameObject loot in lootPrefabs)
            {
                GameObject instantiatedLoot = Instantiate(loot, spawnLocation.position, Quaternion.identity);
                Rigidbody rb = instantiatedLoot.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)).normalized;

                    rb.AddForce(randomDirection * throwForce, ForceMode.Impulse);
                }
            }
            isItemSpawned = true;
        }    
    }
    private IEnumerator ChestAnim()
    {
        if (!isAnimating)
        {
            float duration = 1f;
            float elapsedTime = 0f;
            Vector3 startPosition = chestCover.transform.position;
            Vector3 targetPosition = startPosition + Vector3.up * 1f;
            while (elapsedTime < duration)
            {
                chestCover.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration); // Move the chest cover gradually
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            chestCover.transform.position = targetPosition;
        }
        isAnimating = true;
    }
    private IEnumerator DeleteChestAfterOpening()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}