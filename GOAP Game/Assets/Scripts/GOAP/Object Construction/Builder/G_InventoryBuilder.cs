using UnityEngine;

namespace GOAP
{
    /// <summary>
    /// Rename with the actual class builder name that we want to create
    /// </summary>
    public class G_InventoryBuilder
    {
        #region Basic Values
        string name = "";
        Inventory inventory;

        public G_InventoryBuilder(string name)
        {
            this.name = name;
        }
        #endregion

        #region WithFunctions
        public G_InventoryBuilder WithInventory(Inventory inventory)
        {
            this.inventory = inventory;
            return this;
        }
        #endregion

        #region ObjectCreation

        /// <summary>
        /// Replace object type with the class type we want to build
        /// </summary>
        /// <returns></returns>
        public G_Inventory Build()
        {
            G_Inventory inventoryState = ScriptableObject.CreateInstance<G_Inventory>();
            inventoryState.Construct(name, inventory);
            return inventoryState;
        }

        public static implicit operator G_Inventory(G_InventoryBuilder builder)
        {
            return builder.Build();
        }

        #endregion
    }
}
