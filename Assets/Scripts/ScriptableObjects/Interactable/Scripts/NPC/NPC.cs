using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    Talkative,
    Trader,
    QuestGiver
}
public class NPC : MonoBehaviour, IInteractable
{
    public Conversation conversation;
    public Questions question;
    public DialogueManager dialogueManager;
    public GameObject dialogueObj;

    public NPCType type;

    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;


    [Header("NPC Settings")]
    [SerializeField] private float turnSpeed = 5f;
    private Vector3 playerPosition;

    public delegate void TriggerDialogue();
    public static event TriggerDialogue ChangeToDialogueCam;
    public static event TriggerDialogue ChangeToThirdPersonCam;

    public delegate bool SetIsTalking();
    public static event SetIsTalking ToggleIsTalking;

    public InteractableConfig GetInteractableConfig() => objectDetails;  
    public void Interact()
    {
        UpdatePlayerPosition();
        StartDialogue();
        //if merchant, another script handles the trading process
    }
    //refactor
    void StartDialogue()
    {
        ChangeToDialogueCam?.Invoke();
        ToggleIsTalking?.Invoke(); //sets IsTalking flag to true to disable movements and interaction in the locomotion script
        StartCoroutine(FacePlayer());

        //to be put to dialogue manager
        dialogueObj?.SetActive(true);
        dialogueManager.StartDialogue(conversation, question);
    }
    //refactor
    public void EndDialogue()
    {
        ChangeToThirdPersonCam?.Invoke();
        dialogueObj?.SetActive(false);
        ToggleIsTalking?.Invoke();
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
}
