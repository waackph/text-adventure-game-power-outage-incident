namespace text_adventure
{
    /// <summary>Abstract class <c>Entity</c> is the most basic class for a Thing in a Room. 
    /// It is a simple data structure with a name and a description.
    /// </summary>
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