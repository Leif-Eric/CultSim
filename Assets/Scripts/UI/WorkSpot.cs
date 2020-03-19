using System.Collections.Generic;
using UnityEngine;

public class WorkSpot : MonoBehaviour
{
    public string AnimationName;
    public Transform SpotPosition;

    public List<Transform> DestinationWay = new List<Transform>();

    private void Awake()
    {
        SpotPosition = this.transform;
    }

    public bool IsFree { get; set; }

    public bool isLocked { get; set; }
}