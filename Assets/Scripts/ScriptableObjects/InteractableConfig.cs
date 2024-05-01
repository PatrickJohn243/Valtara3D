using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Interactables/interactables")]
public class InteractableConfig : ScriptableObject
{
    [Header("Object Settings")]
    public Texture2D itemImg;
    public GameObject[] interactableObj; 

    public string prompt;
}
