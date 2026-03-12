using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_ConditionBuilder
    {
        #region Basic Values
        G_State state;
        G_StateComparison comparison = G_StateComparison.equal;
        object expectedValue;
        bool met = false;

        public G_ConditionBuilder()
        {

        }
        #endregion

        #region WithFunctions
        public G_ConditionBuilder WithState(G_State state)
        {
            this.state = state;
            return this;
        }

        public G_ConditionBuilder WithComparison(G_StateComparison comparison)
        {
            this.comparison = comparison;
            return this;
        }

        public G_ConditionBuilder WithExpectedValue(object expectedValue)
        {
            this.expectedValue = expectedValue;
            return this;
        }

        public G_ConditionBuilder WithMet(bool met)
        {
            this.met = met;
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Condition Build()
        {
            G_Condition condition = new G_Condition(state, expectedValue, comparison, met);
            return condition;
        }

        public static implicit operator G_Condition(G_ConditionBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
