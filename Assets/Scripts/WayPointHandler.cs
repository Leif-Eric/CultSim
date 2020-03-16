using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WayPointHandler : MonoBehaviour
{
    public List<Way> Ways = new List<Way>();
   
    public List<Transform> GetWayPoints(int start, int target)
    {
        return Ways.FirstOrDefault(each => each.Start == start && each.Target == target).WayPoints;
    }
}

[Serializable]
public class Way
{
    public int Start;
    public int Target;

    public List<Transform> WayPoints = new List<Transform>();
}
