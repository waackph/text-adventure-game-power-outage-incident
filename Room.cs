using System.Collections.Generic;

namespace text_adventure
{
    public class Room : Entity
    {
        public List<Item> Items;
        public List<Character> Characters;
        public string lookAroundText;

        public Room(string Name, string Description, string lookAroundText, List<Item> Items) : base(Name, Description){
            this.Name = Name;
            this.Description = Description;
            this.lookAroundText = lookAroundText;
            this.Items = Items;
        }

        public Item GetRoomItem(string itemName){
            foreach(Item item in Items){
                if(item.Name == itemName){
                    return item;
                }
            }
            return null;
        }

        public Character GetRoomCharacter(string characterName){
            foreach(Character character in Characters){
                if(character.Name == characterName){
                    return character;
                }
            }
            return null;
        }
    }
}
