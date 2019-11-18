using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathCreatorController : MonoBehaviour
{
    public static PathCreatorController instance;
    public List<PathCreator> pathCreator;
    private void Start()
    {
        instance = this;
    }
}
