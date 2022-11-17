using System.Collections.Generic;
using System;
namespace text_adventure
{
    /// <summary>Class <c>Inventory</c> holds data of Items that have been picked up by the player.
    /// </summary>
    public static class Inventory
    {
        public static List<Item> Items;

        public static List<Item> GetItems(){
            return Items;
        }

        public static void AddItem(Item item){
            Items.Add(item);
        }

        public static void RemoveItem(Item item){
            Items.Remove(item);
        }

        public static void PrintItems(){
            if(Items.Count == 0){
                Console.WriteLine("There are no Items in Nicks Inventory");
            }
            else{
                Console.WriteLine("There are the following Items in Nicks Inventory:");
                foreach (Item item in Items){
                    Console.WriteLine(item.Name);  // + ": " + item.Description);
                }
            }
        }

        public static bool IsInInventory(string itemName){
            foreach(Item item in Items){
                if(item.Name == itemName){
                    return true;
                }
            }
            return false;
        }
    }
}