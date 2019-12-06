using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
 public void BtnSelectLevel(int i)
    {
        DataParam.indexMap = i;
        Application.LoadLevel(1);
    }
}
