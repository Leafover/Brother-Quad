using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using PathCreation;
public class MapController : MonoBehaviour
{
    public ProCamera2DTriggerBoundaries[] procam2DTriggerBoudaries;
    public PathCreator[] pathCreator;
    public GameObject pointBeginPlayer;
    private void OnValidate()
    {
        procam2DTriggerBoudaries = GetComponentsInChildren<ProCamera2DTriggerBoundaries>();
        pathCreator = GetComponentsInChildren<PathCreator>();
    }
}
