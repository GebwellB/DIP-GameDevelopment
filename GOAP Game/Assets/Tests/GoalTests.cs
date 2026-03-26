using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class GoalTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void CloneTest()
    {
        ActionTests.SlicedBreadData breadData = new ActionTests.SlicedBreadData();

        G_Goal slice_bread = A.Goal("slice_bread")
            .WithTrigger(A.Condition().WithState(breadData.inventory)
                .WithComparison(G_StateComparison.equal)
                .WithExpectedValue(ItemStack.EmptyStack(breadData.sliced_bread)))
            .WithEffect(A.Condition().WithState(breadData.inventory)
                .WithComparison(G_StateComparison.greater)
                .WithExpectedValue(ItemStack.EmptyStack(breadData.sliced_bread)))
            .WithPriority(0);

        G_Goal clone = slice_bread.Clone();

        Assert.AreEqual(true, clone != null);

        Assert.AreEqual(slice_bread.name, clone.name);

        Assert.AreEqual(true, slice_bread.triggerConditions.Count > 0);
        Assert.AreEqual(true, clone.triggerConditions.Count > 0);

        Assert.AreEqual(slice_bread.triggerConditions.Count, clone.triggerConditions.Count);
        for (int i = 0; i < slice_bread.triggerConditions.Count; i++)
        {
            Assert.AreEqual(slice_bread.triggerConditions[i].State, clone.triggerConditions[i].State);
            Assert.AreEqual(slice_bread.triggerConditions[i].Comparison, clone.triggerConditions[i].Comparison);
            Assert.AreEqual(slice_bread.triggerConditions[i].ExpectedValue, clone.triggerConditions[i].ExpectedValue);
        }

        Assert.AreEqual(true, slice_bread.goalEffects.Count > 0);
        Assert.AreEqual(true, clone.goalEffects.Count > 0);

        Assert.AreEqual(slice_bread.goalEffects.Count, clone.goalEffects.Count);
        for (int i = 0; i < slice_bread.goalEffects.Count; i++)
        {
            Assert.AreEqual(slice_bread.goalEffects[i].State, clone.goalEffects[i].State);
            Assert.AreEqual(slice_bread.goalEffects[i].Comparison, clone.goalEffects[i].Comparison);
            Assert.AreEqual(slice_bread.goalEffects[i].ExpectedValue, clone.goalEffects[i].ExpectedValue);
        }

        Assert.AreEqual(slice_bread.priority, clone.priority);
    }
}
