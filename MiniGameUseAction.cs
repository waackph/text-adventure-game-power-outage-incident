using System;

namespace text_adventure
{
    /// <summary>Class <c>MiniGameUseAction</c> implements an action that starts a mini game
    /// and handles its outcome.
    /// </summary>
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