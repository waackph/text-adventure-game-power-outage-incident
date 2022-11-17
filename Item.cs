namespace text_adventure
{
    /// <summary>Class <c>Item</c> holds data for an interactable Item.
    /// </summary>
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