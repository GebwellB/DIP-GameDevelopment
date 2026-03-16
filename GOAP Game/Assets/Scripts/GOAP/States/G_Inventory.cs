using GOAP;
using System;
using UnityEngine;

namespace GOAP
{
    public class G_Inventory : G_State
    {
        // The value we are storing
        Inventory value;

        #region Basic Controls
        public override void Construct(string name, object value)
        {
            this.name = name;
            SetValue(value);
        }

        public override void SetValue(object value)
        {
            this.value = value as Inventory;
        }

        public override object GetValue()
        {
            return value;
        }

        public override G_State Clone()
        {
            G_Inventory clone = An.InventoryState(name).WithInventory(value);
            return clone;
        }

        #endregion

        #region Testing Controls
        /// <summary>
        /// Tests the given state agaisnt the expectedValue using the chosen comparison, returning true if the comparison
        /// is correct and false if not
        /// </summary>
        /// <param name="state"></param>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public override bool TestState(G_State state, G_StateComparison comparison, object expectedValue)
        {
            bool success = false;     
            
            object stateValue = state.GetValue();

            if(CanTestState(stateValue, expectedValue))
            {
                success = TestInventoryState(stateValue, comparison, expectedValue);
            }

            return success;
        }

        bool TestInventoryState(object stateValue, G_StateComparison comparison, object expectedValue)
        {
            bool success = false;
            ItemStack expectedStack = expectedValue as ItemStack;
            Inventory testInventory = stateValue as Inventory;

            ItemStack inventoryStack = testInventory.FindInInventory(expectedStack.item);

            if (inventoryStack != null)
            {
                success = G_NumberConditionComparer.TestVaules(inventoryStack.quantity, comparison, expectedStack.quantity);
            }
            else if (NullStackIsEqualToZero(inventoryStack, comparison, expectedStack))
            {
                success = true;
            }

            return success;
        }

        /// <summary>
        /// This function returns true if the two given conditions match their states, expected values, and comparisons
        /// </summary>
        /// <returns></returns>
        public override bool TestStateConditionMatch(G_Condition precondition, G_Condition effect)
        {
            return false;
        }

        /// <summary>
        /// Returns true if the state type has an implementation for the given comparison type
        /// </summary>
        /// <returns></returns>
        public override bool StateSupportsComparion(G_StateComparison comparison)
        {
            return comparison == G_StateComparison.equal
                || comparison == G_StateComparison.greater
                || comparison == G_StateComparison.lesser
                || comparison == G_StateComparison.greater_or_equal
                || comparison == G_StateComparison.lesser_or_equal;
        }

        /// <summary>
        /// Tests if the given value is of the same type as the value stored in this state and returns true if it is
        /// </summary>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public override bool TestValueMatch(object testValue)
        {
            return testValue is ItemStack || testValue is Inventory;
        }

        #endregion

        #region Conditions

        bool CanTestState(object stateValue, object expectedValue)
        {
            return TestValueMatch(stateValue) && TestValueMatch(expectedValue)
                && (expectedValue as ItemStack).item != null;
        }

        bool NullStackIsEqualToZero(ItemStack inventoryStack, G_StateComparison comparison, ItemStack expectedStack)
        {
            return inventoryStack == null
                && comparison == G_StateComparison.equal
                && expectedStack.quantity == 0;
        }

        #endregion
    }
}
