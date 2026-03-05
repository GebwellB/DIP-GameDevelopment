using UnityEngine;

namespace GOAP
{
    public class G_BoolStateBuilder
    {
        #region Basic Values
        string name = "";
        bool value = false;

        public G_BoolStateBuilder()
        {

        }
        #endregion

        #region WithFunctions
        public G_BoolStateBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public G_BoolStateBuilder WithValue(bool value)
        {
            this.value = value;
            return this;
        }
        #endregion

        #region ObjectCreation

        public G_BoolState Build()
        {
            G_BoolState state = ScriptableObject.CreateInstance<G_BoolState>();
            state.Construct(name, value);
            return state;
        }

        public static implicit operator G_BoolState(G_BoolStateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}

