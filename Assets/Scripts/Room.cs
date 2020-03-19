using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Messages;

public class Room : MonoBehaviour
{
    public int RoomIndex;
    private bool _isSelected;

    public List<WorkSpot> WorkingSpots = new List<WorkSpot>();

    private void Awake()
    {
        for (int i = 0; i < WorkingSpots.Count; i++)
        {
            WorkingSpots[i].IsFree = true;
            WorkingSpots[i].isLocked = i > 4;
        }

        GameController.MessageBus.Subscribe<RoomUpdatedMessage>(OnRoomUpgraded);
    }
    public WorkSpot GetWorkingSpot()
    {
        WorkSpot[] freeSpots = WorkingSpots.Where(each => each.IsFree && !each.isLocked).ToArray();
        System.Random rand = new System.Random();
        return freeSpots.ElementAt(rand.Next(freeSpots.Length));
    }

    public bool HasFreeSpot()
    {
        return WorkingSpots.Any(each => each.IsFree && !each.isLocked);
    }

    private void OnMouseEnter()
    {
        _isSelected = true;
    }

    private void OnMouseExit()
    {
        _isSelected = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && _isSelected)
        {
            GameController.Instance.DefaultRoomUi.OpenRoom(RoomIndex);
        }
    }

    private void UpdateLockedSpots(bool firstUpgrade)
    {
        WorkSpot[] lockedSpots = WorkingSpots.Where(each => each.isLocked).ToArray();

        for (int i = 0; i < lockedSpots.Length; i++)
        {
            if(firstUpgrade)
            {
                if (i == 0 || i == 1)
                {
                    lockedSpots[i].isLocked = false;
                }
            }
            {
                lockedSpots[i].isLocked = false;
            }
        }
    }

    private void OnRoomUpgraded(RoomUpdatedMessage msg)
    {
        if(msg.roomID == RoomIndex)
        {
            UpdateLockedSpots(msg.firstUpgrade);
        }
    }
}
