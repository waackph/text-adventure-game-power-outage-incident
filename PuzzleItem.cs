using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace text_adventure
{
    /// <summary>Class <c>PuzzleItem</c> is an Item that can be combined or used with specific items.
    /// </summary>
    public class PuzzleItem : Item
    {
        
        [JsonProperty]
        private Dictionary<string, bool> dependencies;
        [JsonProperty]
        private Dictionary<string, string> combineTexts;
        [JsonProperty]
        private Dictionary<string, string> useTexts;
        [JsonProperty]
        private UseAction action;
        public bool solved = false;
        // interactionName : interactionText
        public PuzzleItem(string Name, string Description, bool isPickupAble, bool isCombineable, bool isUseAble, 
                          string examineText, Dictionary<string, bool> dependencies,
                          Dictionary<string, string> combineTexts, 
                          Dictionary<string, string> useTexts, UseAction action) : base(Name, Description, isPickupAble, examineText)
        {
            this.Name = Name;
            this.Description = Description;

            this.isPickupAble = isPickupAble;
            this.isCombineable = isCombineable;
            this.isUseAble = isUseAble;

            this.dependencies = dependencies;
            this.combineTexts = combineTexts;
            this.useTexts = useTexts;

            this.action = action;
        }

        public bool combineAction(string item){

            if(dependencies.ContainsKey(item)){
                if(dependencies[item] == false){
                    dependencies[item] = true;
                    Console.WriteLine(combineTexts[item]);
                    if(!dependencies.ContainsValue(false)){
                        solved = true;
                    }
                }
                else{
                    Console.WriteLine("Nick already combined that.");
                }
                return true;
            }
            else{
                Console.WriteLine("Nick can't combine that.");
                return false;
            }
        }

        public override void UseAction(){

            if(solved){
                Console.WriteLine(useTexts["solved"]);
                action.DoUseAction();
            }
            else{
                string currDependency = null;
                foreach(KeyValuePair<string, bool> entry in dependencies){
                    if(!entry.Value){
                        currDependency = entry.Key;
                        break;
                    }
                }
                Console.WriteLine(useTexts[currDependency]);
            }
        }
    }
}