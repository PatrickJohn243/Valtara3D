using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    private InputHandler inputHandler;

    [Header("Quest Descriptions")]
    [SerializeField] private GameObject questTab;
    [SerializeField] private TextMeshProUGUI description;

    public QuestObject quest;
    
    public delegate void DetermineQuestStatus();
    public static event DetermineQuestStatus SetIsQuestStarted;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
    }
    private void OnEnable()
    {
        GiveQuest.ReturnQuestObject += GetCurrentQuest;
    }
    private void Update()
    {
        OpenQuestUI();
    }
    private void OpenQuestUI()
    {
        if (inputHandler.isQuestTabPressed)
        {
            questTab.SetActive(true);
        }
        else
        {
            questTab.SetActive(false);
        }
    }
    private void GetCurrentQuest(QuestObject questObject)
    {
        SetIsQuestStarted?.Invoke();
        //get quest from event
        quest = questObject;
        if (quest != null)
        {
            SetCurrentQuest();
        }
    }
    private void SetCurrentQuest()
    {
        //set quest description to UI
        description.text = quest.questDescription;
        
        //set cannot receive quest boolean to true;
    }
    private void ClearCurrentQuest()
    {
        quest = null; 
    }
}

