using System;
using System.Collections.Generic;

namespace text_adventure
{
    public class DialogTreeNode
    {
        private string option;
        private string text;
        private string response;
        private List<DialogTreeNode> children;

        public DialogTreeNode(string option, string text, string response){
            this.option = option;
            this.text = text;
            this.response = response;
        }

        public string GetText(){
            return text;
        }

        public string GetOption(){
            return option;
        }

        public string GetResponse(){
            return response;
        }

        public void AddChild(DialogTreeNode child){
            children.Add(child);
        }

        public List<string> GetChildrenData(){
            List<string> childData = new List<string>();
            foreach(DialogTreeNode child in children){
                childData.Add(child.GetOption() + ": " + child.GetText());
            }
            return childData;
        }

        public DialogTreeNode GetChild(string option){
            foreach(DialogTreeNode child in children){
                if(child.GetOption() == option){
                    return child;
                }
            }
            return null;
        }

        public int GetAmountChildren(){
            return children.Count;
        }
    }
}