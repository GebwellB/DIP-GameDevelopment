using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class ActionTests
{
    
    [Test]
    public void ActionCloning()
    {
        SlicedBreadData breadData = new SlicedBreadData();

        G_Action clone = breadData.slice_bread.Clone();

        Assert.AreEqual(true, clone != null);

        Assert.AreEqual(breadData.slice_bread.name, clone.name);

        Assert.AreEqual(breadData.slice_bread.preconditions.Count, clone.preconditions.Count);
        for (int i = 0; i < breadData.slice_bread.preconditions.Count; i++)
        {
            Assert.AreEqual(breadData.slice_bread.preconditions[i].State, clone.preconditions[i].State);
            Assert.AreEqual(breadData.slice_bread.preconditions[i].Comparison, clone.preconditions[i].Comparison);
            Assert.AreEqual(breadData.slice_bread.preconditions[i].ExpectedValue, clone.preconditions[i].ExpectedValue);

            Assert.AreEqual(breadData.slice_bread.preconditions[i].UseExpectedReference, clone.preconditions[i].UseExpectedReference);
            if (breadData.slice_bread.preconditions[i].UseExpectedReference)
            {
                Assert.AreEqual(breadData.slice_bread.preconditions[i].ExpectedReference, clone.preconditions[i].ExpectedReference);
            }
            else
            {
                Assert.AreEqual(breadData.slice_bread.preconditions[i].ExpectedValue, clone.preconditions[i].ExpectedValue);
            }
        }

        Assert.AreEqual(breadData.slice_bread.effects.Count, clone.effects.Count);
        for (int i = 0; i < breadData.slice_bread.effects.Count; i++)
        {
            Assert.AreEqual(breadData.slice_bread.effects[i].State, clone.effects[i].State);
            Assert.AreEqual(breadData.slice_bread.effects[i].Comparison, clone.effects[i].Comparison);
            Assert.AreEqual(breadData.slice_bread.effects[i].ExpectedValue, clone.effects[i].ExpectedValue);

            Assert.AreEqual(breadData.slice_bread.effects[i].UseExpectedReference, clone.effects[i].UseExpectedReference);
            if (breadData.slice_bread.effects[i].UseExpectedReference)
            {
                Assert.AreEqual(breadData.slice_bread.effects[i].ExpectedReference, clone.effects[i].ExpectedReference);
            }
            else
            {
                Assert.AreEqual(breadData.slice_bread.effects[i].ExpectedValue, clone.effects[i].ExpectedValue);
            }
        }

        Assert.AreEqual(breadData.slice_bread.GetCost(), clone.GetCost());
        Assert.AreEqual(breadData.slice_bread.GetPriority(), clone.GetPriority());
    }

    [Test]
    public void TestEffectAgaintsPreconiditions()
    {
        SlicedBreadData breadData = new SlicedBreadData();

        bool preconditionsMet = breadData.go_to_kitchen.TestEffectsAgaintsPreconditions(breadData.slice_bread.preconditions);

        Assert.AreEqual(true, preconditionsMet);
    }

    public class SlicedBreadData
    {
        public GameObject go;
        public Inventory inventoryComponent;
        // States
        public G_AtLocation at_location;
        public G_Inventory inventory;
        public G_BoolState is_able;

        // Actions
        // precondition action
        public G_Action go_to_kitchen;
        // effect action
        public G_Action slice_bread;

        // Location
        public LocationType kitchen = A.LocationType("kitchen");

        // Items
        public Item bread = An.Item("bread").IsStackable(true);
        public Item bread_knife = An.Item("bread_knife").IsStackable(false);
        public Item sliced_bread = An.Item("sliced_bread").IsStackable(true);

        public SlicedBreadData()
        {
            // Inventory data
            go = new GameObject();
            inventoryComponent = go.AddComponent<Inventory>();

            inventoryComponent.AddToInventory(new ItemStack(bread, 1));
            inventoryComponent.AddToInventory(new ItemStack(bread_knife, 1));

            // States Setup
            is_able = A.BoolState("is_able").WithValue(true);
            inventory = An.InventoryState("inventory").WithInventory(inventoryComponent);
            at_location = An.AtLocation("at_location");


            go_to_kitchen = An.Action("go_to_kitchen")
                .WithEffect(A.Condition().WithState(at_location).WithExpectedReference(kitchen));

            slice_bread = An.Action("slice_bread")
                .WithPrecondition(A.Condition()
                .WithState(inventory).WithComparison(G_StateComparison.greater).WithExpectedValue(ItemStack.EmptyStack(bread_knife)))
                .WithPrecondition(A.Condition()
                .WithState(inventory).WithComparison(G_StateComparison.greater).WithExpectedValue(ItemStack.EmptyStack(bread)))
                .WithPrecondition(A.Condition()
                .WithState(at_location).WithExpectedReference(kitchen))
                .WithPrecondition(A.Condition()
                .WithState(is_able).WithExpectedValue(true))

                .WithEffect(A.Condition()
                .WithState(inventory).WithComparison(G_StateComparison.greater).WithExpectedValue(ItemStack.EmptyStack(sliced_bread)));
        }
    }
}
