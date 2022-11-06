using System;

using Newtonsoft.Json;

namespace text_adventure
{
    public class MiniGameUseAction : UseAction
    {
        //[JsonProperty]
        public string requiredRoom;
        //[JsonProperty]
        public MiniGame miniGame;

        public MiniGameUseAction(string requiredRoom, MiniGame miniGame){
            this.requiredRoom = requiredRoom;
            this.miniGame = miniGame;
        }

        public override bool DoUseAction(){

            bool success = miniGame.start();

            if(InteractionManager.currentRoom.GetName() == requiredRoom & success){
                Console.WriteLine("It worked!");
                return true;
            }
            else{
                Console.WriteLine("That didn't work.");
                return false;
            }
        }
    }
}