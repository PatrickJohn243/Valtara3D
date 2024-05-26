using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName =  "GatheringQuest", menuName = "Dialogue/Quest/Gathering")]
public class GatheringQuest : QuestObject
{
    public ItemObject itemRequirement;
    public int requiredItemAmount = 0;
    private void Awake()
    {
        questType = QuestType.Gathering;
    }
}
