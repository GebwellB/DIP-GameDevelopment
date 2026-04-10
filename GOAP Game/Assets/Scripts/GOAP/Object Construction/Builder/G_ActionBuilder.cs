using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_ActionBuilder
    {
        #region Basic Values
        string name = "";
        List<G_Condition> preconditions = new List<G_Condition>();
        List<G_Condition> effects = new List<G_Condition>();
        int cost = 10;
        int priority = 0;

        public G_ActionBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        public G_ActionBuilder WithCost(int cost)
        {
            this.cost = cost;
            return this;
        }

        public G_ActionBuilder WithPriority(int priority)
        {
            this.priority = priority;
            return this;
        }

        public G_ActionBuilder WithPrecondition(G_Condition precondition)
        {
            if(preconditions == null)
            {
                preconditions = new List<G_Condition>();
            }
            preconditions.Add(precondition);
            return this;
        }

        public G_ActionBuilder WithEffect(G_Condition effect)
        {
            if (effects == null)
            {
                effects = new List<G_Condition>();
            }
            effects.Add(effect);
            return this;
        }

        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Action Build()
        {
            G_Action action = ScriptableObject.CreateInstance<G_Action>();
            action.Construct(name, preconditions, effects, cost, priority);
            return action;
        }

        public static implicit operator G_Action(G_ActionBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
