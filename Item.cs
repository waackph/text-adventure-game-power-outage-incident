using System;

namespace text_adventure
{
    public class Item : Entity
    {
        public bool isPickupAble;
        public bool isUseAble;
        public bool isCombineable;
        public string examineText;
        public Item(string Name, string Description, 
                    bool isPickupAble, string examineText) : base(Name, Description)
        {
            this.Name = Name;
            this.Description = Description;

            this.isPickupAble = isPickupAble;
            this.isUseAble = false;
            this.isCombineable = false;

            this.examineText = examineText;
        }

        public virtual void UseAction(){}
   }
}