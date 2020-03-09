using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class testboss2 : MonoBehaviour
{
    public SkeletonAnimation sa;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            sa.AnimationState.SetAnimation(0, "Enemy Nem bom", false);
            sa.AnimationState.SetAnimation(1, "Die Nong 1", false);
            sa.AnimationState.SetAnimation(2, "Die Nong 2", false);
            sa.AnimationState.SetAnimation(3, "Die Nong 3", false);
            sa.AnimationState.SetAnimation(4, "Die Nong 4", false);

        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            sa.AnimationState.SetAnimation(0, "Enemy Die", false);
        }
    }
}
