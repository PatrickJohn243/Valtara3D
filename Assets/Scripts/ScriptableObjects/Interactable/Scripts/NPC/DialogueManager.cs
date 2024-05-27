using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialouge UI's")]
    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    [Header("Flags")]
    public bool isQuestionFinished = false;
    private bool isDialogueStarted = false;

    private NPC npc;
    private QuestDialogueManager questDialogManager;

    private int currentLineIndex = 0;
    private Conversation.ConversationLines[] currentDialogueLines;

    private Conversation conversationObj;
    private Questions questionObj;


    private void Awake()
    {
        npc = GetComponent<NPC>();  
        questDialogManager = GetComponent<QuestDialogueManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isDialogueStarted == true)
        {
            DisplayNextLine();
        }
    }
    public void StartDialogue(Conversation conversation, Questions question)
    {
        isDialogueStarted = true;
        ResetDialogueState();
        this.conversationObj = conversation;
        this.questionObj = question;
        
        DisplayNextLine();
    }
    public void DisplayNextLine()
    {
        if (!isDialogueStarted)
        {
            return;
        }
        currentDialogueLines = conversationObj.conversationLines;
        if (currentLineIndex < currentDialogueLines.Length)
        {
            DisplayDialogueLine();
        }
        else if (currentLineIndex == currentDialogueLines.Length)
        {
            DetermineNPCType();
        }
        else
        {
            EndDialogue();
        }
    }
    private void DisplayDialogueLine()
    {
        Conversation.ConversationLines line = currentDialogueLines[currentLineIndex];
        speakerText.text = line.speaker;
        dialogueText.text = line.text;
        currentLineIndex++;
    }
    private void DisplayQuestionDialogue()
    {
        if (!isQuestionFinished)
        {
            questDialogManager.InitalizeQuestion(questionObj);
            isQuestionFinished = true;
        }
        if (questDialogManager.isChoosingDone)
        {
            isQuestionFinished = true;
            EndDialogue();
            questDialogManager.isChoosingDone = false;
        }
    }
    public void EndDialogue()
    {
        isDialogueStarted = false;
        npc.EndDialogue();
        ResetDialogueState();
    }
    private void ResetDialogueState()
    {
        conversationObj = null;
        questionObj = null;
        currentLineIndex = 0;
        isQuestionFinished = false;
    }
    private void DetermineNPCType()
    {
        if (npc.type == NPCType.Talkative)
        {
            print("talkative");
            EndDialogue();
        }
        else if (npc.type == NPCType.QuestGiver)
        {
            //print("quest giver");
            DisplayQuestionDialogue();
        }
        else if (npc.type == NPCType.Trader)
        {
            //show trade ui
        }
        else
        {
            print("NPC type is null");
        }
    }
}
