using UnityEngine;

namespace GOAP
{
    public class G_BoolStateBuilder
    {
        #region Basic Values
        string name = "";
        bool value = false;
        bool isLocal = false;

        public G_BoolStateBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        //public G_BoolStateBuilder WithName(string name)
        //{
        //    this.name = name;
        //    return this;
        //}

        public G_BoolStateBuilder WithValue(bool value)
        {
            this.value = value;
            return this;
        }

        public G_BoolStateBuilder IsLocal(bool isLocal)
        {
            this.isLocal = isLocal;
            return this;
        }
        #endregion

        #region ObjectCreation

        public G_BoolState Build()
        {
            G_BoolState state = ScriptableObject.CreateInstance<G_BoolState>();
            state.Construct(name, value, isLocal);
            return state;
        }

        public static implicit operator G_BoolState(G_BoolStateBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}

