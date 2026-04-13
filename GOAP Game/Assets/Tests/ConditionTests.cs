using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class ConditionTests
{
    #region Conditon Functions

    [TestCase(false, TestName = "Use Internal State")]
    [TestCase(true, TestName = "Use Parameter State")]
    public void DoesStateMeetCondition(bool useParameter)
    {
        G_BoolState boolState = A.BoolState("test").WithValue(true);

        G_Condition condition = A.Condition()
            .WithState(boolState)
            .WithComparison(G_StateComparison.equal)
            .WithExpectedValue(true);

        bool result = false;

        if (useParameter)
        {
            result = condition.DoesStateMeetCondition(boolState);
        }
        else
        {
            result = condition.DoesStateMeetCondition();
        }

        Assert.IsTrue(result);
    }

    #endregion

    #region Bool Conditions

    // Equals
    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, true, true, TestName = "Equal True vs Equal True")]
    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, false, false, TestName = "Equal True vs Equal False")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, true, false, TestName = "Equal False vs Equal True")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, false, true, TestName = "Equal False vs Equal False")]

    // Not Equals
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.not_equal, true, true, TestName = "Not Equal True vs Not Equal True")]
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.not_equal, false, false, TestName = "Not Equal True vs Not Equal False")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.not_equal, true, false, TestName = "Not Equal False vs Not Equal True")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.not_equal, false, true, TestName = "Not Equal False vs Not Equal False")]

    // Equals vs Not Equal
    [TestCase(G_StateComparison.equal, true, G_StateComparison.not_equal, true, false, TestName = "Equal True vs Not Equal True")]
    [TestCase(G_StateComparison.equal, true, G_StateComparison.not_equal, false, true, TestName = "Equal True vs Not Equal False")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.not_equal, true, true, TestName = "Equal False vs Not Equal True")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.not_equal, false, false, TestName = "Equal False vs Not Equal False")]

    // Not Equal vs Not Equal
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.equal, true, false, TestName = "Not Equal True vs Equal True")]
    [TestCase(G_StateComparison.not_equal, true, G_StateComparison.equal, false, true, TestName = "Not Equal True vs Equal False")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.equal, true, true, TestName = "Not Equal False vs Equal True")]
    [TestCase(G_StateComparison.not_equal, false, G_StateComparison.equal, false, false, TestName = "Not Equal False vs Equal False")]
    public void CompareConditionToEffect_Bool(
        G_StateComparison preComparison,
        bool preExpectedValue,
        G_StateComparison effectComparison,
        bool effectExpectedValue,
        bool expectedResult)
    {
        // Arrange
        G_BoolState boolState = A.BoolState("test").WithValue(true);

        G_Condition preCondition = A.Condition()
            .WithState(boolState)
            .WithComparison(preComparison)
            .WithExpectedValue(preExpectedValue);

        G_Condition effect = A.Condition()
            .WithState(boolState)
            .WithComparison(effectComparison)
            .WithExpectedValue(effectExpectedValue);

        // Act
        bool result = preCondition.CompareConditionToEffect(effect);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion

    #region Float Coniditons

    #region Equals

    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Equal to 5 vs Pre Equal 5")]
    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 4, false, TestName = "Pre Equal to 5 vs Pre Equal 4")]

    #endregion

    #region Greater

    #region Vs Equal

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 4, false, TestName = "Pre Greater than 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 5, false, TestName = "Pre Greater than 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 6, true, TestName = "Pre Greater than 5 vs Effect Equal 6")]

    #endregion

    #region Vs Greater

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 4, false, TestName = "Pre Greater than 5 vs Effect Greater than 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 5, true, TestName = "Pre Greater than 5 vs Effect Greater than 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 6, true, TestName = "Pre Greater than 5 vs Effect Greater than 6")]

    #endregion

    #region Vs Greater Or Equal

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 4, false, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 5, false, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 6, true, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 6")]

    #endregion

    #endregion

    #region Lesser

    #region Vs Equal

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 4, true, TestName = "Pre Less than 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 5, false, TestName = "Pre Less than 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 6, false, TestName = "Pre Less than 5 vs Effect Equal 6")]

    #endregion

    #region Vs Lesser

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 4, true, TestName = "Pre Less than 5 vs Effect Less than 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 5, true, TestName = "Pre Less than 5 vs Effect Less than 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 6, false, TestName = "Pre Less than 5 vs Effect Less than 6")]

    #endregion

    #region Vs Lesser Or Equal

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 4, true, TestName = "Pre Less than 5 vs Effect Less than or Equal to 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 5, false, TestName = "Pre Less than 5 vs Effect Less than or Equal to 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 6, false, TestName = "Pre Less than 5 vs Effect Less than or Equal to 6")]

    #endregion

    #endregion

    #region Greater Than or Equal

    #region Vs Equal

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 4, false, TestName = "Pre Greater than or Equal 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Equal 6")]

    #endregion

    #region Vs Greater

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 4, false, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 6")]

    #endregion

    #region Vs Greater Or Equal

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 4, false, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 6")]

    #endregion

    #endregion

    #region Lesser Than or Equal

    #region Vs Equal

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 6, false, TestName = "Pre Less than or Equal to 5 vs Effect Equal 6")]

    #endregion

    #region Vs Lesser

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 6, false, TestName = "Pre Less than or Equal to 5 vs Effect Less than 6")]

    #endregion

    #region Vs Lesser Or Equal

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 6, false, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 6")]

    #endregion

    #endregion


    public void CompareConditionToEffect_Float(
        G_StateComparison preComparison,
        float preExpectedValue,
        G_StateComparison effectComparison,
        float effectExpectedValue,
        bool expectedResult)
    {
        // Arrange
        G_FloatState floatState = A.FloatState("test").WithValue(5);

        G_Condition preCondition = A.Condition()
            .WithState(floatState)
            .WithComparison(preComparison)
            .WithExpectedValue(preExpectedValue);

        G_Condition effect = A.Condition()
            .WithState(floatState)
            .WithComparison(effectComparison)
            .WithExpectedValue(effectExpectedValue);

        // Act
        bool result = preCondition.CompareConditionToEffect(effect);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion

    #region Int Conditions

    #region Equals

    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Equal to 5 vs Pre Equal 5")]
    [TestCase(G_StateComparison.equal, 5, G_StateComparison.equal, 4, false, TestName = "Pre Equal to 5 vs Pre Equal 4")]

    #endregion

    #region Greater

    #region Vs Equal

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 4, false, TestName = "Pre Greater than 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 5, false, TestName = "Pre Greater than 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.equal, 6, true, TestName = "Pre Greater than 5 vs Effect Equal 6")]

    #endregion

    #region Vs Greater

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 4, false, TestName = "Pre Greater than 5 vs Effect Greater than 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 5, true, TestName = "Pre Greater than 5 vs Effect Greater than 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater, 6, true, TestName = "Pre Greater than 5 vs Effect Greater than 6")]

    #endregion

    #region Vs Greater Or Equal

    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 4, false, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 4")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 5, false, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 5")]
    [TestCase(G_StateComparison.greater, 5, G_StateComparison.greater_or_equal, 6, true, TestName = "Pre Greater than 5 vs Effect Greater than or Equal to 6")]

    #endregion

    #endregion

    #region Lesser

    #region Vs Equal

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 4, true, TestName = "Pre Less than 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 5, false, TestName = "Pre Less than 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.equal, 6, false, TestName = "Pre Less than 5 vs Effect Equal 6")]

    #endregion

    #region Vs Lesser

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 4, true, TestName = "Pre Less than 5 vs Effect Less than 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 5, true, TestName = "Pre Less than 5 vs Effect Less than 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser, 6, false, TestName = "Pre Less than 5 vs Effect Less than 6")]

    #endregion

    #region Vs Lesser Or Equal

    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 4, true, TestName = "Pre Less than 5 vs Effect Less than or Equal to 4")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 5, false, TestName = "Pre Less than 5 vs Effect Less than or Equal to 5")]
    [TestCase(G_StateComparison.lesser, 5, G_StateComparison.lesser_or_equal, 6, false, TestName = "Pre Less than 5 vs Effect Less than or Equal to 6")]

    #endregion

    #endregion

    #region Greater Than or Equal

    #region Vs Equal

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 4, false, TestName = "Pre Greater than or Equal 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.equal, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Equal 6")]

    #endregion

    #region Vs Greater

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 3, false, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 3")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 4, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than 6")]

    #endregion

    #region Vs Greater Or Equal

    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 4, false, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 4")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 5, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 5")]
    [TestCase(G_StateComparison.greater_or_equal, 5, G_StateComparison.greater_or_equal, 6, true, TestName = "Pre Greater than or Equal 5 vs Effect Greater than or Equal to 6")]

    #endregion

    #endregion

    #region Lesser Than or Equal

    #region Vs Equal

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Equal 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Equal 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.equal, 6, false, TestName = "Pre Less than or Equal to 5 vs Effect Equal 6")]

    #endregion

    #region Vs Lesser

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 6, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than 6")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser, 7, false, TestName = "Pre Less than or Equal to 5 vs Effect Less than 7")]

    #endregion

    #region Vs Lesser Or Equal

    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 4, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 4")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 5, true, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 5")]
    [TestCase(G_StateComparison.lesser_or_equal, 5, G_StateComparison.lesser_or_equal, 6, false, TestName = "Pre Less than or Equal to 5 vs Effect Less than or Equal to 6")]

    #endregion

    #endregion

    public void CompareConditionToEffect_Int(
        G_StateComparison preComparison,
        int preExpectedValue,
        G_StateComparison effectComparison,
        int effectExpectedValue,
        bool expectedResult)
    {
        // Arrange
        G_IntState intState = An.IntState("test").WithValue(5);

        G_Condition preCondition = A.Condition()
            .WithState(intState)
            .WithComparison(preComparison)
            .WithExpectedValue(preExpectedValue);

        G_Condition effect = A.Condition()
            .WithState(intState)
            .WithComparison(effectComparison)
            .WithExpectedValue(effectExpectedValue);

        // Act
        bool result = preCondition.CompareConditionToEffect(effect);

        // Assert
        Assert.AreEqual(expectedResult, result);
    }

    #endregion

    #region At Location Compares


    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, true, true, TestName = "Equals tree vs Equals tree")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, false, true, TestName = "Equals null vs Equals null")]
    [TestCase(G_StateComparison.equal, false, G_StateComparison.equal, true, false, TestName = "Equals null vs Equals tree")]
    [TestCase(G_StateComparison.equal, true, G_StateComparison.equal, false, false, TestName = "Equals tree vs Equals null")]

    [TestCase(G_StateComparison.greater, true, G_StateComparison.equal, true, false, TestName = "Greater tree vs Equals tree")]
    [TestCase(G_StateComparison.greater, false, G_StateComparison.equal, false, false, TestName = "Greater null vs Equals null")]
    public void AtLocationConditionCompare(G_StateComparison preCompare,
        bool validPreExpectedValue,
        G_StateComparison effectCompare,
        bool validEffectExpectedValue,
        bool expectedResult)
    {
        LocationType tree = A.LocationType("tree");
        G_AtLocation at_location = An.AtLocation("at_location").WithLocationType(tree);
        LocationType preExpectedValue = null;
        LocationType effectExpectedValue = null;

        if (validPreExpectedValue)
        {
            preExpectedValue = tree;
        }
        if (validEffectExpectedValue)
        {
            effectExpectedValue = tree;
        }
        G_Condition preConiditon = A.Condition().WithState(at_location).WithComparison(preCompare).WithExpectedReference(preExpectedValue);

        G_Condition effect = A.Condition().WithState(at_location).WithComparison(effectCompare).WithExpectedReference(effectExpectedValue);

        bool result = preConiditon.CompareConditionToEffect(effect);

        Assert.AreEqual(expectedResult, result);
    }

    #endregion

    #region Inventory Conditions

    [TestCase(true, G_StateComparison.equal, 1, G_StateComparison.equal, 1, true, TestName = "Both equal to 1 item")]
    [TestCase(true, G_StateComparison.equal, 1, G_StateComparison.equal, 0, false, TestName = "Not Equal to Same quantity")]
    [TestCase(false, G_StateComparison.equal, 1, G_StateComparison.equal, 1, false, TestName = "Different Items")]
    [TestCase(true, G_StateComparison.greater, 0, G_StateComparison.equal, 1, true, TestName = "Equal 1 vs Greater 0")]
    public void InventoryConditionCompare(bool useSameItem,
        G_StateComparison preCompare,
        int preQuantity,
        G_StateComparison effectCompare,
        int effectQuantity,
        bool expectedResult
        )
    {
        GameObject go = new GameObject();
        Inventory inventory = go.AddComponent<Inventory>();
        Item axe = An.Item("axe").IsStackable(false);
        Item wood = An.Item("wood").IsStackable(true);

        G_Inventory testState = An.InventoryState("test").WithInventory(inventory);

        ItemStack preExpectedValue = new ItemStack(axe, preQuantity);
        ItemStack effectExpectedValue = useSameItem ? new ItemStack(axe, effectQuantity) : new ItemStack(wood, effectQuantity);

        G_Condition precondition
            = A.Condition().WithState(testState).WithComparison(preCompare).WithExpectedValue(preExpectedValue);
        G_Condition effect
            = A.Condition().WithState(testState).WithComparison(effectCompare).WithExpectedValue(effectExpectedValue);

        bool result = precondition.CompareConditionToEffect(effect);

        Assert.AreEqual(expectedResult, result);
    }

    #endregion

}

