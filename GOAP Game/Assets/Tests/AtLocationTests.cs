using GOAP;
using NUnit.Framework;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.TestTools;

public class AtLocationTests
{
    [Test]
    public void AtLocationClone()
    {
        LocationType tree = A.LocationType("tree");
        G_AtLocation at_location = An.AtLocation("at_location").WithLocationType(tree);

        G_AtLocation clone = at_location.Clone() as G_AtLocation;

        Assert.AreEqual(at_location.name, clone.name);
        Assert.AreEqual(at_location.GetValue() as LocationType, clone.GetValue() as LocationType);
    }

    [TestCase(true, true, true, TestName = "State Tree vs Expected Tree")]
    [TestCase(false, false, true, TestName = "State Null vs Expected Null")]
    [TestCase(true, false, false, TestName = "State Tree vs Expected Null")]
    [TestCase(false, true, false, TestName = "State Null vs Expected Tree")]
    public void AtLocationState(bool useLocationForState, bool useLocationForExpected, bool expectedResult)
    {
        LocationType tree = A.LocationType("tree");
        LocationType stateLocation = null;
        LocationType expectedLocation = null;

        if (useLocationForState)
        {
            stateLocation = tree;
        }
        if (useLocationForExpected)
        {
            expectedLocation = tree;
        }
        G_AtLocation at_location = An.AtLocation("at_location").WithLocationType(stateLocation);

        bool result = at_location.TestState(at_location, G_StateComparison.equal, expectedLocation);

        Assert.AreEqual(expectedResult, result);
    }
}

