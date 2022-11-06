using System;
namespace text_adventure
{
    public abstract class Entity
    {
        public string Name;
        public string Description;

        public Entity(string Name, string Description){
            this.Name = Name;
            this.Description = Description;
        }

        public string GetName(){
            return this.Name;
        }

        public string GetDescription(){
            return this.Description;
        }

    }
}