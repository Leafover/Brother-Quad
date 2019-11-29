using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Testfindbone : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset anim;
    private Bone boneTarget;
    [SpineBone]
    public string strBoneTarget;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            skeletonAnimation.AnimationState.SetAnimation(0, anim, false);
    }
    private void OnDisable()
    {
        Debug.LogError("zooooooooo");
    }
}
