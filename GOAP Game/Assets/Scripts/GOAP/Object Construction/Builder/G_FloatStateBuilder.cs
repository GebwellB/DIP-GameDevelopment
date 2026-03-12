using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_FloatStateBuilder
    {
        #region Basic Values
        string name = "";
        float value;

        public G_FloatStateBuilder()
        {

        }
        #endregion

        #region WithFunctions
        public G_FloatStateBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public G_FloatStateBuilder WithValue(float value)
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
        public G_FloatState Build()
        {
            G_FloatState state = ScriptableObject.CreateInstance<G_FloatState>();
            state.Construct(name, value);
            return state;
        }

        public static implicit operator G_FloatState(G_FloatStateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
