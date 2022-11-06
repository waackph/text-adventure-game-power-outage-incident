using System;
using Newtonsoft.Json;

namespace text_adventure
{
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