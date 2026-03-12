using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_IntStateBuilder
    {
        #region Basic Values
        string name = "";
        int value;

        public G_IntStateBuilder()
        {

        }
        #endregion

        #region WithFunctions
        public G_IntStateBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public G_IntStateBuilder WithValue(int value)
        {
            this.value = value;
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_IntState Build()
        {
            G_IntState state = ScriptableObject.CreateInstance<G_IntState>();
            state.Construct(name, value);
            return state;
        }

        public static implicit operator G_IntState(G_IntStateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
