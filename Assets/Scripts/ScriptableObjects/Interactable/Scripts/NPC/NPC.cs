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
    private NPC currentQuestNPC;


    private GiveQuest giveQuest;

    [Header("UI Settings")]
    [SerializeField] private InteractableConfig objectDetails;


    [Header("NPC Settings")]
    [SerializeField] private float turnSpeed = 5f;
    private Vector3 playerPosition;

    [Header("Quest Status")]
    public bool isQuestStarted = false;

    public delegate void TriggerDialogue();
    public static event TriggerDialogue ChangeToDialogueCam;
    public static event TriggerDialogue ChangeToThirdPersonCam;

    public delegate bool SetIsTalking();
    public static event SetIsTalking ToggleIsTalking;

    private void Awake()
    {
        giveQuest = GetComponent<GiveQuest>();
    }
    private void OnEnable()
    {
        QuestHandler.SetIsQuestStarted += ToggleIsQuestStarted;
    }
    private void OnDisable()
    {
        QuestHandler.SetIsQuestStarted -= ToggleIsQuestStarted;
    }
    public InteractableConfig GetInteractableConfig() => objectDetails;  
    public void Interact()
    {
        //print("current Quest NPC = " + currentQuestNPC);
        //print("this = " + this);
        if (type == NPCType.QuestGiver && currentQuestNPC == this)
        {
            print("take items from inventory");
            giveQuest.GetObjectivesInPlayerInventory();
            return;
        }
        if (type == NPCType.QuestGiver && isQuestStarted && currentQuestNPC != this) //prevents player from taking many quests
        {
            //spawn text of finish quest first
            print("Finish Quest First!");
            return;
        }
        
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

        //sets current NPC to avoid taking quest to other quest NPCs
        currentQuestNPC = this;
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
    void ToggleIsQuestStarted()
    {
        isQuestStarted = !isQuestStarted;
        if (!isQuestStarted)
        {
            currentQuestNPC = null;
        }
    }
}
