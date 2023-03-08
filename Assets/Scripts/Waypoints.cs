using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Waypoints : ScriptableObject
{
    [SerializeField] List<Vector3> waypointList = new List<Vector3>();

    public List<Vector3> WaypointList { get => waypointList; set => waypointList = value; }
}
