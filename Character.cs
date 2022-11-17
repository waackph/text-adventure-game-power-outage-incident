using System;
using System.Collections.Generic;
namespace text_adventure
{
    /// <summary>Class <c>Character</c> holds data for a Character in a Room.
    /// A Character has also the logic to traverse a dialog tree.
    /// </summary>
    public class Character : Entity
    {
        private DialogTreeNode root;
        private Item dependency;
        private string giveText;
        private UseAction action;

        public Character(string Name, string Description, DialogTreeNode tree, Item dependency, string giveText, UseAction action) : base(Name, Description){
            this.Name = Name;
            this.Description = Description;
            this.root = tree;
            this.dependency = dependency;
            this.giveText = giveText;
            this.action = action;
        }

        public void giveAction(Item item){
            if(item == null || item == dependency){
                Console.WriteLine(giveText);
                action.DoUseAction();
            }
            else{
                Console.WriteLine("I have no use for that!");
            }
        }
        
        // Dialog Tree traversing
        public void ListOptions(DialogTreeNode node){
            List<string> childrenText = node.GetChildrenData();
            foreach(string text in childrenText){
                Console.WriteLine(text);
            }
        }

        public void traverseDialog(DialogTreeNode node){
            Console.WriteLine(node.GetResponse());
            if(node.GetAmountChildren() != 0){
                Console.WriteLine("Choose an answer:");
                ListOptions(node);
                string nextOption = Console.ReadLine();
                DialogTreeNode next = node.GetChild(nextOption);
                if(next != null){
                    traverseDialog(next);
                }
                else{
                    Console.WriteLine("Invalid answer. Please choose one of the given answers");
                    traverseDialog(node);
                }
            }
        }

        public DialogTreeNode GetDialogTreeRoot(){
            return root;
        }
    }
}