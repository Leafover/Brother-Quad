using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int index, levelBase = 1;

    public bool activeFar;
    [HideInInspector]
    public bool jumpOut = false;
    public Transform leftFace, rightFace;
    //  [HideInInspector]
    public List<BulletEnemy> listMyBullet;
    public LineBlood lineBlood;
    public bool isBoss, isMiniBoss;
    public System.Action<float> acOnUpdate;
    public bool canoutcam, incam, isMachine = false;
    public enum EnemyState
    {
        idle,
        run,
        attack,
        die
    }
    public EnemyState enemyState = EnemyState.idle;
    public bool canmove;

    public float radius;
    public Collider2D boxAttack1, boxAttack2;
    [HideInInspector]
    public Collider2D takeDamageBox;
    [HideInInspector]
    public float damage1, damage2, damage3, health, bulletspeed1, bulletspeed2, bulletspeed3, attackrank, bulletimeexist, exp, speed, maxtimeDelayAttack1, maxtimeDelayAttack2, maxtimeDelayAttack3;

    public float maxtimedelayChangePos = 4;
    [HideInInspector]
    public AnimationReferenceAsset currentAnim;
    public AssetSpineEnemyController aec;
    [HideInInspector]
    public float timePreviousAttack;

    public LayerMask lm = 13;
    [HideInInspector]
    public Rigidbody2D rid;

    public float currentHealth;
    public float distanceActive = 6;

    [HideInInspector]
    public bool isActive;
    int dir;
    [HideInInspector]
    public int combo, randomCombo;
    public SkeletonAnimation skeletonAnimation;

    Vector2 originPos;

    public Bone boneBarrelGun, boneBarrelGun1, boneBarrelGun2;
    [SpineBone]
    public string strboneBarrelGun, strboneBarrelGun1, strboneBarrelGun2;
    public Transform targetPos;
    [HideInInspector]
    public float tempXBegin;

    public Vector2 OriginPos
    {
        get { return originPos; }
        set { originPos = value; }
    }

    public virtual void PlayAnim(int indexTrack, AnimationReferenceAsset anim, bool loop)
    {
        if (enemyState == EnemyState.die)
            return;
        if (currentAnim != anim)
        {
            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            currentAnim = anim;
        }
    }

    private void OnValidate()
    {
        if (rid == null)
        {
            rid = GetComponent<Rigidbody2D>();
        }
        if (skeletonAnimation == null)
        {
            skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        }
        if (takeDamageBox == null)
            takeDamageBox = GetComponent<Collider2D>();



    }
    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
        return vt2;
    }
    public Vector2 GetTarget(bool run)
    {
        if (!run)
        {
            return PlayerController.instance.transform.position;
        }
        else
        {
            // Debug.LogError(":" + FlipX);
            //if (FlipX)
            //{
            //    Debug.Log("zo day 1" + ":" + rightFace.transform.position + ":" + targetPos.transform.position);
            //    return rightFace.position;

            //}
            //else
            //{
            //    Debug.Log("zo day 2" + ":" + leftFace.transform.position + ":" + targetPos.transform.position);
            //    return leftFace.position;
            //}

            return /*boneBarrelGun.GetWorldPosition(skeletonAnimation.transform)*/FlipX ? rightFace.position : leftFace.position;
        }
    }
    public virtual void Attack(int indexTrack, AnimationReferenceAsset anim, bool loop, float _maxTimedelayAttack)
    {
        if (enemyState == EnemyState.die)
            return;
        if (Time.time - timePreviousAttack >= _maxTimedelayAttack)
        {
            timePreviousAttack = Time.time;
            skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
            if (currentAnim != anim)
                currentAnim = anim;

        }
    }
    //public virtual void Shoot(int indexTrack, AnimationReferenceAsset anim, bool loop, float timeDelayAttack,float _timePreviousAttack)
    //{
    //    if (enemyState == EnemyState.die)
    //        return;
    //    //   Debug.LogError(timePreviousAttack + ":" + timeDelayAttack);
    //    if (Time.time - _timePreviousAttack >= timeDelayAttack)
    //    {
    //        _timePreviousAttack = Time.time;

    //        skeletonAnimation.AnimationState.SetAnimation(indexTrack, anim, loop);
    //        if (currentAnim != anim)
    //            currentAnim = anim;
    //        //  Debug.Log("nem luu dan:" + timeDelayAttack);
    //    }
    //}
    public virtual void OnDisable()
    {
        if (skeletonAnimation != null/* && skeletonAnimation.AnimationState != null*/)
        {
            skeletonAnimation.AnimationState.Event -= Event;
            skeletonAnimation.AnimationState.Complete -= Complete;
        }
        acOnUpdate -= OnUpdate;
        if (GameController.instance != null)
        {
            GameController.instance.RemoveTarget(this);


        }
        if (takeDamageBox != null)
            takeDamageBox.enabled = false;


    }
    public virtual void Start()
    {
        skeletonAnimation.Initialize(true);
        //  Debug.Log("init =====");
    }
    public virtual void Init()
    {
        tempXBegin = transform.position.x;
        if (lineBlood != null)
        {
            lineBlood.Reset();
        }
        //  _timePreviousAttack = maxtimeDelayAttack / 2;

        isActive = false;
        if (boxAttack1 != null)
            boxAttack1.gameObject.SetActive(false);
        if (boxAttack2 != null)
            boxAttack2.gameObject.SetActive(false);

        takeDamageBox.enabled = true;
        enemyState = EnemyState.idle;


        acOnUpdate += OnUpdate;
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.Event += Event;
            skeletonAnimation.AnimationState.Complete += Complete;
        }

        // currentAnim = aec.idle;
        if (skeletonAnimation != null)
        {
            if (boneBarrelGun == null)
            {
                boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
                boneBarrelGun1 = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun1);
                boneBarrelGun2 = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun2);
            }
        }
        //if (aec.aimTargetAnim != null)
        //{
        //    skeletonAnimation.AnimationState.SetAnimation(2, aec.aimTargetAnim, false);
        //    PlayAnim(0, aec.idle, true);
        //}

        if (!isBoss)
        {

            if (!activeFar)
                distanceActive = Camera.main.orthographicSize * 2f;
            else
                distanceActive = Camera.main.orthographicSize * 2f + 2f;
        }
        else
            distanceActive = Camera.main.orthographicSize * 2 + 3;

        AddProperties();

        currentHealth = health;

        skeletonAnimation.gameObject.SetActive(false);
    }

    void AddProperties()
    {

        //  Debug.Log(DataController.instance.allDataEnemy[index].enemyData.Count);

        damage1 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].dmg1;
        damage2 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].dmg2;
        damage3 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].dmg3;

        bulletspeed1 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].bulletspeed1;
        bulletspeed2 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].bulletspeed2pixels;
        bulletspeed3 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].bulletspeed3;

        maxtimeDelayAttack1 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].atksecond1;
        maxtimeDelayAttack2 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].atksecond2;
        maxtimeDelayAttack3 = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].atksecond3;

        attackrank = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].atkrange;

        bulletimeexist = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].bulletexisttime;

        health = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].hp;
        speed = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].movespeed;

        exp = (float)DataController.instance.allDataEnemy[index].enemyData[levelBase - 1].exp;
    }

    public int CheckDirFollowPlayer(float posX)
    {

        if (transform.position.x < posX)
        {
            FlipX = true;
            dir = (int)speed;
        }
        else
        {
            FlipX = false;
            dir = -(int)speed;
        }

        return dir;
    }

    private void Event(TrackEntry trackEntry, Spine.Event e)
    {
        OnEvent(trackEntry, e);
    }

    private void Complete(TrackEntry trackEntry)
    {
        OnComplete(trackEntry);
    }

    protected virtual void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {

    }
    GameObject exploDie;
    protected virtual void OnComplete(TrackEntry trackEntry)
    {
        if (aec.die == null || trackEntry == null)
            return;
        if (trackEntry.Animation.Name.Equals(aec.die.name))
        {
            gameObject.SetActive(false);

            if (!isBoss)
            {
                if (isMachine)
                {
                    SoundController.instance.PlaySound(soundGame.soundexploenemy);
                    exploDie = ObjectPoolerManager.Instance.enemyExploPooler.GetPooledObject();
                    exploDie.transform.position = gameObject.transform.position;
                    exploDie.SetActive(true);
                }
            }
            else
            {
                // SoundController.instance.PlaySound(soundGame.exploGrenade);
                SoundController.instance.PlaySound(soundGame.soundexploenemy);
                exploDie = ObjectPoolerManager.Instance.boss1ExploPooler.GetPooledObject();
                exploDie.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1);
                exploDie.SetActive(true);
                CameraController.instance.Shake(CameraController.ShakeType.ExplosionBossShake);
            }
        }

    }



    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    public virtual void Active()
    {
        isActive = true;
        skeletonAnimation.gameObject.SetActive(true);
        PlayAnim(0, aec.idle, true);

        if (isBoss || isMiniBoss)
        {

            GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currentHealth, health);
            if (isMiniBoss)
                GameController.instance.uiPanel.healthBarBoss.DisplayBegin("Mini Boss " + "Mission " + (DataParam.indexMap + 1));
            if (isBoss)
                GameController.instance.uiPanel.healthBarBoss.DisplayBegin("Boss " + "Mission " + (DataParam.indexMap + 1));

            if (isBoss)
                GameController.instance.isDestroyBoss = true;
        }


        if (aec.aimTargetAnim == null)
            return;
        skeletonAnimation.AnimationState.SetAnimation(2, aec.aimTargetAnim, false);

    }

    public virtual void OnUpdate(float deltaTime)
    {
        if (!isActive)
        {
            var tempCamX = Camera.main.transform.position.x;
            if (tempXBegin - tempCamX <= distanceActive)
            {
                Active();
            }
        }
        else
        {
            if (isBoss || isMiniBoss)
                return;
            if (!incam)
            {
                if (transform.position.x < CameraController.instance.NumericBoundaries.LeftBoundary)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    public void UpdateActionForEnemyManager(float deltaTime)
    {
        if (acOnUpdate == null)
            return;
        acOnUpdate(deltaTime);
    }
    public Vector2 Origin()
    {
        return transform.position;
    }
    public virtual void Dead()
    {
        if (enemyState == EnemyState.die)
            return;

        // DisableAllBullet();
        if (lineBlood != null)
        {
            lineBlood.Hide();
        }
        rid.velocity = Vector2.zero;
        takeDamageBox.enabled = false;
        GameController.instance.targetDetectSprite.SetActive(false);
        skeletonAnimation.ClearState();
        PlayAnim(0, aec.die, false);
        enemyState = EnemyState.die;
        GameController.instance.RemoveTarget(this);
        PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
        DisableAllBullet();

        if(isBoss || isMiniBoss)
        GameController.instance.uiPanel.healthBarBoss.DisableHealthBar();

    }
    void DisableAllBullet()
    {
        //  Debug.LogError(":::::::" + listMyBullet.Count);
        if (listMyBullet.Count == 0)
            return;
        for (int i = 0; i < listMyBullet.Count; i++)
        {
            listMyBullet[i].myEnemy = null;
            listMyBullet[i].gameObject.SetActive(false);
            // Debug.Log("---------disable");
        }
        listMyBullet.Clear();

    }

    Vector2 posHitTemp;
    GameObject hiteffect;
    float hitPosTemp;
    public virtual void TakeDamage(float damage)
    {

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Dead();
            //    Debug.LogError("dead");
        }
        if (lineBlood != null)
        {
            lineBlood.Show(currentHealth, health);
        }
        else
        {
            if (isBoss || isMiniBoss)
            {
                GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currentHealth, health);
            }
        }
        //if(!isMachine)
        //{
        //    StartCoroutine(BeAttackFill());
        //}
        //else
        //{
        //    GameObject hiteffect = ObjectPoolerManager.Instance.hitMachinePooler.GetPooledObject();
        //    hiteffect.transform.position = gameObject.transform.position;
        //    hiteffect.SetActive(true);
        //}
        //if (isBoss || isMiniBoss)
        //    hitPosTemp = 1.5f;
        //else
        if (isBoss || isMiniBoss || isMachine)
        {
            hitPosTemp = 0.2f;
            posHitTemp.x = transform.position.x + Random.Range(-hitPosTemp, hitPosTemp);
            posHitTemp.y = transform.position.y + Random.Range(-hitPosTemp, hitPosTemp);

            hiteffect = ObjectPoolerManager.Instance.hitMachinePooler.GetPooledObject();
            hiteffect.transform.position = posHitTemp;
            hiteffect.SetActive(true);
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (!incam || enemyState == EnemyState.die)
                return;
            TakeDamage(PlayerController.instance.damageBullet);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == 14)
        {
            if (!incam || enemyState == EnemyState.die)
                return;

            TakeDamage(PlayerController.instance.damgeGrenade);
        }
        else if (collision.gameObject.layer == 20)
            gameObject.SetActive(false);

    }
}

