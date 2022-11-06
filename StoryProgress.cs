using Newtonsoft.Json;
using System;

namespace text_adventure
{
    public static class StoryProgress
    {
        [JsonProperty]
        private static bool isStart = true;
        public static void progressListener(){
            // Begining
            if(isStart){
                Console.WriteLine("Nick lays on the Sofa and stares at the TV screen. He is 100% concentrated on a game to complete. Suddenly the lights go off. \nAnd even worse: The gaming console and TV also. Complete silence fills the room. It seems to be a power failure. \nNick tries hard to recall the outside of his living room and after some time sitting in the dark \nhe remembers that there should be a cellar somewhere in the house that might has a fuse box in it. \nHe needs to turn on the electricty again. It is of the utmost importance!");
                isStart = false;
            }

            // Ending
            if(InteractionManager.currentRoom.Name == "Living Room Bright"){
                Menu.run = false;
                Console.WriteLine("Yey! The light is on now. Nick starts up his gaming console and sits on the sofa. There he plays games and lives happily ever after.");
                Console.WriteLine("Congratulations!");
                DialogUtility.ContinueText();
            }
        }
    }
}