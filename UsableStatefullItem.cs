using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace text_adventure
{
    /// <summary>Class <c>UsableStatefullItem</c> is an Item that is usable which leads to a specific action.
    /// But the executed action depends on the state the Item is currently in.
    /// </summary>
    public class UsableStatefullItem : UsableItem
    {
        [JsonProperty]
        private UseAction action;
        private UseAction otherAction;
        [JsonProperty]
        private Dictionary<bool, string> useTexts;
        [JsonProperty]
        private List<UsableItem> dependencyList;
        [JsonProperty]
        private bool isWorking;
        //[JsonProperty]
        //public bool isUsed;
        // interactionName : interactionText
        public UsableStatefullItem(string Name, string Description, bool isPickupAble, 
                          string examineText, Dictionary<bool, string> useTexts,
                          UseAction action, UseAction otherAction,
                          List<UsableItem> dependencyList, bool isWorking) : base(Name, Description, isPickupAble, examineText, useTexts, action, otherAction, dependencyList, isWorking)
        {
            this.Name = Name;
            this.Description = Description;

            this.isPickupAble = isPickupAble;
            this.isUseAble = true;

            this.action = action;
            this.otherAction = otherAction;
            
            this.useTexts = useTexts;

            this.dependencyList = dependencyList;
            this.isWorking = isWorking;
            
            this.isUsed = false;
        }

        public override void UseAction(){
            if(!isWorking){
                isWorking = true;
                foreach(UsableItem item in dependencyList){
                    if(!item.isUsed){
                        isWorking = false;
                        break;
                    }
                }
            }            
            if(isWorking){
                if(!isUsed){
                    Console.WriteLine(useTexts[isWorking]);
                    isUsed = otherAction.DoUseAction();
                }
                else{
                    Console.WriteLine("The item has already been used");
                }
            }
            else{
                Console.WriteLine(useTexts[isWorking]);
                action.DoUseAction();
            }
        }
    }
}