using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structures : MonoBehaviour, IInteractable
{
    private InteractableConfig objectDetails;
    public InteractableConfig GetInteractableConfig() => objectDetails;
    public void Interact()
    {
        print("interact");
    }

}
