using System;
using Newtonsoft.Json;

namespace text_adventure
{
    /// <summary>Class <c>NothingUseAction</c> implements an action that simply prints a text.
    /// </summary>
    public class NothingUseAction : UseAction
    {
        [JsonProperty]
        private string text;

        public NothingUseAction(string text){
            this.text = text;
        }

        public override bool DoUseAction(){
            Console.WriteLine(this.text);
            return true;    
        }
    }
}