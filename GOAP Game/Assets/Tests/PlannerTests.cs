using GOAP;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TestTools;

public class PlannerTests
{
    [TestCase(false, TestName = "Standard Expected Plan")]
    [TestCase(true, TestName = "Shop Plan")]
    public void LoggerPlan(bool useShopPlan)
    {
        GatherWoodTestData testData = new GatherWoodTestData();
        testData.AddDataForTest(useShopPlan);
        List<G_Action> plan = new List<G_Action>();

        bool success = G_Planner.GeneratePlan(testData.npc_world_state.goals[0],
            testData.npc_world_state,
            out plan);

        for (int i = 0; i < plan.Count; i++)
        {
            Debug.Log(plan[i].name);
        }

        // ALWAYS REMEMBER TO CHECK DATA BEFORE SPENDING HOURS DIGGING THROUGH CODE.
        // IT'S ALWAYS THE DATA THAT I BREAK.

        if (useShopPlan)
        {
            Assert.AreEqual(true, plan != null);
            Assert.AreEqual(true, success);
            Assert.AreEqual(4, plan.Count);

            Assert.AreEqual("deliver_wood", plan[3].name);
            Assert.AreEqual("go_to_woodstock", plan[2].name);
            Assert.AreEqual("buy_wood", plan[1].name);
            Assert.AreEqual("go_to_shop", plan[0].name);
        }
        else
        {
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

}
