using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Room : MonoBehaviour
{
    public int RoomIndex;
    private bool _isSelected;

    public List<WorkSpot> WorkingSpots = new List<WorkSpot>();

    private void Awake()
    {
        foreach (var spot in WorkingSpots)
        {
            spot.IsFree = true;
        }
    }
    public WorkSpot GetWorkingSpot()
    {
        WorkSpot[] freeSpots = WorkingSpots.Where(each => each.IsFree).ToArray();
        System.Random rand = new System.Random();
        return freeSpots.ElementAt(rand.Next(freeSpots.Length));
    }

    public bool HasFreeSpot()
    {
        return WorkingSpots.Any(each => each.IsFree);
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
}
