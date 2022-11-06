using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
namespace text_adventure
{
    // This is more like an InputHandler (design is changing too often, so I will leave it as it is for now)
    public static class InteractionManager
    {
        public static Room currentRoom;
        public static bool isInitilized = false;
        public static List<Room> rooms = new List<Room>();
        private static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public static void SaveState(){
            string date = DateTime.Now.ToString("yyyyMMdd-HHmm");
            string savePath = "save_states/" + date;

            File.WriteAllText(savePath+"rooms.json", JsonConvert.SerializeObject(rooms, Formatting.Indented, settings));
            File.WriteAllText(savePath+"inventory.json", JsonConvert.SerializeObject(Inventory.Items, Formatting.Indented, settings));
            File.WriteAllText(savePath+"currentRoom.json", JsonConvert.SerializeObject(currentRoom, Formatting.Indented, settings));
        }

        public static void LoadState(string date){
            string savePath = "save_states/" + date;
            rooms = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(savePath+"rooms.json"), settings);
            Inventory.Items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(savePath+"inventory.json"), settings);
            currentRoom = JsonConvert.DeserializeObject<Room>(File.ReadAllText(savePath+"currentRoom.json"), settings);
            isInitilized = true;
        }

        public static void InitilizeRoom(){
            // Initilize current room and its items
            string DarkLivingRoomName = "Living Room Dark";
            string DimmedLivingRoomName = "Living Room Dimmed";
            string BrightLivingRoomName = "Living Room Bright";
            NothingUseAction GameBoyOffAction = new NothingUseAction("The Gameboy is already turned on"); //new ChangeRoomUseAction(DimmedLivingRoomName, DarkLivingRoomName);
            ChangeRoomUseAction GameBoyOnAction = new ChangeRoomUseAction(DarkLivingRoomName, DimmedLivingRoomName);
            UsableStatefullItem Gameboy = new UsableStatefullItem("Gameboy", "As Nick looks around he steps on a hard thing. As a trained gamer Nick immediately recognizes it as a Gameboy as he feels the surfaces with his right foot.", true, "An old and greasy looking Gameboy.", new Dictionary<bool, string>(){[true] = "Nick turns on the Gameboy", [false] = "Nick turns off the Gameboy"}, GameBoyOffAction, GameBoyOnAction, new List<UsableItem>(), true);
            List<Item> DarkLivingRoomItems = new List<Item>(){Gameboy};

            string DarkLivingRoomDescr = "Nick can't see anything, not even the calming light of his gaming console.";
            string DarkLivingRoomLookAround = "It is completely dark.";
            Room livingRoomDark = new Room(DarkLivingRoomName, DarkLivingRoomDescr, DarkLivingRoomLookAround, DarkLivingRoomItems);
            rooms.Add(livingRoomDark);
            
            // Living Room Dimmed
            CodeMiniGame comboLockCodeGame = new CodeMiniGame("8765");
            MiniGameUseAction comboLockUseAction = new MiniGameUseAction("Cellar", comboLockCodeGame);
            UsableStatefullItem comboLock = new UsableStatefullItem("ComboLock", "There is a ComboLock attached to the FuseBox.", false, "It is an ComboLock with a four digit combination", new Dictionary<bool, string>(){[true] = "Nick starts turning the little digit wheels"}, null, comboLockUseAction, new List<UsableItem>(), true);
            NothingUseAction fuseBoxLockedUseAction = new NothingUseAction("Nothing happens"); 
            NothingUseAction fuseBoxUnlockedUseAction = new NothingUseAction("Some light in the cellar turns on and the room gets a tiny little bit brighter.\nFor some reason the cellar becomes even more uncomfortable for Nick now.");
            UsableStatefullItem fuseBox = new UsableStatefullItem("FuseBox", "An old FuseBox is located at the front wall of the cellar", false, "It's an old FuseBox", new Dictionary<bool, string>(){[true] = "Nick opens the FuseBox and wildly turns on and off the switches until some sound of an engine starting up is to hear", [false] = "The FuseBox door won't open. It's locked by a ComboLock"}, fuseBoxLockedUseAction, fuseBoxUnlockedUseAction, new List<UsableItem>(){comboLock}, false);
            
            ChangeRoomUseAction lightSwitchOnAction = new ChangeRoomUseAction("Living Room Dimmed", "Living Room Bright");
            NothingUseAction LightSwitchOffAction = new NothingUseAction("Nothing is happening");
            UsableItem LightSwitchOff = new UsableItem("LightSwitch", "Nick recognizes something that might be a LightSwitch.", false, "Switch to turn on the light", new Dictionary<bool, string>(){[true] = "Nick turns on the light switch", [false] = "Nick switches the light switch."}, LightSwitchOffAction, lightSwitchOnAction, new List<UsableItem>(){fuseBox}, false);
            ChangeRoomUseAction dimmedhallwayDoorOpenAction = new ChangeRoomUseAction("Living Room Dimmed", "Hallway");
            UsableItem dimmedHallwayDoor = new UsableItem("HallwayDoor", "There is a HallwayDoor on the left side of the living room", false, "The door should lead to the Hallway.", new Dictionary<bool, string>(){[true] = "Nick steps through the door."}, null, dimmedhallwayDoorOpenAction, new List<UsableItem>(){}, true);
            List<Item> DimmedLivingRoomItems = new List<Item>(){LightSwitchOff, dimmedHallwayDoor};

            string DimmedLivingRoomDescr = "The room is slightly brighter.";
            string DimmedLivingRoomLookAround = "There is only a small side of the wall to see";
            Room livingRoomDimmed = new Room(DimmedLivingRoomName, DimmedLivingRoomDescr, DimmedLivingRoomLookAround, DimmedLivingRoomItems);
            rooms.Add(livingRoomDimmed);

            // ChangeRoomUseAction lightSwitchOffAction = new ChangeRoomUseAction("Living Room Bright", "Living Room Dimmed");
            // UsableItem LightSwitchOn = new UsableItem("LightSwitch", "There is a switched on LightSwitch", false, "Switch to turn off the light", new Dictionary<bool, string>(){[true] = "Nick turns off the light switch"}, lightSwitchOffAction);

            // List<Item> BrightLivingRoomItems = new List<Item>(){LightSwitchOn, HallwayDoor};
            ChangeRoomUseAction brightHallwayDoorOpenAction = new ChangeRoomUseAction("Living Room Bright", "Hallway");
            UsableItem brightHallwayDoor = new UsableItem("HallwayDoor", "There is a HallwayDoor on the left side of the living room", false, "The door should lead to the Hallway.", new Dictionary<bool, string>(){[true] = "Nick steps through the door."}, null, brightHallwayDoorOpenAction, new List<UsableItem>(){}, true);
            List<Item> BrightLivingRoomItems = new List<Item>(){brightHallwayDoor};

            string BrightLivingRoomDescr = "The room is full of light!";
            string BrightLivingRoomLookAround = "The room looks utterly chaotic.";
            Room livingRoomBright = new Room(BrightLivingRoomName, BrightLivingRoomDescr, BrightLivingRoomLookAround, BrightLivingRoomItems);
            rooms.Add(livingRoomBright);

            // Define the hallway
            ChangeRoomUseAction brightLivingRoomDoorOpenAction = new ChangeRoomUseAction("Hallway", "Living Room Bright");
            ChangeRoomUseAction dimmedLivingRoomDoorOpenAction = new ChangeRoomUseAction("Hallway", "Living Room Dimmed");
            UsableItem LivingRoomDoor = new UsableItem("LivingRoomDoor", "There is the LivingRoomDoor on the left end of the hallway.", false, "The door should lead to the living room.", new Dictionary<bool, string>(){[true] = "Nick steps through the door.", [false] = "Nick steps through the door"}, dimmedLivingRoomDoorOpenAction, brightLivingRoomDoorOpenAction, new List<UsableItem>(){LightSwitchOff}, false);
            ChangeRoomUseAction cellarDoorOpenAction = new ChangeRoomUseAction("Hallway", "Cellar");
            PuzzleItem cellarDoor = new PuzzleItem("CellarDoor", "There is a rusty CellarDoor in the middle of the hallway", false, true, true, "It is a rusty old door. It should lead to the cellar", new Dictionary<string, bool>(){["OldKey"] = false, ["OilBottle"] = false}, new Dictionary<string, string>(){["OldKey"] = "The door is unlocked", ["OilBottle"] = "The hinges are oiled now."}, new Dictionary<string, string>(){["OldKey"] = "The door is locked", ["OilBottle"] = "The door is stuck. The hinges might be too rusty", ["solved"] = "The door finally opens."}, cellarDoorOpenAction);
            Item oldKey = new Item("OldKey", "An OldKey is laying on a small table in the left corner of the hallway", true, "The key looks nice and old. It's smelling a little bit.");
            Item oilBottle = new Item("OilBottle", "In a cupboard in the left mid side is an OilBottle", true, "It is a small oil bottle. Its really slippery.");

            List<Item> HallwayItems = new List<Item>(){LivingRoomDoor, oldKey, oilBottle, cellarDoor};

            string HallwayName = "Hallway";
            string HallwayDescr = "Its a small hallway";
            string HallwayLookAround = "The hallway is nearly empty.";
            Room Hallway = new Room(HallwayName, HallwayDescr, HallwayLookAround, HallwayItems);
            rooms.Add(Hallway);

            // Define the cellar
            ChangeRoomUseAction cellarDoorHallwayOpenAction = new ChangeRoomUseAction("Cellar", "Hallway");
            UsableItem cellarHallwayDoor = new UsableItem("HallwayDoor", "There is the HallwayDoor", false, "It is a rusty old door. It leads back to the Hallway", new Dictionary<bool, string>(){[true] = "Nick steps back into the Hallway"}, null, cellarDoorHallwayOpenAction, new List<UsableItem>(), true);
            Item cellarTable = new Item("SteelTable", "An old SteelTable is located at the end of the room. There seems to be nothing but useless stuff on it. Nothing of importance.", false, "Even though there is nothing useful on the table Nick nevertheless looks closer. How pointless...\nIs it though? To everybodys suprise Nick finds something scratched into the surface of the table. It reads: 'Don't forget 8765'");
            List<Item> CellarItems = new List<Item>(){fuseBox, comboLock, cellarTable, cellarHallwayDoor};

            string CellarName = "Cellar";
            string CellarDescr = "Its a gloomy cellar";
            string CellarLookAround = "The cellar is full of useless stuff.";
            Room Cellar = new Room(CellarName, CellarDescr, CellarLookAround, CellarItems);
            rooms.Add(Cellar);

            Inventory.Items = new List<Item>();
            currentRoom = livingRoomDark;
            isInitilized = true;
            
            StoryProgress.progressListener();
            EnterRoom();
            Console.WriteLine("What should Nick do?");
        }

        public static Room getRoomByName(string name){
            foreach(Room room in rooms){
                if (room.GetName() == name){
                    return room;
                }
            }
            return null;
        }

        public static void EnterRoom(){

            // Print Room Description
            Console.WriteLine(currentRoom.GetDescription());
            //Console.WriteLine("What should Nick do?");
        }

        public static void InteractionDialog(){

            string input = DialogUtility.InteractionText().ToLower();
            
            // Find appropriate interaction given the input
            if(input == "m" || input == "menu"){
                Menu.run = false;
            }
            else if(input == "h" || input == "help"){
                Console.WriteLine("You can enter the menu with (m)enu");
                Console.WriteLine("To interact use the keywords:");
                Console.WriteLine("List (i)nventory\nLook Around\nExamine [item]\nPickup [item]\nUse [item]\nCombine [item] with [item]"); // \nGive [Character] [item]\nTalk to [Character]");
            }
            else if(input == "i" || input == "inventory" || input == "list inventory"){
                Player.ListInventory();
            }
            else if(input == "look around"){
                Player.LookAround(currentRoom);
            }
            else if(input.Split().Length == 2){
                string[] inputSplit = input.Split(" ");
                string Action = inputSplit[0];
                string itemName = inputSplit[1];

                if(Action == "pickup"){
                    Player.PickUp(currentRoom, input.Split(" ")[1]);
                }
                else if(Action == "examine"){
                    Player.Examine(currentRoom, itemName);
                }
                else if(Action == "use"){
                    Player.Use(currentRoom, itemName);
                }
                else if(Action == "talkto"){
                    Player.TalkTo(currentRoom, itemName);
                }
                else{
                    Console.WriteLine("I don't understand.. Use (h)elp to find the interaction keywords");
                }
            }
            else if(input.Split().Length == 4){
                string[] inputSplit = input.Split(" ");
                string Action = inputSplit[0];
                string itemName1 = inputSplit[1];
                string preposition = inputSplit[2];
                string itemName2 = inputSplit[3];

                if(Action == "combine" & preposition == "with"){
                    Player.Combine(currentRoom, itemName1, itemName2);
                }
                else if(Action == "give" & preposition == "to"){

                }
                else{
                    Console.WriteLine("I don't understand.. Use (h)elp to find the interaction keywords");
                }
            }
            else{
                Console.WriteLine("I don't understand.. Use (h)elp to find the interaction keywords");
            }
        }

    }
}