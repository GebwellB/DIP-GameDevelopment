using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class LocationTypeBuilder
    {
        #region Basic Values
        string name = "";

        public LocationTypeBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        //public LocationTypeBuilder WithName(string name)
        //{
        //    this.name = name;
        //    return this;
        //}
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public LocationType Build()
        {
            LocationType location = ScriptableObject.CreateInstance<LocationType>();
            location.name = name;
            return location;
        }

        public static implicit operator LocationType(LocationTypeBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
