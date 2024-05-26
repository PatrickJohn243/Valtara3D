using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    private AnimationHandler animationHandler;
    private Locomotion locomotion;

    public GameObject interactionUI;
    public ShowInteractableUI showInteractableUI;

    private IInteractable interactableObj;
    private InteractableConfig interactable;


    [Header("Interactable Range")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 0.05f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    void Awake()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();  
        locomotion = GetComponent<Locomotion>();
    }
    public void Interact()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableMask);
        
        if(numFound > 0f && !locomotion.isTalking)
        {
            interactableObj = colliders[0].GetComponent<IInteractable>();

            interactable = interactableObj.GetInteractableConfig();
            showInteractableUI.InteractionPrompt = interactable.prompt;
            showInteractableUI.EnableInteractableUI();

            if (interactable != null && Keyboard.current.fKey.wasPressedThisFrame)
            {
                switch (interactable.interactableType)
                {
                    case InteractableType.NPC:
                        break;
                    case InteractableType.Item:
                        break;
                    case InteractableType.Structure:
                        break;
                    case InteractableType.Chest:
                        animationHandler.PlayTargetAnimation("Grab", true, .3f);
                        break;
                }
                interactableObj.Interact();
            }
        }
        else
        {
            showInteractableUI.DisableInteractableUI();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
