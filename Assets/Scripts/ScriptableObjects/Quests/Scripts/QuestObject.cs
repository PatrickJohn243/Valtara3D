using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    Gathering,
    Killing
}
public abstract class QuestObject : ScriptableObject
{
    [Header("Quest Details")]
    public int questID;
    public string questTitle;
    [TextArea(10,20)]
    public string questDescription;
    public QuestType questType;
}
