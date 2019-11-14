using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Testfindbone : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    private Bone boneTarget;
    [SpineBone]
    public string strBoneTarget;
    // Start is called before the first frame update
    void Start()
    {

        Debug.LogError("wtfffffffffffffffffffff");
        boneTarget = skeletonAnimation.Skeleton.FindBone(strBoneTarget);
        if (boneTarget == null)
            Debug.Log("null");
        else
            Debug.Log("not null");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
