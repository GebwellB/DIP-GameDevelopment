using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class GatherWoodTestData
    {
        public const int woodStockMax = 9999;

        #region Data and Objects

        #region Items

        public Item chopped_wood;
        public Item axe;
        public Item money;

        #endregion

        #region Location Types

        public LocationType tree;
        public LocationType workshop;
        public LocationType woodstock;
        public LocationType shop;

        #endregion

        #region GameObjects

        public GameObject npc_object;
        public GameObject tree_object;
        public GameObject workshop_object;
        public GameObject woodstock_object;
        public GameObject shop_object;

        #endregion

        #region Inventories

        public Inventory npc_inventory_component;
        public Inventory workshop_inventory_component;
        public Inventory woodstock_inventory_component;
        public Inventory tree_inventory_component;
        public Inventory shop_inventory_component;

        #endregion

        #endregion

        #region GOAP

        #region States

        public G_Inventory npc_inventory;
        public G_Inventory workshop_inventory;
        public G_Inventory woodstock_inventory;
        public G_Inventory tree_inventory;
        public G_Inventory shop_inventory;
        public G_AtLocation at_location;

        #endregion

        #region Actions

        public G_Action deliver_wood;
        public G_Action go_to_woodstock;
        public G_Action chop_tree;
        public G_Action go_to_tree;
        public G_Action take_axe;
        public G_Action go_to_workshop;
        public G_Action buy_wood;
        public G_Action go_to_shop;

        #endregion

        #region Goals

        public G_Goal gather_wood;

        #endregion

        #region World States

        public G_WorldState npc_world_state;

        #endregion

        #endregion

        public GatherWoodTestData()
        {
            #region Data Creation
            chopped_wood = An.Item("chopped_wood").IsStackable(true);
            axe = An.Item("axe").IsStackable(false);
            money = An.Item("money").IsStackable(true);

            tree = A.LocationType("tree");
            workshop = A.LocationType("workshop");
            woodstock = A.LocationType("woodstock");
            shop = A.LocationType("shop");

            #endregion

            #region Object and Component Creation

            npc_object = new GameObject();
            tree_object = new GameObject();
            workshop_object = new GameObject();
            woodstock_object = new GameObject();
            shop_object = new GameObject();

            npc_inventory_component = npc_object.AddComponent<Inventory>();
            tree_inventory_component = tree_object.AddComponent<Inventory>();
            workshop_inventory_component = workshop_object.AddComponent<Inventory>();
            woodstock_inventory_component = woodstock_object.AddComponent<Inventory>();
            shop_inventory_component = shop_object.AddComponent<Inventory>();

            #endregion

            #region State Creation

            npc_inventory = An.InventoryState("npc_inventory").WithInventory(npc_inventory_component);
            workshop_inventory = An.InventoryState("workshop_inventory").WithInventory(workshop_inventory_component);
            woodstock_inventory = An.InventoryState("woodstock_inventory").WithInventory(woodstock_inventory_component);
            tree_inventory = An.InventoryState("tree_inventory").WithInventory(tree_inventory_component);
            shop_inventory = An.InventoryState("shop_inventory").WithInventory(shop_inventory_component);
            at_location = An.AtLocation("at_location").WithLocationType(null);

            #endregion

            #region Action Generator

            deliver_wood = An.Action("deliver_wood")
                .WithPrecondition(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater_or_equal)
                    .WithExpectedValue(new ItemStack(chopped_wood, 10)))

                .WithPrecondition(A.Condition().WithState(at_location)
                    .WithExpectedValue(woodstock))


                .WithEffect(A.Condition().WithState(woodstock_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(new ItemStack(chopped_wood, woodStockMax)))

                .WithEffect(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))


                .WithCost(10);


            go_to_woodstock = An.Action("go_to_woodstock")

                .WithEffect(A.Condition().WithState(at_location)
                    .WithExpectedValue(woodstock))

                .WithCost(10)
                .WithPriority(1);


            chop_tree = An.Action("chop_tree")
                .WithPrecondition(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater)
                    .WithExpectedValue(ItemStack.EmptyStack(axe)))

                .WithPrecondition(A.Condition().WithState(at_location)
                    .WithExpectedValue(tree))


                .WithEffect(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater_or_equal)
                    .WithExpectedValue(new ItemStack(chopped_wood, 10)))

                .WithEffect(A.Condition().WithState(tree_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))


                .WithCost(10);


            go_to_tree = An.Action("go_to_tree")

                .WithEffect(A.Condition().WithState(at_location)
                    .WithExpectedValue(tree))

                .WithCost(10)
                .WithPriority(1);


            take_axe = An.Action("take_axe")
                .WithPrecondition(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(axe)))

                .WithPrecondition(A.Condition().WithState(workshop_inventory)
                    .WithComparison(G_StateComparison.greater)
                    .WithExpectedValue(ItemStack.EmptyStack(axe)))

                .WithPrecondition(A.Condition().WithState(at_location)
                    .WithExpectedValue(workshop))


                .WithEffect(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater)
                    .WithExpectedValue(ItemStack.EmptyStack(axe)))

                .WithEffect(A.Condition().WithState(workshop_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(axe)))

                .WithCost(10);


            go_to_workshop = An.Action("go_to_workshop")

               .WithEffect(A.Condition().WithState(at_location)
                   .WithExpectedValue(workshop))

               .WithCost(10)
               .WithPriority(1);


            buy_wood = An.Action("buy_wood")
                .WithPrecondition(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))

                .WithPrecondition(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater_or_equal)
                    .WithExpectedValue(new ItemStack(money, 1)))

                .WithPrecondition(A.Condition().WithState(shop_inventory)
                    .WithComparison(G_StateComparison.greater)
                    .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))

                .WithPrecondition(A.Condition().WithState(at_location)
                    .WithExpectedValue(shop))


                .WithEffect(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.greater_or_equal)
                    .WithExpectedValue(new ItemStack(chopped_wood, 10)))

                .WithEffect(A.Condition().WithState(npc_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(money)))

                .WithEffect(A.Condition().WithState(shop_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(ItemStack.EmptyStack(chopped_wood)))


                .WithCost(10);


            go_to_shop = An.Action("go_to_shop")

               .WithEffect(A.Condition().WithState(at_location)
                   .WithExpectedValue(shop))

               .WithCost(10)
               .WithPriority(1);

            #endregion

            #region Goals

            gather_wood = A.Goal("gather_wood")
                .WithTrigger(A.Condition().WithState(woodstock_inventory)
                    .WithComparison(G_StateComparison.lesser)
                    .WithExpectedValue(new ItemStack(chopped_wood, woodStockMax)))

                .WithEffect(A.Condition().WithState(woodstock_inventory)
                    .WithComparison(G_StateComparison.equal)
                    .WithExpectedValue(new ItemStack(chopped_wood, woodStockMax)))

               .WithPriority(1);

            #endregion

            #region World State

            npc_world_state = ScriptableObject.CreateInstance<G_WorldState>();

            npc_world_state.states.Add(npc_inventory);
            npc_world_state.states.Add(workshop_inventory);
            npc_world_state.states.Add(woodstock_inventory);
            npc_world_state.states.Add(tree_inventory);
            npc_world_state.states.Add(shop_inventory);
            npc_world_state.states.Add(at_location);

            npc_world_state.actionPool.Add(deliver_wood);
            npc_world_state.actionPool.Add(go_to_woodstock);
            npc_world_state.actionPool.Add(chop_tree);
            npc_world_state.actionPool.Add(go_to_tree);
            npc_world_state.actionPool.Add(take_axe);
            npc_world_state.actionPool.Add(go_to_workshop);
            npc_world_state.actionPool.Add(buy_wood);
            npc_world_state.actionPool.Add(go_to_shop);

            npc_world_state.goals.Add(gather_wood);

            #endregion
        }

        public void AddDataForTest(bool hasShop)
        {
            workshop_inventory_component.AddToInventory(new ItemStack(axe, 1));
            tree_inventory_component.AddToInventory(new ItemStack(chopped_wood, 10));
            if (hasShop)
            {
                shop_inventory_component.AddToInventory(new ItemStack(chopped_wood, 10));
                npc_inventory_component.AddToInventory(new ItemStack(money, 1));
            }
        }
    }
}
