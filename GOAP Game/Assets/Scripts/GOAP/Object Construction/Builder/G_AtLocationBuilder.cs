using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_AtLocationBuilder
    {
        #region Basic Values
        string name = "";
        LocationType value;
        bool isLocal = false;

        public G_AtLocationBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        //public G_AtLocationBuilder WithName(string name)
        //{
        //    this.name = name;
        //    return this;
        //}

        public G_AtLocationBuilder WithLocationType(LocationType value)
        {
            this.value = value;
            return this;
        }

        public G_AtLocationBuilder IsLocal(bool isLocal)
        {
            this.isLocal = isLocal;
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_AtLocation Build()
        {
            G_AtLocation state = ScriptableObject.CreateInstance<G_AtLocation>();
            state.Construct(name, value, isLocal);
            return state;
        }

        public static implicit operator G_AtLocation(G_AtLocationBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
