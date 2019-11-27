using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class MapController : MonoBehaviour
{
    public ProCamera2DTriggerBoundaries[] procam2DTriggerBoudaries;
    private void OnValidate()
    {
        procam2DTriggerBoudaries = GetComponentsInChildren<ProCamera2DTriggerBoundaries>();
    }
}
