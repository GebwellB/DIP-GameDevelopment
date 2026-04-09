using GOAP;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TestTools;

public class PlannerTests
{
    [TestCase(TestName = "Standard Expected Plan")]
    //[TestCase(TestName = "Shop Plan")]
    public void LoggerPlan()
    {
        GatherWoodTestData testData = new GatherWoodTestData();
        testData.AddDataForStandardTest();
        List<G_Action> plan = new List<G_Action>();

        bool success = G_Planner.GeneratePlan(testData.npc_world_state.goals[0],
            testData.npc_world_state,
            out plan);

        for(int i = 0; i < plan.Count; i++)
        {
            Debug.Log(plan[i].name);
        }

        Assert.AreEqual(true, plan != null);
        Assert.AreEqual(true, success); 
        Assert.AreEqual(6, plan.Count);

        Assert.AreEqual("deliver_wood", plan[5].name);
        Assert.AreEqual("go_to_woodstock", plan[4].name);
        Assert.AreEqual("chop_tree", plan[3].name);
        Assert.AreEqual("go_to_tree", plan[2].name);
        Assert.AreEqual("take_axe", plan[1].name);
        Assert.AreEqual("go_to_workshop", plan[0].name);
    }

}
