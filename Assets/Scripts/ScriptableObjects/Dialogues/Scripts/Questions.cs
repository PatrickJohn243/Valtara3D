using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Questions")]
public class Questions : ScriptableObject
{
    [System.Serializable]
    public struct Choice
    {
        [TextArea(2, 5)]
        public string choice;
        public Conversation.ConversationLines conversation;
    }

    [TextArea(2, 5)]
    public string question;
    public Choice [] choices;
}

