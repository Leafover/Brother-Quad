using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset animTarget, animRun, animFire;// nhớ chọn skin với các anim tương ứng với súng. OK chưa // e đang bị mắc chỗ xoay cái tâm đó bằng joystick quay trở lại cái play nhé
    [SpineBone]
    public string strBoneTarget; // tên của cái xương 
    [SpineSkin]
    public string skin;
    private Bone boneTarget;// cái xương target;
    private Spine.Animation[] anims;
    private Skin[] skins;
    // Start is called before the first frame update
    void Start()
    {
        boneTarget = skeletonAnimation.Skeleton.FindBone(strBoneTarget);// lấy được xương rồi thì có thể code để xoay nó lúc mình cần k đẻ ra xương nữa thì sẽ tối ưu hiệu năng hơn, chú có code metal thì mở ra xem mấy con như enemy sniper sẽ thấy phần code của a điều khiển xương target bằng code
        boneTarget.GetWorldPosition(skeletonAnimation.transform);//cái này trả về tọa độ world của cái xương, nếu thằng skeleton mà có localPos=0,0,0 so với gameobjet này thì để luôn là boneTarget.GetWorldPosition(transform)
        skeletonAnimation.AnimationState.ClearTrack(0);// cái này để clear 1 track nhất định;
        skeletonAnimation.AnimationState.ClearTracks(); // clear all track; nhiều lúc cả 2 cái này k có tác dụng thì dùng cái dưới:
        skeletonAnimation.ClearState();// thằng này nó sẽ clear sạch trả về trạng thái như lúc vừa kéo vào scene;
        anims = skeletonAnimation.Skeleton.Data.Animations.Items;//cái này là tất cả anim, có thể dùng nó để chạy anim thay cho việc khai báo từng anim code này phải để cho skeleton nó awake xong mới được gọi
        skeletonAnimation.AnimationState.SetAnimation(0, anims[0], false);//ví dụ cho anims
        skins = skeletonAnimation.Skeleton.Data.Skins.Items;// tat ca skin
        skeletonAnimation.Skeleton.SetSkin(skins[0]);//0-la skin default
        skeletonAnimation.Skeleton.SetSlotsToSetupPose();// goi thang nay de lenh setskin chac chan duoc chay
     // cach khac de set skin;
        skeletonAnimation.Skeleton.SetSkin(skin);

        // khi mà thay đổi skin thì mấy cái anim cũng phải chạy tương ứng à
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skeletonAnimation.AnimationState.SetAnimation(2, animTarget, false);//  thằng điều khiển súng quay theo target này phải để ở track cao hơn track bắn thì nó mới hoạt động
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skeletonAnimation.AnimationState.SetAnimation(1, animFire, false);// bắn
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animRun, true); // các trạng thái di chuyển thường để ở track thấp nhất

        }
    }
}
