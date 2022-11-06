using System;

using Newtonsoft.Json;

namespace text_adventure
{
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