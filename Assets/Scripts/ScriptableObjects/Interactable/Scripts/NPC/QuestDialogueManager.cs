using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestDialogueManager : MonoBehaviour
{
    [Header("Choices UI")]
    [SerializeField] private TMP_Text[] choiceText;
    [SerializeField] private Button[] choiceObj;

    [Header("Flags")]
    public bool isChoosingDone = false;

    private DialogueManager dialogueManager;

    private Questions questionObj;

    private void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }
    public void InitalizeQuestion(Questions question)
    {
        GetQuestion(question);
        EnableChoices();
    }
    private void GetQuestion(Questions question)
    {
        this.questionObj = question;

        dialogueManager.dialogueText.text = question.question;
    }
    public void EnableChoices()
    {
        print("set choice is called");
        for (int i = 0; i < choiceText.Length; i++)
        {
            int currentIndex = i;

            choiceText[i].text = questionObj.choices[i].choice;
            choiceObj[i].gameObject.SetActive(true);
            choiceObj[i].onClick.AddListener(()=>ChoiceDialogue(currentIndex));
        }
    }
    private void DisableChoices()
    {
        for (int i = 0; i < choiceObj.Length; i++)
        {
            choiceObj[i].gameObject.SetActive(false);
        }
    }
    private void ChoiceDialogue(int choiceChount)
    {
        DisableChoices();
        Conversation.ConversationLines lines = questionObj.choices[choiceChount].conversation;
        dialogueManager.dialogueText.text = lines.text;
        isChoosingDone = true;
    }
}
