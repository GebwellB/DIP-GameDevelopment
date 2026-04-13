using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_WorldStateBuilder
    {
        #region Basic Values

        // Any vales to be transffered to the built object
        string name = "";
        List<G_State> states = new List<G_State>();
        List<G_Action> actionPool = new List<G_Action>();
        List<G_Goal> goals = new List<G_Goal>();

        public G_WorldStateBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        public G_WorldStateBuilder WithState(G_State state)
        {
            if(states == null)
            {
                states = new List<G_State>();
            } 

            states.Add(state);
            return this;
        }

        public G_WorldStateBuilder WithActions(G_Action action)
        {
            if (actionPool == null)
            {
                actionPool = new List<G_Action>();
            }

            actionPool.Add(action);
            return this;
        }

        public G_WorldStateBuilder WithGoals(G_Goal goal)
        {
            if (goals == null)
            {
                goals = new List<G_Goal>();
            }

            goals.Add(goal);
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_WorldState Build()
        {
            G_WorldState worldState = ScriptableObject.CreateInstance<G_WorldState>();
            worldState.Construct(states, actionPool, goals);
            return worldState;
        }

        public static implicit operator G_WorldState(G_WorldStateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
