using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour, IInteractable
{
    private Locomotion locomotion;

    void Awake()
    {
        locomotion = GetComponent<Locomotion>();
    }
    public void CanInteract(string type, InteractableType objectType)
    {

    }
}
