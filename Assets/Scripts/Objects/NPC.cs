using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum NPCType
{
    Talkative,
    Trader,
    QuestGiver
}
public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] NPCType type;

    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;
    //[SerializeField] private string interactText;

    public delegate void ChangeCameraEventHandler();
    public static event ChangeCameraEventHandler OnStartDialouge;
    public static event ChangeCameraEventHandler OnEndDialogue;

    [Header("NPC Settings")]
    [SerializeField] private float turnSpeed = 5f;
    private Vector3 playerPosition;
    
    public InteractableConfig GetInteractableConfig() => objectDetails;

    private Locomotion locomotion;

    
    public void Interact()
    {
        UpdatePlayerPosition();

        //swap to dialogue camera
        StartDialogue();
        
        //Transform playerPosition = interactionHandler.transform;
        //print("Player position: " + playerPosition.position);
        //determine npc type

        //turn the npc to face player
        //display dialogue options
        //if merchant, another script handles the trading process
    }
    void StartDialogue()
    {
        OnStartDialouge.Invoke();
        StartCoroutine(FacePlayer());
    }
    void EndDialogue()
    {
        OnEndDialogue.Invoke();
    }
    private IEnumerator FacePlayer()
    {
        Vector3 targetDirection = playerPosition - transform.position;
        targetDirection.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            yield return null;
        }
    }
    void UpdatePlayerPosition()
    {
        playerPosition = FindObjectOfType<InteractionHandler>().transform.position;
    }
    void DetermineNPCType()
    {
        switch (type)
        {
            case NPCType.Talkative:
                break;
            case NPCType.Trader:
                break;
            case NPCType.QuestGiver:
                break;
        }
    }
}
