using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class InventoryDisplayer : MonoBehaviour
{
    public List<Item> observedItems = new List<Item>();
    public ValueTracker trackerReference;
    Inventory inventory;

    [Header("Win Condition")]
    public GameObject winGUI;
    public G_Condition winCondition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
        if(inventory != null)
        {
            inventory.InventoryUpdated += ReceiveUpdate;
            ReceiveUpdate(inventory);
            //G_Inventory tempState = inventory.GetWorldState();
            winCondition.TrySwitchToLocalState(inventory.GetWorldState());
        }
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.InventoryUpdated -= ReceiveUpdate;
        }
    }

    void ReceiveUpdate(object value)
    {
        Inventory inventory = value as Inventory;
        List<string> displayStrings = new List<string>();

        for (int i = 0; i < observedItems.Count; i++)
        {
            string displayText = observedItems[i].name;
            ItemStack stack = inventory.FindInInventory(observedItems[i]);

            if (stack != null)
            {
                displayText += $" {stack.quantity}";
            }
            else
            {
                displayText += $" {0}";
            }

            displayStrings.Add(displayText);
        }

        if (trackerReference != null)
        {
            trackerReference.Track(displayStrings);
        }

        if (winCondition.DoesStateMeetCondition())
        {
            Debug.Log("WE WIN!!!!!!");
            winGUI?.SetActive(true);
        }
    }
}
