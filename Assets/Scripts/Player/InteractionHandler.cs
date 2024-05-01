using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    private AnimationHandler animationHandler;
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
    }
    public void Interact()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableMask);
        
        //if(numFound > 0f && colliders[0].TryGetComponent<IInteractable>(out interactableObj))
        if(numFound > 0f)
        {
            interactableObj = colliders[0].GetComponent<IInteractable>();
            print(interactableObj);
            interactable = interactableObj.GetInteractableConfig();
            showInteractableUI.InteractionPrompt = interactable.prompt;
            showInteractableUI.EnableInteractableUI();

            if (interactable != null && Keyboard.current.fKey.wasPressedThisFrame)
            {
                animationHandler.PlayTargetAnimation("Grab", true, .3f);
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
