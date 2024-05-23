using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    [System.Serializable]
    public struct ConversationLines
    {
        [TextArea(2, 5)]
        public string text;
        public string speaker;
    }

    public ConversationLines[] conversationLines;
}
