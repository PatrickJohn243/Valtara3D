using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InteractableType
{
    NPC,
    Item,
    Structure,
    Chest
}
[CreateAssetMenu(menuName = "Interactables/interactables")]
public class InteractableConfig : ScriptableObject
{
    public string prompt;
    public InteractableType interactableType;
}
