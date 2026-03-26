using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_GoalBuilder
    {
        #region Basic Values
        string name = "";
        int priority = 0;

        List<G_Condition> triggers = new List<G_Condition>();
        List<G_Condition> effects = new List<G_Condition>();

        public G_GoalBuilder(string name)
        {
            this.name = name;

        }
        #endregion

        #region WithFunctions
        public G_GoalBuilder WithPriority(int priority)
        {
            this.priority = priority;
            return this;
        }
        public G_GoalBuilder WithTrigger(G_Condition trigger)
        {
            if (triggers == null)
            {
                triggers = new List<G_Condition>();
            }
            triggers.Add(trigger);
            return this;
        }

        public G_GoalBuilder WithEffect(G_Condition effect)
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
        public G_Goal Build()
        {
            G_Goal goal = ScriptableObject.CreateInstance<G_Goal>();
            goal.Construct(name, triggers, effects, priority);
            return goal;
        }

        public static implicit operator G_Goal(G_GoalBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
