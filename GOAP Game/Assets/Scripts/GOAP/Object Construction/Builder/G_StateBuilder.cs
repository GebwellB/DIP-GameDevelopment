using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_StateBuilder
    {
        #region Basic Values
        string name = "";
        bool isLocal = false;
        object value = null;

        public G_StateBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        public G_StateBuilder WithValue(object value)
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
        public G_State Build()
        {
            G_State state = ScriptableObject.CreateInstance<G_State>();
            state.Construct(this.name, this.value, this.isLocal);
            return state;
        }

        public static implicit operator G_State(G_StateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
