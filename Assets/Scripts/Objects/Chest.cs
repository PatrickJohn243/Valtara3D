using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableConfig objectDetails;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private GameObject interactBox;

    [Header("Items")]
    [SerializeField] private GameObject[] lootPrefabs;
    [SerializeField] private float lootForce = 10f;
    [SerializeField] private float maxAngleVariation = 45f;
    [SerializeField] private Transform spawmLocation;
    public void Interact()
    {
        print(lootPrefabs[0].name);
        //Open Chest - animation
        //Drop Item - use addforce to make loots jump out of chest
        foreach (GameObject lootPrefab in lootPrefabs)
        {
            GameObject instantiatedLoot = Instantiate(lootPrefab, spawmLocation.transform.position, Quaternion.identity);
            Rigidbody lootRB = instantiatedLoot.GetComponent<Rigidbody>();

            if (lootRB != null)
            {
                // Generate a random vector with some variation in the angle
                float angleVariation = Random.Range(-maxAngleVariation, maxAngleVariation);
                Vector3 randomDirection = Quaternion.Euler(0f, 0f, angleVariation) * Vector3.up;
                lootRB.AddForce(randomDirection * lootForce, ForceMode.Impulse);
            }
        }
        //Delete chest
    }
    public void ShowInteractUI(bool showUI)
    {
       interactBox.gameObject.SetActive(showUI);
       interactText.text = objectDetails.prompt;
    }
}
