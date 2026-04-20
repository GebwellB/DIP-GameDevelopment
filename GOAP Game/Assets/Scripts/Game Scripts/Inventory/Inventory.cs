using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GOAP;

public class Inventory : MonoBehaviour
{
    //public int capacity = 10;
    [SerializeField]
    List<ItemStack> inventory = new List<ItemStack>();
    MapInjector mapInjector = new MapInjector();

    [SerializeField] G_Inventory refInventoryState;
    [SerializeField] G_Inventory inventoryState;

    private void Awake() // When object is loaded
    {
        mapInjector.FindAndInjectObject(transform.position, this);
        if(refInventoryState != null)
        {
            AssignWorldState();
        }
    }

    #region World State
    void AssignWorldState()
    {
        if (refInventoryState.isLocal)
        {
            inventoryState = refInventoryState.Clone() as G_Inventory;
        }
        inventoryState.SetValue(this);
    }

    public G_Inventory GetWorldState()
    {
        return inventoryState;
    }

    #endregion

    #region Inventory Functions

    /// <summary>
    /// For adding items to the Inventory. If it finds the item type in the inventory, it will add to the stack of that item
    /// If it doesn't, it will start a new stack
    /// </summary>
    /// <param name="stack"></param>
    public void AddToInventory(ItemStack stack)
    {
        if (stack.item != null)
        {
            if (stack.item.stackable)
            {
                StackItem(stack);
            }
            else
            {
                inventory.Add(new ItemStack(stack));
            }
        }
    }

    public int SubtractFromInventory(ItemStack stack)
    {
        int subtraction = stack.quantity;
        if (stack != null && stack.item != null)
        {
            ItemStack stacktoSubtrack = FindInInventory(stack.item);
            if(stack.quantity > stacktoSubtrack.quantity)
            {
                subtraction = stacktoSubtrack.quantity;
            }

            stacktoSubtrack.quantity -= stack.quantity;
            if(stacktoSubtrack.quantity <= 0)
            {
                inventory.Remove(stacktoSubtrack);
            }
        }
        else
        {
            subtraction = 0;
        }
        return subtraction;
    }

    void StackItem(ItemStack stack)
    {
        ItemStack existingStack = FindInInventory(stack.item);

        if (existingStack != null) // Stacks the item
        {
            existingStack.quantity += stack.quantity;
        }
        else // Adds a new stack
        {
            inventory.Add(new ItemStack(stack));
        }
    }

    /// <summary>
    /// Returns a stack of given item in the inventory if it is in there, otherwise returns null
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public ItemStack FindInInventory(Item item)
    {
        ItemStack foundStack = null;
        if(item != null)
        {
            foundStack = inventory.Find((stack) => stack.item == item);
        }
        return foundStack;
    }

    #endregion

    #region Trade

    public bool IsTradeValid(ItemStack requestedItem, ItemStack offeredItem, bool requestFullQuantity)
    {
        bool isValid = false;
        // Debug.Log("Validate Trade");
        if (IsTrade(requestedItem, offeredItem)) // Trade
        {
            // Debug.Log("Trade");
            if (CanTakeFromInventory(requestedItem, requestFullQuantity))
            {
                // Debug.Log("Trade is valid");
                isValid = true;
            }
        }
        else if (IsTake(requestedItem, offeredItem)) // Take
        {
            // Debug.Log("Take");
            if (CanTakeFromInventory(requestedItem, requestFullQuantity))
            {
                // Debug.Log("Take is valid");
                isValid = true;
            }
        }
        else if (IsGive(requestedItem, offeredItem)) // Give
        {
            isValid = true;
        }

        return isValid;
    }

    public bool Trade(ItemStack requestedItem, ItemStack offeredItem, bool requestFullQuantity, out ItemStack receivedItem)
    {
        bool succeeded = false;
        receivedItem = null;

        if (IsTrade(requestedItem, offeredItem)) // Trade
        {
            receivedItem = TradeItem(requestedItem, offeredItem, requestFullQuantity);
            if(receivedItem != null)
            {
                succeeded = true;
            }
        }
        else if(IsTake(requestedItem, offeredItem)) // Take
        {
            receivedItem = TakeItem(requestedItem, requestFullQuantity);
            if (receivedItem != null)
            {
                succeeded = true;
            }
        }
        else if (IsGive(requestedItem, offeredItem)) // Give
        {
            GiveItem(offeredItem);
            succeeded = true;
        }

        return succeeded;
    }

    ItemStack TradeItem(ItemStack requestedItem, ItemStack offeredItem, bool requestFullQuantity)
    {
        ItemStack receivedItem = null;
        receivedItem = TakeItem(requestedItem, requestFullQuantity);
        GiveItem(offeredItem);
        return receivedItem;
    }

    ItemStack TakeItem(ItemStack requestedItem, bool requestFullQuantity)
    {
        ItemStack takenStack = null;
        ItemStack foundItem = FindInInventory(requestedItem.item);
        if (CanTakeFromInventory(requestedItem, foundItem, requestFullQuantity))
        {
            takenStack = new ItemStack(requestedItem.item, SubtractFromInventory(requestedItem));
        }
        return takenStack;
    }

    void GiveItem(ItemStack offeredItem)
    {
        // Commented code left for reference for possible expansion of inventory system (capacity limits)
        //if (inventory.Count < capacity
        //        || inventory.Count == capacity && inventory.Exists((stack) => stack.item == offeredItem.item))
        //{
            AddToInventory(offeredItem);
        //}
    }

    #endregion

    #region Conditions

    public bool IsTrade(ItemStack requestedItem, ItemStack offeredItem)
    {
        return IsStackValid(requestedItem) && IsStackValid(offeredItem);
    }

    public bool IsTake(ItemStack requestedItem, ItemStack offeredItem)
    {
        return IsStackValid(requestedItem) && IsStackValid(offeredItem) == false;
    }

    public bool IsGive(ItemStack requestedItem, ItemStack offeredItem)
    {
        return IsStackValid(requestedItem) == false && IsStackValid(offeredItem);
    }

    public bool IsStackValid(ItemStack stack)
    {
        return stack != null && stack.item != null;
    }

    public bool CanTakeFromInventory(ItemStack requestedItem, ItemStack foundItem, bool requestFullQuantity)
    {
        return foundItem != null
                && InventoryHasQuantity(foundItem, requestedItem, requestFullQuantity);
    }

    public bool CanTakeFromInventory(ItemStack requestedItem, bool requestFullQuantity)
    {
        ItemStack foundItem = FindInInventory(requestedItem.item);
        // Debug.Log($"foundItem is not null {foundItem != null}");
        return foundItem != null
                && InventoryHasQuantity(foundItem, requestedItem, requestFullQuantity);
    }
    bool InventoryHasQuantity(ItemStack foundItem, ItemStack requestedItem, bool requestFullQuantity)
    {
        return requestFullQuantity && foundItem.quantity >= requestedItem.quantity
            || !requestFullQuantity && foundItem.quantity > 0;
    }
    #endregion
}

///// <summary>
///// Datatype for item slots in inventories
///// </summary>
//public class InventorySlot
//{
//    ItemStack stack = new ItemStack();
//}