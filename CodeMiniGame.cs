using System;

namespace text_adventure
{
    /// <summary>Class <c>CodeMiniGame</c> implements a mini game where the player must enter the correct code.
    /// </summary>
    public class CodeMiniGame : MiniGame
    {
        public string code;

        public CodeMiniGame(string code){
            this.code = code;
        }        

        public override bool start(){
            Console.WriteLine("Enter the code:");
            string input = Console.ReadLine();

            if(this.code == input){
                return true;
            }
            else{
                return false;
            }
        }
    }
}