using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class ItemBuilder
    {
        #region Basic Values
        string name = "";
        bool stackable = true;

        public ItemBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions

        public ItemBuilder IsStackable(bool stackable)
        {
            this.stackable = stackable;
            return this;
        }
        //public ItemBuilder WithName(string name)
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
        public Item Build()
        {
            Item item = ScriptableObject.CreateInstance<Item>();
            item.stackable = stackable;
            return item;
        }

        public static implicit operator Item(ItemBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
