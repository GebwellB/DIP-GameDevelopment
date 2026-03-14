using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using GOAP;

public class AtLocationTests
{
    [Test]
    public void AtLocationClone()
    {
        
    }

    [TestCase(false, false, true, TestName = "State Tree vs Expected Tree")]
    [TestCase(true, true, true, TestName = "State Null vs Expected Null")]
    [TestCase(false, true, false, TestName = "State Tree vs Expected Null")]
    [TestCase(true, false, false, TestName = "State Null vs Expected Tree")]
    public void AtLocationState(bool nullStateLocation, bool nullExpectedLocation, bool expectedResult)
    {
        LocationType tree = A.LocationType("tree");

        LocationType stateLocation = tree;

        LocationType expectedLocation = tree;

        if (nullStateLocation)
        {
            stateLocation = null;
        }
        if (nullExpectedLocation)
        {
            expectedLocation = null;
        }

        G_AtLocation at_location = An.AtLocation().WithName("at_location").WithLocationType(stateLocation);

        bool result = at_location.TestState(at_location, G_StateComparison.equal, expectedLocation);

        Assert.AreEqual(expectedResult, result);
    }

    [TestCase(true, TestName = "Equals tree vs Equals tree")]
    [TestCase(true, TestName = "Equals null vs Equals null")]
    [TestCase(false, TestName = "Equals null vs Equals tree")]
    [TestCase(false, TestName = "Equals tree vs Equals null")]

    [TestCase(false, TestName = "Greater tree vs Equals tree")]
    [TestCase(false, TestName = "Greater null vs Equals null")]
    public void AtLocationConditionCompare(bool expectedResult)
    {
        
    }

}
