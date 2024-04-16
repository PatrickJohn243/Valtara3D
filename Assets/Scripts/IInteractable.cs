using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable 
{
    void CanInteract(string type, InteractableType objectType);
}
