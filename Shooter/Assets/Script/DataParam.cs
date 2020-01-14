using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataParam : MonoBehaviour
{
    public static int indexMap, nextSceneAfterLoad, indexStage, indexMode, levelBase;
    public static float totalCoin;

    public static void AddCoin(float _coin)
    {
        totalCoin += _coin;
    }
}
