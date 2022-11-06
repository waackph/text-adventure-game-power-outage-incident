using System;

namespace text_adventure
{
     // This is more like an InteractionHandler (design is changing too often, so I will leave it as it is for now)
     // There is only interaction with items - the item has dependencies with a room, itself or other objects
   public static class Player
    {

        // Function shows text of inventory
        public static void ListInventory(){
            Inventory.PrintItems();
        }

        // Function shows text of room
        public static void LookAround(Room room){
            Console.WriteLine(room.lookAroundText);
            foreach(Item item in room.Items){
                Console.WriteLine(item.GetDescription());
            }
        }

        // Function shows text of Room
        public static void Walk(Room room, string direction){
            Console.WriteLine("Nick can't do that.");
        }

        // Function shows text if item is found (either in inventory or given room)
        public static void Examine(Room room, string itemName){
            int idx = DialogUtility.GetListItemIndex(itemName, Inventory.Items);

            if(idx != -1){
                Console.WriteLine(Inventory.Items[idx].examineText);
            }
            else{
                idx = DialogUtility.GetListItemIndex(itemName, room.Items);
                if(idx != -1){
                    Console.WriteLine(room.Items[idx].examineText);
                }
                else{
                    Console.WriteLine("There is no such item");
                }
            }
        }

        // Function handles pickup of item if found in room
        public static void PickUp(Room room, string itemName){
            int idx = DialogUtility.GetListItemIndex(itemName, room.Items);

            if(idx != -1){
                if(room.Items[idx].isPickupAble){
                    Console.WriteLine("Nick picks up the " + room.Items[idx].GetName());
                    Inventory.AddItem(room.Items[idx]);
                    room.Items.RemoveAt(idx);
                }
                else{
                    Console.WriteLine("Nick can't pick that up");
                }
            }
            else{
                Console.WriteLine("There is no such item");
            }
        }

        // Function handles use of an item and possible modification of room/items
        public static void Use(Room room, string itemName){
            int idx = DialogUtility.GetListItemIndex(itemName, room.Items);

            if(idx == -1){
                idx = DialogUtility.GetListItemIndex(itemName, Inventory.Items);
                if(idx == -1){
                    Console.WriteLine("There is no such item");
                }
                else if(Inventory.Items[idx].isUseAble){
                    //Console.WriteLine(Inventory.Items[idx].useText);
                    Inventory.Items[idx].UseAction();
                }
                else{
                    Console.WriteLine("Nick can't use this item.");
                }
            }
            else{
                if(room.Items[idx].isUseAble){
                    if(room.Items[idx].isPickupAble){
                        PickUp(room, itemName);
                        idx = DialogUtility.GetListItemIndex(itemName, Inventory.Items);
                        //Console.WriteLine(Inventory.Items[idx].useText);
                        Inventory.Items[idx].UseAction();
                    }
                    else{
                        //Console.WriteLine(room.Items[idx].useText);
                        room.Items[idx].UseAction();
                    }
                }
                else{
                    Console.WriteLine("Nick can't use this item.");
                }
            }
        }

        public static void Combine(Room room, string itemName1, string itemName2){
            // Test if Items are existent in the current context (room or inventory)
            // Test if one of the items is combineable
            // Test if both pickupable => pick, combine and then pick combined item up
            Item item1 = DialogUtility.GetRoomInventoryItemIndex(itemName1, room.Items);
            Item item2 = DialogUtility.GetRoomInventoryItemIndex(itemName2, room.Items);
            if(item1 == null || item2 == null){
                Console.WriteLine("Nick can't combine items that does not exist in his environment.");
            }
            else{
                if(!item1.isCombineable & !item2.isCombineable){
                    Console.WriteLine("Nick can't combine that.");
                }
                else{
                    PuzzleItem puzzleItem = null;
                    Item normalItem = null;
                    if(item1.isCombineable){
                        puzzleItem = (PuzzleItem) item1;
                        normalItem = item2;
                    }
                    else{
                        puzzleItem = (PuzzleItem) item2;
                        normalItem = item1;
                    }
                    // PickUp combined item
                    if(puzzleItem.isPickupAble){
                        if(!Inventory.IsInInventory(puzzleItem.Name)){
                            PickUp(room, puzzleItem.Name);
                        }
                    }
                    // Execute Combine Action
                    bool iscombined = puzzleItem.combineAction(normalItem.Name);
                    // Remove the normal item from game world, because it isn't important anymore
                    if(iscombined){
                        if(Inventory.IsInInventory(normalItem.Name)){
                            Inventory.RemoveItem(normalItem);
                        }
                        else{
                            room.Items.Remove(normalItem);
                        }
                    }
                } 
            }
        }

        public static void Give(Room room, string characterName, string itemName){
            Character character = room.GetRoomCharacter(characterName);
            if(character == null){
                Console.WriteLine("There is no such character");
            }
            else{
                int idx = DialogUtility.GetListItemIndex(itemName, room.Items);
                if(idx == -1){
                    idx = DialogUtility.GetListItemIndex(itemName, Inventory.Items);
                    if(idx == -1){
                        Console.WriteLine("There is no such item");
                    }
                    else{
                        character.giveAction(Inventory.Items[idx]);
                    }
                }
                else{
                    character.giveAction(room.Items[idx]);
                }
            }
            
        }

        public static void TalkTo(Room room, string characterName){
            Character character = room.GetRoomCharacter(characterName);
            character.traverseDialog(character.GetDialogTreeRoot());
        }
    }
}