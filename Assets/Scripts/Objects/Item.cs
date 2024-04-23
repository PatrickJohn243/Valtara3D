using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractableConfig objectDetails;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private GameObject interactBox;
    public void Interact()
    {
        print(objectDetails.prompt);
        //access inventory
        //delete item in the world
    }
    public void ShowInteractUI(bool showUI)
    {
        interactBox.gameObject.SetActive(showUI);
        interactText.text = objectDetails.prompt;
    }
}
