using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GiveQuest : MonoBehaviour
{
    public QuestObject quest;
    public ItemObject itemRequirement;
    private int requiredAmount;

    public delegate void GetQuestObject(QuestObject questObject);
    public static event GetQuestObject ReturnQuestObject;

    public void ReturnQuest()
    {
        //returns an questObject
        ReturnQuestObject?.Invoke(quest);
    }
    private void Start()
    {
        DetermineObjective();
    }
    private void DetermineObjective()
    {
        if(quest is GatheringQuest gatheringQuest)
        {
            itemRequirement = gatheringQuest.itemRequirement;
            requiredAmount = gatheringQuest.requiredItemAmount;
        }
    }
    public void GetObjectivesInPlayerInventory()
    {
        //find player
        InventoryHandler inventoryHandler = FindObjectOfType<InventoryHandler>();
        //get a component of type inventoryHandler.inventoryObject
        InventoryObject inventory = inventoryHandler.inventory;
        print(inventory);

        //find the object in the inventory,if so, then reduce item, else return text to show player that item is insufficient
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == itemRequirement.ID)
            {
                print("Item is: " + inventory.Container.Items[i].ID);
                if(inventory.Container.Items[i].amount >= requiredAmount)
                {
                    print("Items Subtracted");
                    inventory.SubtractItem(inventory.Container.Items[i].item, requiredAmount);
                   
                }
                else
                {
                    //spawn a text of insufficient items
                    print("Insufficient Item");
                }
            }
            else
            {
                //spawn text on no item found
                return;
            }
        }
        //finish quest - set isqueststarted to true, and questhandler: quest to null
    }
    private void FinishQuest()
    {
        
    }
}
