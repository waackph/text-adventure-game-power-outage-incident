namespace text_adventure
{
    public class ChangeRoomUseAction : UseAction
    {
        public string requiredRoom;
        public string newRoom;

        public ChangeRoomUseAction(string requiredRoom, string newRoom){
            this.requiredRoom = requiredRoom;
            this.newRoom = newRoom;
        }

        public override bool DoUseAction(){
            if(InteractionManager.currentRoom.GetName() == requiredRoom){
                InteractionManager.currentRoom = InteractionManager.getRoomByName(newRoom);
                InteractionManager.EnterRoom();
                return true;
            }
            else{
                return false;
            }
        }
    }
}