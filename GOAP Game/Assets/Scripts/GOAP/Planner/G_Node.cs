using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GOAP
{
    public class G_Node
    {
        #region Data

        G_NodeState nodeState = G_NodeState.open;

        public G_NodeState NodeState
        {
            get {  return nodeState; }
        }

        G_Action nodeAction;
        public G_Action NodeAction
        { get {  return nodeAction; } }

        G_Node parentNode;
        public G_Node ParentNode
        { get { return parentNode; } }

        int hCost = 0;
        public int HCost
        { get { return hCost; } }

        int unmetPreconditions = 0;
        public int UnmetPreconditions
        { 
            get { return unmetPreconditions; }
        }

        List<G_Action> nodeActionPool = new List<G_Action>();

        public List<G_Condition> preconditions = new List<G_Condition>();

        #endregion

        #region Constructors



        #endregion
    }
}

