using UnityEngine;
using GOAP;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace GOAP
{
    public class G_Planner
    {
        public static bool GeneratePlan(G_Goal goal, G_WorldState worldState, out List<G_Action> plan)
        {
            bool success = false;
            plan = new List<G_Action>();


            // Initialise the node pool
            // Create a node for the goal
            // Add node to the node pool

            //while plan not found
            //  find cheapest node
            //  process node
            //  if node is successful
            //      return plan
            //      break from loop
            //  else if plan failed
            //      return empty plan
            //      break from loop
            //  else
            //      generate child nodes
            //      continue loop

            return success;
        }
    }
}

