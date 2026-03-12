using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class BuilderTemplate
    {
        #region Basic Values
        string name = "";

        public BuilderTemplate()
        {

        }
        #endregion

        #region WithFunctions
        public BuilderTemplate WithName(string name)
        {
            this.name = name;
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public object Build()
        {
            object returnedObject = name;
            return returnedObject;
        }

        //public static implicit operator object(BuilderTemplate builder)
        //{
        //    return builder.Build();
        //}

        #endregion
    }
}
