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
        public bool justUpdate;

        public RoomUpdatedMessage(int RoomID, bool isFirstUpgrade, bool justUpdate = false)
        {
            roomID = RoomID;
            firstUpgrade = isFirstUpgrade;
            justUpdate = justUpdate;
            //0=faith
            //1=watchscore
            //2= money
            //3=worker
        }
    }
}
