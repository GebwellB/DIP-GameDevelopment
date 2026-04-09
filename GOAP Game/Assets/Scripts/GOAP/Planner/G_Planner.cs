using UnityEngine;
using GOAP;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GOAP
{
    public static class G_Planner
    {
        public static bool GeneratePlan(G_Goal goal, G_WorldState worldState, out List<G_Action> plan)
        {
            bool success = false;
            plan = new List<G_Action>();

            // Initialise the node pool
            List<G_Node> nodePool = new List<G_Node>();

            // Create a node for the goal
            G_Node rootNode = new G_Node(worldState.actionPool, goal.goalEffects, worldState);

            // Add node to node pool
            nodePool.Add(rootNode);

            G_Node currentNode = null;
            int poolCounter = 0;
            // While plan not found
            while (true)
            {
                poolCounter++;
                //Debug.Log($"Iteration: {poolCounter} ================================================================");
                currentNode = nodePool[0];
                currentNode.ProcessNode();

                if (currentNode.NodeState == G_NodeState.success)
                {
                    success = true;
                    plan = currentNode.ReturnPlan();
                    if (plan == null)
                    {
                        success = false;
                    }
                    break;
                }
                else if (currentNode.NodeState == G_NodeState.failed)
                {
                    success = false;
                    break;
                }
                else if (currentNode.NodeState == G_NodeState.closed)
                {
                    nodePool.AddRange(currentNode.GenerateChildNodes());
                    nodePool = SortPool(nodePool);

                    //for (int i = 0; i < nodePool.Count; i++)
                    //{
                    //    if (nodePool[i].IsGoalNode)
                    //    {
                    //        Debug.Log($"Node {i} is a goal node");
                    //    }
                    //    else
                    //    {
                    //        Debug.Log($"Node: {i}: {nodePool[i].NodeAction.name}, State: {nodePool[i].NodeState}, Cost: {nodePool[i].HCost}");
                    //    }
                    //}

                    if (nodePool[0].NodeState != G_NodeState.open)
                    {
                        success = false;
                        break;
                    }
                }
            }

            return success;
        }

        public static List<G_Node> SortPool(List<G_Node> pool)
        {
            return pool.OrderBy((node) => node.NodeState)
                .ThenBy((node) => node.HCost)
                .ToList();
        }
    }
}

