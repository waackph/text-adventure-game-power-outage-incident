using System;
using System.Collections.Generic;
namespace text_adventure
{
    /// <summary>Class <c>DialogUtility</c> implements a response UI system for player interaction.
    /// </summary>
    public class DialogUtility
    {
        public static void ContinueText(){
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static string InteractionText(){
            Console.ForegroundColor = ConsoleColor.Blue;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }

        public static int GetListItemIndex(string itemName, List<Item> list){
            bool itemExists = false;
            int idx = -1;
            for(idx = 0; idx < list.Count; idx++){
                if(list[idx].GetName().ToLower() == itemName){
                    itemExists = true;
                    break;
                }
            }
            if(itemExists){
                return idx;
            }
            else{
                return -1;
            }
        }

        public static Item GetRoomInventoryItemIndex(string itemName, List<Item> list){
            // Check room item list
            int idx = DialogUtility.GetListItemIndex(itemName, list);

            // if not in room, check inventory item list
            if(idx == -1){
                idx = DialogUtility.GetListItemIndex(itemName, Inventory.Items);
                if(idx == -1){
                    return null;
                }
                else{
                    return Inventory.Items[idx];
                }
            }
            else{
                return list[idx];
            }
        }
    }
}