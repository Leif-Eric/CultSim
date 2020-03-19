using GameEventBus.Events;

public class Messages
{

    public class RessourcesUpdatedMessage : EventBase
    {
        public RessourcesUpdatedMessage()
        {

        }
    }

    public class RoomUpdatedMessage : EventBase
    {
        public int roomID;
        public bool firstUpgrade;

        public RoomUpdatedMessage(int RoomID, bool isFirstUpgrade)
        {
            roomID = RoomID;
            firstUpgrade = isFirstUpgrade;
            //0=faith
            //1=watchscore
            //2= money
            //3=worker
        }
    }
}
