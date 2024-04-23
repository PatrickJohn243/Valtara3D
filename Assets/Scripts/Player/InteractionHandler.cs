using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    private Locomotion locomotion;

    private IInteractable interactable;


    [Header("Interactable Range")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius = 0.05f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    void Awake()
    {
        locomotion = GetComponent<Locomotion>();       
    }
    public void Interact()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableMask);
        
        if(numFound > 0f)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            interactable?.ShowInteractUI(true);
            if(interactable != null && Keyboard.current.fKey.wasPressedThisFrame)
            {
                interactable.Interact();
            }
        }
        else
        {
            interactable?.ShowInteractUI(false);
            interactable = null;
        }
    }   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
