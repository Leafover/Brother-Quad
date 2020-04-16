using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public UAVController uav;
    public GameObject reloadObj;
    public Image reloadImg;
    bool isregen;
    public int countKillByGrenade;
    public GameObject shield;
    private Skin[] skins;
    public AudioSource au, auW7;
    public Animator animArrow;
    public int level = 1;
    //public bool isGrenade;
    public float damageBullet = 1, damgeGrenade = 3, critRate, critDamage, bulletSpeed, attackRange, slowRate, missRate, healthRateBonus, healthRegenRate;
    [HideInInspector]
    public bool reload, stun;
    public Collider2D meleeAtackBox;
    public GameObject dustdown, dustrun, effecthealth, stunEffect;
    [HideInInspector]
    public Collider2D colliderStand;
    [HideInInspector]
    public GameObject currentStand;
    public LineBlood lineBlood;
    public AnimationReferenceAsset currentAnim;
    public AssetSpinePlayerController apc;
    public ParticleSystem /*effectG4, effectG345,*/ effecthutcoin, effectsmokeendshot/*, effectG2*/;
    public GameObject /*parentFXG345, parentFXG2,*/ effectbeginW7, effectendW7;
    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun, strboneHandGrenade;

    float timePreviousAttack, timePreviousGrenade, timePreviousRocket, timePreviousMeleeAttack, timestun, timePreviousSkill;
    public float timedelayAttackGun, timedelayMeleeAttack, timedelayGrenade, timedelayRocket, maxtimestun, timedelaySkill;

    public int numberBullet, maxNumberBullet;
    public float health, maxHealth = 100;

    bool isKnife;

    public Rigidbody2D rid;
    public BoxCollider2D box;
    public FootPlayer foot;
    public LayerMask lm, lmMeleeAtack;

    [SerializeField]
    Vector2 offsetBox, sizeBox;
    [SerializeField]
    Vector2 offsetBoxSit, sizeBoxSit;
    public Transform targetPos;
    public static PlayerController instance;
    public bool isSlow, isLowJump;

    public enum PlayerState
    {
        Idle, Run, Sit, Jump, WaitStand, Die, Win
    }
    public PlayerState playerState = PlayerState.Idle;

    public SkeletonAnimation skeletonAnimation, du;

    public float speedMoveMax = 3;
    [HideInInspector]
    public bool dirMove;
    [HideInInspector]
    public float speedmove;
    [HideInInspector]
    public bool isBouderJoystickShoot, isBouderJoystickMove, isWaitStand, isGround, isfalldow, candoublejump;
    public bool isMeleeAttack;

    //public IEnumerator Move()
    //{

    //    yield return new WaitForEndOfFrame();
    //    StartCoroutine(Move());
    //}

    public float forceJump;
    public float timeJump;
    private float force;
    public void ShowLineBlood()
    {
        lineBlood.Show(health, maxHealth);
    }
    public void Stun()
    {
        if (playerState == PlayerState.Jump || playerState == PlayerState.Die)
            return;
        stun = true;
        rid.velocity = Vector2.zero;
        speedmove = 0;
        //   skeletonAnimation.ClearState();

        if (playerState != PlayerState.Sit)
        {
            playerState = PlayerState.Idle;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, true);
        }
        stunEffect.SetActive(true);
    }
    int randomMiss;
    public void TakeDamage(float damage, bool isNotDestroyAll = true)
    {
        if (playerState == PlayerState.Die || GameController.instance.gameState == GameController.GameState.gameover)
            return;
        if (isReviving && isNotDestroyAll || GameController.instance.win)
            return;
        randomMiss = UnityEngine.Random.Range(0, 100);
        if (randomMiss < missRate)
            return;

        health -= damage;


        if (health <= maxHealth / 100 * 10)
        {
            if (!GameController.instance.uiPanel.lowHealth.activeSelf)
            {
                if (healthRegenRate > 0)
                    isregen = true;
                if (!au.isPlaying)
                    au.Play();
                GameController.instance.uiPanel.takeDamgePanel.SetActive(false);
                GameController.instance.uiPanel.lowHealth.SetActive(true);
            }
        }
        else
        {
            if (!GameController.instance.uiPanel.takeDamgePanel.activeSelf)
            {
                GameController.instance.uiPanel.takeDamgePanel.SetActive(true);
                Debug.LogError("take damage");
            }
        }


        if (DataUtils.playerInfo.healthPack > 0)
        {
            if (GameController.instance.usingHealthPack == 0)
            {
                GameController.instance.uiPanel.DisplayBtnHealth(false, DataUtils.playerInfo.healthPack);
                GameController.instance.usingHealthPack = 1;
            }
        }
        ShowLineBlood();
        // StartCoroutine(BeAttackFill());
        switch(GameController.instance.currentChar)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundplayerhit);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.soundhitnv2);
                break;
        }

        if (health <= 0)
        {
            DEAD();
        }
    }
    public void DEAD()
    {
        GameController.instance.DIE();
        AnimDie();
        playerState = PlayerState.Die;
        switch(GameController.instance.currentChar)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.playerDie);
                break;
            case 1:
                SoundController.instance.PlaySound(soundGame.sounddienv2);
                break;
        }

        au.Stop();
        rid.velocity = Vector2.zero;
        speedmove = 0;
        stunEffect.SetActive(false);
        meleeAtackBox.gameObject.SetActive(false);
        isWaitStand = false;
        isfalldow = false;
        isSlow = false;
        isLowJump = false;
        isMeleeAttack = false;
        //isGrenade = false;
        isGround = false;
        health = 0;
        isregen = false;
        timeRegen = 0;
        reloadObj.SetActive(false);
        //effectG4.Stop();
        //effectG345.Stop();
        //effectG2.Stop();
        DisableLaser();

        if (GameController.instance.currentMap.isVIPProtect)
        {
            GameController.instance.npcController.speedmove = 0;
            GameController.instance.npcController.rid.velocity = Vector2.zero;
        }

    }
    public void CalculateTimeStun(float deltaTime)
    {
        timestun -= deltaTime;
        if (timestun <= 0)
        {
            timestun = maxtimestun;
            stun = false;
            stunEffect.SetActive(false);
            meleeAtackBox.gameObject.SetActive(false);
        }
    }
    public LayerMask lmgroundcandown;
    public void CheckColliderStand(Collider2D _collider)
    {
        if (_collider == null)
        {
            if (colliderStand != null)
                Physics2D.IgnoreCollision(foot.collider, colliderStand, false);
        }
        else
        {
            if (_collider != colliderStand)
            {
                if (colliderStand != null)
                    Physics2D.IgnoreCollision(foot.collider, colliderStand, false);
            }
        }
        colliderStand = _collider;
    }
    public void TryJump()
    {
        if (playerState == PlayerState.Sit)
        {
            if (colliderStand != null)
            {
                Physics2D.IgnoreCollision(foot.collider, colliderStand, true);
                //   foot.myPhysic.friction = 0;
            }

            return;
        }

        if ((playerState != PlayerState.Jump))
        {
            StartCoroutine(Jump());
        }
        else
        {
            if (!candoublejump || isLowJump)
                return;
            candoublejump = false;
            StartCoroutine(DoubleJump());
            //force = rid.velocity.y + 6f;
            //rid.velocity = new Vector2(rid.velocity.x, force);
        }

    }
    public void USESKILL()
    {
        if (timePreviousSkill > 0)
            return;

        timePreviousSkill = timedelaySkill;
        GameController.instance.uiPanel.effectBtnSkill.SetActive(false);
        switch (GameController.instance.currentChar)
        {
            case 0:
                uav.gameObject.SetActive(true);
                Debug.LogError("use skill");
                break;
            case 1:
                GameController.instance.maybay.isAttack = true;
                GameController.instance.maybay.Begin(CameraController.instance.bouders[3].transform.position, false, 10);
                break;
        }
    }
    public void TryGrenade()
    {
        if (timePreviousGrenade > 0)
            return;
        timePreviousGrenade = timedelayGrenade;
        countKillByGrenade = 0;
        //    Debug.Log(timePreviousGrenade);
        if (playerState != PlayerState.Jump)
        {
            skeletonAnimation.AnimationState.SetAnimation(3, apc.grenadeAnim, false);
            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);
            // isGrenade = true;
        }
        else
        {
            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
            grenade.SetActive(true);

        }
        MissionController.Instance.DoMission(4, 1);
        SoundController.instance.PlaySound(soundGame.throwGrenade);
    }
    public void AddProperties()
    {

        Debug.LogError("Level:" + DataUtils.heroInfo.level);

        //damgeGrenade = (float)DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].DmgGrenade;
        //speedMoveMax = (float)DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed + ((float)DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed * DataUtils.itemShoes.moveSpeedIncrease / 100) - ((float)DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed * DataUtils.itemArmor.speedReduce / 100);
        //maxHealth = (float)DataController.instance.playerData[DataUtils.HeroIndex()].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level  : DataUtils.MAX_LEVEL_HERO - 1].hp;

        damgeGrenade = (float)DataController.instance.playerData[GameController.instance.currentChar].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].DmgGrenade;
        speedMoveMax = (float)DataController.instance.playerData[GameController.instance.currentChar].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed + ((float)DataController.instance.playerData[GameController.instance.currentChar].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed * DataUtils.itemShoes.moveSpeedIncrease / 100) - ((float)DataController.instance.playerData[GameController.instance.currentChar].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].MoveSpeed * DataUtils.itemArmor.speedReduce / 100);
        maxHealth = (float)DataController.instance.playerData[GameController.instance.currentChar].playerData[DataUtils.heroInfo.level < DataUtils.MAX_LEVEL_HERO ? DataUtils.heroInfo.level : DataUtils.MAX_LEVEL_HERO - 1].hp;
        Debug.LogError("maxHealth:" + maxHealth);
        Debug.LogError("speedMoveMax:" + speedMoveMax);
        Debug.LogError("damgeGrenade:" + damgeGrenade);



        missRate = DataUtils.itemArmor.defIncrease + DataUtils.itemHelmet.defIncrease;
        healthRateBonus = DataUtils.itemBag.totalAidDrop;
        healthRegenRate = DataUtils.itemBag.HealthRegeneration;
        forceJump += (forceJump * DataUtils.itemShoes.jumpHeight / 100);
        health = maxHealth;
        speedmove = 0;
    }
    public void SetGun(int index)
    {
        currentGun = index;

        CalculateForGun();
        if (currentGun != 6)
        {
            DisableLaser();
        }

        skeletonAnimation.Skeleton.SetSkin(skins[index + 2]);
        //else
        //{
        //    skeletonAnimation.Skeleton.SetSkin(skins[5 + 2]);
        //}
        //  Debug.LogError(currentGun);
    }
    public void CalculateForGun()
    {
        if (currentGun != DataUtils.itemWeapon.weponIndex)
        {
            damageBullet = (float)DataController.instance.allWeapon[currentGun].weaponList[0].DmgValue[0];
            maxTimeReload = (float)DataController.instance.allWeapon[currentGun].weaponList[0].ReloadSpeedValue[0] - ((float)DataController.instance.allWeapon[currentGun].weaponList[0].ReloadSpeedValue[0] * DataUtils.itemGloves.reloadTimeReduce / 100);
            maxNumberBullet = (int)DataController.instance.allWeapon[currentGun].weaponList[0].MagazineValue[0];
            critRate = (float)DataController.instance.allWeapon[currentGun].weaponList[0].CritRateValue[0] + ((float)DataController.instance.allWeapon[currentGun].weaponList[0].CritRateValue[0] * DataUtils.itemGloves.critRateIncrease / 100);
            critDamage = (float)DataController.instance.allWeapon[currentGun].weaponList[0].CritDmgValue[0] + ((float)DataController.instance.allWeapon[currentGun].weaponList[0].CritDmgValue[0] * DataUtils.itemGloves.critDamageIncrease / 100);
            bulletSpeed = (float)DataController.instance.allWeapon[currentGun].weaponList[0].BulletSpeedValue[0];
            attackRange = (float)DataController.instance.allWeapon[currentGun].weaponList[0].AtkRangeValue[0];
            timedelayAttackGun = (float)DataController.instance.allWeapon[currentGun].weaponList[0].AtksecValue[0];
            if (currentGun == 4)
            {
                numberLine = 3;
            }
        }
        else
        {
            if (currentGun != 6)
            {
                damageBullet = DataUtils.itemWeapon.DmgValue;
                maxTimeReload = DataUtils.itemWeapon.ReloadSpeedValue - (DataUtils.itemWeapon.ReloadSpeedValue * DataUtils.itemGloves.reloadTimeReduce / 100);
                maxNumberBullet = (int)DataUtils.itemWeapon.MagazineValue;
                critRate = DataUtils.itemWeapon.CritRateValue + (DataUtils.itemWeapon.CritRateValue * DataUtils.itemGloves.critRateIncrease / 100);
                critDamage = DataUtils.itemWeapon.CritDmgValue + (DataUtils.itemWeapon.CritDmgValue * DataUtils.itemGloves.critDamageIncrease / 100);
                bulletSpeed = DataUtils.itemWeapon.BulletSpeedValue;
                attackRange = DataUtils.itemWeapon.AtkRangeValue;
                timedelayAttackGun = DataUtils.itemWeapon.AtksecValue;
                if (currentGun == 4)
                {
                    switch (DataUtils.eWeaponLevel)
                    {
                        case "Normal":
                            numberLine = 3;
                            break;
                        case "Uncommon":
                            numberLine = 3;
                            break;
                        case "Rare":
                            numberLine = 5;
                            break;
                        case "Epic":
                            numberLine = 5;
                            break;
                        case "Legendary":
                            numberLine = 5;
                            break;
                    }
                }
            }
            else
            {
                damageBullet = (float)DataController.instance.allWeapon[currentGun].weaponList[0].DmgValue[0];
                maxTimeReload = (float)DataController.instance.allWeapon[currentGun].weaponList[0].ReloadSpeedValue[0] - ((float)DataController.instance.allWeapon[currentGun].weaponList[0].ReloadSpeedValue[0] * DataUtils.itemGloves.reloadTimeReduce / 100);
                maxNumberBullet = (int)DataController.instance.allWeapon[currentGun].weaponList[0].MagazineValue[0];
                critRate = (float)DataController.instance.allWeapon[currentGun].weaponList[0].CritRateValue[0] + ((float)DataController.instance.allWeapon[currentGun].weaponList[0].CritRateValue[0] * DataUtils.itemGloves.critRateIncrease / 100);
                critDamage = (float)DataController.instance.allWeapon[currentGun].weaponList[0].CritDmgValue[0] + ((float)DataController.instance.allWeapon[currentGun].weaponList[0].CritDmgValue[0] * DataUtils.itemGloves.critDamageIncrease / 100);
                bulletSpeed = (float)DataController.instance.allWeapon[currentGun].weaponList[0].BulletSpeedValue[0];
                attackRange = (float)DataController.instance.allWeapon[currentGun].weaponList[0].AtkRangeValue[0];
                timedelayAttackGun = (float)DataController.instance.allWeapon[currentGun].weaponList[0].AtksecValue[0];
            }

        }

        numberBullet = maxNumberBullet;
        timeReload = 0;
        isShoot = false;
        timePreviousAttack = 0;
        countbullet = 0;
        reload = false;
        reloadObj.SetActive(false);
        GameController.instance.uiPanel.bulletText.text = "" + numberBullet;

        //Debug.LogError(currentGun + ":"+numberBullet);
    }
    //public void TryRocket()
    //{
    //    if (Time.time - timePreviousRocket > timedelayRocket)
    //    {
    //        timePreviousRocket = Time.time;
    //        if (isGround)
    //        {
    //            skeletonAnimation.AnimationState.SetAnimation(1, apc.grenadeAnim, false);
    //        }
    //        else
    //        {
    //            GameObject grenade = ObjectPoolerManager.Instance.grenadePooler.GetPooledObject();
    //            grenade.transform.position = boneHandGrenade.GetWorldPosition(skeletonAnimation.transform);
    //            grenade.SetActive(true);
    //        }
    //    }
    //}
    public Transform GetTransformPlayer()
    {
        return transform;
    }
    public float GetTranformXPlayer()
    {
        return transform.position.x;
    }
    private void OnCompleteDu(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals("1"))
        {

        }
        if (trackEntry.Animation.Name.Equals("2"))
        {
            GameController.instance.gameState = GameController.GameState.play;
            GameController.instance.letgo.SetActive(true);
            du.gameObject.SetActive(false);
            skeletonAnimation.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
            GameController.instance.ActiveUI();
            animArrow.gameObject.SetActive(true);
        }
    }
    public void EndEvent()
    {
        du.AnimationState.Complete -= OnCompleteDu;
        skeletonAnimation.AnimationState.Complete -= OnComplete;
    }
    private void Start()
    {
        animArrow.gameObject.SetActive(false);
        lineBlood.Reset();
        skeletonAnimation.Initialize(true);
        boneBarrelGun = skeletonAnimation.Skeleton.FindBone(strboneBarrelGun);
        boneHandGrenade = skeletonAnimation.Skeleton.FindBone(strboneHandGrenade);
        //     skeletonAnimation.AnimationState.Event += HandleEvent;
        skeletonAnimation.AnimationState.Complete += OnComplete;
        du.AnimationState.Complete += OnCompleteDu;

        // skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
        AddNumberBullet(-maxNumberBullet);

        timePreviousGrenade = 0;
        timePreviousAttack = 0;
        timePreviousMeleeAttack = 0;
        timePreviousSkill = 0;

        // waitBeAttack = new WaitForSeconds(0.075f);
        // currentGun = 0;
        skins = skeletonAnimation.Skeleton.Data.Skins.Items;
        // skeletonAnimation.Skeleton.SetSkin(skins[currentGun + 1]);
        AddProperties();

        SetGun(DataUtils.itemWeapon.weponIndex);
        //   Debug.Log(skins.Length);

        au.mute = !DataUtils.IsSoundOn();

        skeletonAnimation.gameObject.SetActive(false);
        DisableLaser();

    }

    public int currentGun;

    public void DetectGround()
    {
        if (isfalldow)
            speedmove = 0;
        isfalldow = false;
        candoublejump = false;
        DeTectPosRevive();
        if (rid.gravityScale == .2f)
            rid.gravityScale = 1;
    }
    Vector2 jumpVelo;
    private IEnumerator DoubleJump()
    {
        if (GameController.instance.gameState != GameController.GameState.gameover)
        {
            float timeUp = timeJump * 0.5f;
            playerState = PlayerState.Jump;
            //  SoundController.instance.PlaySound(soundGame.soundbtnclick);
            SoundController.instance.PlaySound(soundGame.sounddoublejump);
            AnimJump();
            for (float t = 0; t <= timeUp; t += Time.deltaTime)
            {
                if (playerState == PlayerState.Jump)
                {
                    force = forceJump * (timeUp - t);
                    jumpVelo = rid.velocity;
                    jumpVelo.x = rid.velocity.x;
                    jumpVelo.y = force / 2;
                    rid.velocity = /*new Vector2(rid.velocity.x, force / 2)*/jumpVelo;
                    yield return null;
                }
            }
        }
    }
    private IEnumerator Jump()
    {
        if (GameController.instance.gameState != GameController.GameState.gameover)
        {
            //   SoundController.instance.PlaySound(soundGame.soundbtnclick);
            SoundController.instance.PlaySound(soundGame.soundjump);
            isMeleeAttack = false;
            meleeAtackBox.gameObject.SetActive(false);
            if (rid.gravityScale == .2f)
                rid.gravityScale = 1;
            if (colliderStand != null)
                Physics2D.IgnoreCollision(foot.collider, colliderStand, false);
            float timeUp = timeJump * 0.5f;
            playerState = PlayerState.Jump;
            AnimJump();
            //    candoublejump = true;

            for (float t = 0; t <= timeUp; t += Time.deltaTime)
            {
                if (playerState == PlayerState.Jump)
                {
                    force = forceJump * (timeUp - t);
                    jumpVelo = rid.velocity;
                    jumpVelo.x = rid.velocity.x;
                    if (!isLowJump)
                        jumpVelo.y = force;
                    else
                        jumpVelo.y = force / 2;
                    rid.velocity = /*new Vector2(rid.velocity.x, force)*/jumpVelo;
                    yield return null;
                }
            }
            if (!isLowJump)
                candoublejump = true;
        }
    }

    public void SetAnim()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                AnimIdle();
                break;
            case PlayerState.Sit:
                AnimSit();
                break;
            case PlayerState.Jump:
                AnimJump();
                if (isGround && rid.velocity.y <= 0)
                {
                    if (!GameController.instance.joystickMove.GetJoystickState())
                    {
                        isWaitStand = true;
                        playerState = PlayerState.Idle;
                        speedmove = 0;
                        //  skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
                    }
                    else
                    {
                        isWaitStand = false;
                        GameController.instance.CheckAfterJump(GameController.instance.joystickMove);
                        //  skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
                    }
                }
                break;
            case PlayerState.Run:
                AnimRun();
                break;

        }
    }
    public Vector2 target;
    Vector2 movePos;
    public float radius;
    float timeStand = 6;
    public GameObject poitRayGround;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(poitRayGround.transform.position, radius);
    //}
    float timereviving = 2;

    public void BeginPlayer()
    {
        isGround = Physics2D.OverlapCircle(poitRayGround.transform.position, radius, lm);
        if (isGround)
        {
            if (du.AnimationName == "1")
            {
                du.AnimationState.SetAnimation(0, "2", false);
                rid.gravityScale = 1;
            }
        }
    }
    float timeRegen;
    public void OnUpdate(float deltaTime)
    {

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetGun(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGun(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGun(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGun(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetGun(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetGun(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetGun(6);
        }

        isGround = Physics2D.OverlapCircle(poitRayGround.transform.position, radius, lm);
        movePos = rid.velocity;
        movePos.x = speedmove;
        movePos.y = rid.velocity.y;
        rid.velocity = movePos;
        if (!isGround)
        {
            if (playerState == PlayerState.Jump)
            {
                isfalldow = false;
            }
            else
            {
                if (rid.velocity.y < 0)
                {
                    isfalldow = true;
                }
            }
        }
        else
        {
            DetectGround();
        }
        SetAnim();

        if (isReviving)
        {
            timereviving -= deltaTime;
            if (timereviving <= 0)
            {
                shield.SetActive(false);
                timereviving = 2;
                isReviving = false;
            }
        }

        LockPlayer();
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, deltaTime * 35f);

        if (isShoot)
        {
            timePreviousAttack -= deltaTime;
            if (timePreviousAttack <= 0)
            {
                switch (currentGun)
                {
                    case 1:
                        countbullet--;
                        CreateBullet(false);
                        if (countbullet == 0)
                        {
                            timePreviousAttack = timedelayAttackGun * 2;
                            isShoot = false;
                            AddNumberBullet(1);
                        }
                        else
                        {
                            timePreviousAttack = timedelayAttackGun / 4;
                        }
                        break;
                    default:
                        isShoot = false;
                        break;
                }
            }
        }
        else
        {
            timePreviousAttack -= deltaTime;
        }
        if (timePreviousGrenade > 0)
        {
            timePreviousGrenade -= deltaTime;
        }
        if (timePreviousSkill > 0)
            timePreviousSkill -= deltaTime;
        if (timePreviousMeleeAttack >= 0)
        {
            timePreviousMeleeAttack -= deltaTime;
            if (timePreviousMeleeAttack <= 0)
            {
                meleeAtackBox.gameObject.SetActive(false);
                if (!reload)
                    skeletonAnimation.AnimationState.SetEmptyAnimation(3, 0);
            }
        }
        if (playerState == PlayerState.Idle)
        {
            if (timeStand > 0)
            {
                timeStand -= deltaTime;
                if (timeStand <= 0)
                {
                    if (!animArrow.GetBool("animarrow"))
                    {
                        animArrow.SetBool("animarrow", true);
                    }
                    if (apc.chopmatAnim != null)
                    {
                        skeletonAnimation.state.SetAnimation(5, apc.chopmatAnim, false);
                        timeStand = 1f;
                    }

                    //Debug.Log("----chay anim arrow--");
                }
            }
        }
        else
        {
            if (timeStand != 2 && animArrow.GetBool("animarrow"))
            {
                timeStand = 2;
                animArrow.SetBool("animarrow", false);
                //  Debug.Log("------stop anim arrow----");
            }
        }
        GameController.instance.uiPanel.FillGrenade(timePreviousGrenade, timedelayGrenade);
        GameController.instance.uiPanel.FillSkill(timePreviousSkill, timedelaySkill);
        CaculatePosAndRotation();


        if (playerState != PlayerState.Jump)
        {
            isMeleeAttack = Physics2D.Linecast(foot.transform.position, posGun(), lmMeleeAtack); /*Physics2D.OverlapCircle(boneBarrelGun.GetWorldPosition(skeletonAnimation.transform), radius, lmMeleeAtack)*/ /*FlipX ? Physics2D.Linecast(transform.position, rightface.position, lmMeleeAtack) : Physics2D.Linecast(transform.position, leftface.position, lmMeleeAtack)*/;
            // Debug.DrawLine(transform.position, rightface.position);

        }
        else
            isMeleeAttack = false;

        if (isregen)
        {
            timeRegen -= deltaTime;
            if (timeRegen <= 0)
            {
                AddHealth(maxHealth * healthRegenRate / 100);
                timeRegen = 3;
            }
        }

        if (!reload)
        {
            if (numberBullet == 0)
            {
                if (currentGun != DataUtils.itemWeapon.weponIndex)
                {
                    SetGun(DataUtils.itemWeapon.weponIndex);
                    //  Debug.LogError("reset gun");
                    return;
                }

                // Debug.LogError("reload active");
                SoundController.instance.PlaySound(soundGame.soundbulletdrop);
                skeletonAnimation.AnimationState.SetAnimation(3, apc.reloadAnim, true);
                reload = true;
                switch(GameController.instance.currentChar)
                {
                    case 0:
                        SoundController.instance.PlaySound(soundGame.soundreload);
                        break;
                    case 1:
                        SoundController.instance.PlaySound(soundGame.soundreloadnv2);
                        break;
                }

                timeReload = maxTimeReload;
                reloadObj.SetActive(true);
            }
            return;
        }
        timeReload -= deltaTime;
        reloadImg.fillAmount = (maxTimeReload - timeReload) / maxTimeReload;
        DisableLaser();
        if (timeReload <= 0)
        {
            skeletonAnimation.AnimationState.SetEmptyAnimation(3, 0);
            AddNumberBullet(-maxNumberBullet);
            reload = false;
            reloadObj.SetActive(false);
        }

    }
    public Transform leftface, rightface;
    void AddNumberBullet(int _sub)
    {
        numberBullet -= _sub;
        GameController.instance.uiPanel.bulletText.text = "" + numberBullet;

        if (numberBullet <= 5)
        {
            GameController.instance.uiPanel.bulletText.color = Color.red;
        }
        else
        {
            GameController.instance.uiPanel.bulletText.color = Color.white;
        }
    }
    Vector2 posTemp;
    void LockPlayer()
    {
        if (transform.position.x <= CameraController.instance.bouders[3].transform.position.x + 1.5f)
        {
            posTemp = transform.position;
            posTemp.x = CameraController.instance.bouders[3].transform.position.x + 1.5f;
            transform.position = posTemp;
        }
        if (GameController.instance.isDestroyBoss && DataParam.indexStage == 1 && DataParam.indexMap == 7)
        {
            if (transform.position.x >= GameController.instance.currentMap.autoSpawnEnemys[GameController.instance.currentMap.autoSpawnEnemys.Length - 1].transform.position.x + 2)
            {
                posTemp = transform.position;
                posTemp.x = GameController.instance.currentMap.autoSpawnEnemys[GameController.instance.currentMap.autoSpawnEnemys.Length - 1].transform.position.x + 2;
                transform.position = posTemp;
            }
            return;
        }
        if (transform.position.x >= CameraController.instance.bouders[2].transform.position.x - 1.5f)
        {
            posTemp = transform.position;
            posTemp.x = CameraController.instance.bouders[2].transform.position.x - 1.5f;
            transform.position = posTemp;
        }
        //if (transform.position.y >= CameraController.instance.bouders[0].transform.position.y - 1)
        //{
        //    posTemp = transform.position;
        //    posTemp.y = CameraController.instance.bouders[0].transform.position.y - 1;
        //    transform.position = posTemp;
        //}
        //if (transform.position.y <= CameraController.instance.bouders[1].transform.position.y + 1)
        //{
        //    posTemp = transform.position;
        //    posTemp.y = CameraController.instance.bouders[1].transform.position.y + 1;
        //    transform.position = posTemp;
        //}
    }

    public void Init()
    {
        instance = this;
        gameObject.SetActive(true);
    }

    void SetBox(Vector2 size, Vector2 offset)
    {
        if (box.offset != offset)
            box.offset = offset;
        if (box.size != size)
            box.size = size;
    }

    public Vector2 GetTargetTranform()
    {
        return targetPos.transform.position;
    }
    GameObject bullet, grenade;
    Vector2 dirBullet;
    float angle, angle2, angle3, angle4, angle5;
    Quaternion rotation, rotation2, rotation3, rotation4, rotation5;
    // Vector2 posEffect;
    Vector2 posGun()
    {
        return boneBarrelGun.GetWorldPosition(skeletonAnimation.transform);
    }
    void CaculatePosAndRotation()
    {
        if (currentGun == 6)
            return;

        effectsmokeendshot.transform.position = posGun();
        dirBullet = GetTargetTranform() - posGun();
        angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
        angle2 = angle + 10;
        angle3 = angle - 10;
        angle4 = angle + 20;
        angle5 = angle - 20;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation2 = Quaternion.AngleAxis(angle2, Vector3.forward);
        rotation3 = Quaternion.AngleAxis(angle3, Vector3.forward);
        rotation4 = Quaternion.AngleAxis(angle4, Vector3.forward);
        rotation5 = Quaternion.AngleAxis(angle5, Vector3.forward);

        //parentFXG2.transform.position = effectG4.transform.position = parentFXG345.transform.position = /*(Vector2)boneBarrelGun.GetWorldPosition(skeletonAnimation.transform)*/posGun();
        //parentFXG2.transform.rotation = parentFXG345.transform.rotation = effectG4.transform.rotation = rotation;
    }

    public void CreateBulletShotGun()
    {
        SoundController.instance.PlaySound(soundGame.soundshootW3);
        bullet = ObjectPoolerManager.Instance.bulletW3Pooler.GetPooledObject();
        bullet.transform.rotation = rotation;
        bullet.transform.position = posGun();
        // effectG345.Play();
        bullet.SetActive(true);
    }
    int numberLine = 5;
    public void CreateBulletToa()
    {
        SoundController.instance.PlaySound(soundGame.soundshootW5);
        for (int i = 0; i < numberLine; i++)
        {
            bullet = ObjectPoolerManager.Instance.bulletW4Pooler.GetPooledObject();
            if (i == 1)
            {
                bullet.transform.rotation = rotation2;
            }
            else if (i == 2)
            {
                bullet.transform.rotation = rotation3;
            }
            else if (i == 0)
            {
                bullet.transform.rotation = rotation;
            }
            else if (i == 3)
            {
                bullet.transform.rotation = rotation4;
            }
            else if (i == 4)
            {
                bullet.transform.rotation = rotation5;
            }
            bullet.transform.position = posGun();
            bullet.SetActive(true);
        }
        // effectG345.Play();
    }
    int posIndexBullet;
    public void CreateBullet(bool lech)
    {
        switch (currentGun)
        {
            case 0:
                SoundController.instance.PlaySound(soundGame.soundshootW1);
                bullet = ObjectPoolerManager.Instance.bulletW1Pooler.GetPooledObject();
                break;
            case 1:
                bullet = ObjectPoolerManager.Instance.bulletW2Pooler.GetPooledObject();
                break;
            case 5:
                // effectG345.Play();
                SoundController.instance.PlaySound(soundGame.soundshootW6);
                bullet = ObjectPoolerManager.Instance.bulletW5Pooler.GetPooledObject();
                bullet.GetComponent<Bullet>().dir = GetTargetTranform();
                break;
            case 3:
                // effectG4.Play();
                SoundController.instance.PlaySound(soundGame.soundshootW4);
                bullet = ObjectPoolerManager.Instance.bulletW6Pooler.GetPooledObject();
                break;
        }

        bullet.transform.rotation = rotation;

        if (lech)
        {
            posBulletTemp.x = posGun().x;
            if (posIndexBullet == 0)
            {
                posBulletTemp.y = posGun().y + 0.07f;
                posIndexBullet = 1;
            }
            else
            {
                posBulletTemp.y = posGun().y - 0.07f;
                posIndexBullet = 0;
            }
            bullet.transform.position = posBulletTemp;
        }
        else
        {
            bullet.transform.position = posGun();
        }
        bullet.SetActive(true);

    }
    Vector2 posBulletTemp;
    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(apc.waitstandAnim.name))
        {
            isWaitStand = false;
        }
        else if (trackEntry.Animation.Name.Equals(apc.grenadeAnim.name))
        {
            if (!reload)
                skeletonAnimation.AnimationState.SetEmptyAnimation(3, 0);
        }
    }
    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = posGun();
        return vt2;
    }

    public LayerMask layerTarget;

    public bool FlipX
    {
        get { return skeletonAnimation.skeleton.FlipX; }
        set { skeletonAnimation.skeleton.FlipX = value; }
    }
    void AnimWaitStand()
    {
        if (currentAnim == apc.waitstandAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.waitstandAnim, false);
        currentAnim = apc.waitstandAnim;
        SetBox(sizeBoxSit, offsetBoxSit);
    }
    void AnimFallDow()
    {
        if (currentAnim == apc.falldownAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.falldownAnim, false);
        currentAnim = apc.falldownAnim;
        SetBox(sizeBox, offsetBox);
    }
    void AnimDie()
    {
        skeletonAnimation.ClearState();
        if (currentAnim == apc.dieAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.dieAnim, false);
        currentAnim = apc.dieAnim;
    }
    void AnimJump()
    {
        if (isfalldow)
        {
            return;
        }
        else
        {
            skeletonAnimation.state.GetCurrent(0).TimeScale = 1.3f;
            if (currentAnim == apc.jumpAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.jumpAnim, true);
            currentAnim = apc.jumpAnim;
            SetBox(sizeBox, offsetBox);
        }

    }
    void AnimSit()
    {
        if (isfalldow && rid.velocity.y != 0)
        {
            AnimFallDow();
            return;
        }
        else
        {
            if (currentAnim == apc.sitAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.sitAnim, true);
            currentAnim = apc.sitAnim;
            speedmove = 0;
            SetBox(sizeBoxSit, offsetBoxSit);

        }
    }
    float timeScaleRun;
    void AnimRun()
    {
        if (isfalldow && rid.velocity.y < 0)
        {
            AnimFallDow();
            return;
        }
        timeScaleRun = isSlow ? 0.5f : 1.3f;
        skeletonAnimation.state.GetCurrent(0).TimeScale = timeScaleRun;
        if (dirMove == FlipX)
        {
            if (currentAnim == apc.runForwardAnim)
                return;

            skeletonAnimation.AnimationState.SetAnimation(0, apc.runForwardAnim, true);
            currentAnim = apc.runForwardAnim;
            SetBox(sizeBox, offsetBox);

        }
        else
        {
            if (currentAnim == apc.runBackAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.runBackAnim, true);
            currentAnim = apc.runBackAnim;
            SetBox(sizeBox, offsetBox);

        }

    }

    public void AnimWin()
    {
        skeletonAnimation.ClearState();
        if (currentAnim == apc.winAnim)
            return;
        skeletonAnimation.AnimationState.SetAnimation(0, apc.winAnim, true);
        currentAnim = apc.winAnim;
        DisableLaser();

        if (!GameController.instance.currentMap.isVIPProtect)
            return;

        GameController.instance.npcController.AnimWin();

    }
    public void AnimIdle()
    {
        if (isfalldow && rid.velocity.y != 0)
        {
            AnimFallDow();
            return;
        }
        if (isWaitStand)
        {
            AnimWaitStand();
        }
        else
        {
            if (currentAnim == apc.idleAnim)
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, apc.idleAnim, true);
            currentAnim = apc.idleAnim;
            speedmove = 0;
            SetBox(sizeBox, offsetBox);



        }
    }
    public void MeleeAttack()
    {
        if (timePreviousMeleeAttack > 0)
        {
            return;
        }
        DisableLaser();
        timePreviousMeleeAttack = timedelayMeleeAttack;
        skeletonAnimation.AnimationState.SetAnimation(3, apc.meleeAttackAnim, false);
        meleeAtackBox.transform.position = posGun();
        meleeAtackBox.gameObject.SetActive(true);
    }
    bool isShoot;
    public LineRenderer laserRender;
    public BoxCollider2D boxLaser;


    void PlaySoundW7(bool play)
    {
        if (play)
        {
            if (!auW7.isPlaying)
                auW7.Play();
        }
        else
        {
            if (auW7.isPlaying)
                auW7.Stop();
        }
        auW7.mute = !DataUtils.IsSoundOn();
    }

    public void ActiveLaser()
    {
        addColliderToLine();
    }
    Vector2 startPos, endPos, midPoint;
    float lineLength;

    private void addColliderToLine()
    {
        startPos = posGun();
        endPos = GetTargetTranform();
        lineLength = Vector3.Distance(startPos, endPos);
        boxLaser.size = new Vector2(lineLength, 0.1f);
        midPoint = (startPos + endPos) / 2;
        boxLaser.transform.position = midPoint;
        angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        boxLaser.transform.rotation = rotation;

        effectbeginW7.transform.position = startPos;
        laserRender.SetPosition(0, startPos);
        laserRender.SetPosition(1, endPos);
        effectendW7.transform.position = endPos;
        effectendW7.transform.rotation = rotation;

        effectbeginW7.gameObject.SetActive(true);
        effectendW7.gameObject.SetActive(true);

        laserRender.enabled = true;
        PlaySoundW7(true);
    }


    public void DisableLaser()
    {
        if (!laserRender.enabled)
            return;
        laserRender.enabled = false;
        boxLaser.gameObject.SetActive(false);
        effectbeginW7.gameObject.SetActive(false);
        effectendW7.gameObject.SetActive(false);

        PlaySoundW7(false);
    }
    public void ShootDown()
    {
        if (reload || meleeAtackBox.gameObject.activeSelf)
        {
            return;
        }
        if (currentGun == 6)
        {
            ActiveLaser();
        }
        if (isShoot || timePreviousAttack > 0)
            return;
        skeletonAnimation.AnimationState.SetAnimation(1, apc.fireAnim, false);
        isShoot = true;
        switch (currentGun)
        {
            case 1:
                countbullet = 3;
                timePreviousAttack = 0;
                SoundController.instance.PlaySound(soundGame.soundshootW2);
                //effectG2.Play();
                break;
            case 2:
                CreateBulletShotGun();
                timePreviousAttack = timedelayAttackGun;
                AddNumberBullet(1);
                break;
            case 4:
                CreateBulletToa();
                timePreviousAttack = timedelayAttackGun;
                AddNumberBullet(1);
                break;
            case 6:
                if (boxLaser.gameObject.activeSelf)
                    boxLaser.gameObject.SetActive(false);
                boxLaser.gameObject.SetActive(true);
                timePreviousAttack = timedelayAttackGun;
                AddNumberBullet(1);
                break;
            default:
                CreateBullet(true);
                timePreviousAttack = timedelayAttackGun;
                AddNumberBullet(1);
                break;
        }


    }

    int countbullet;
    float timeReload, maxTimeReload;

    public void ChangeKnife()
    {
        isKnife = !isKnife;
    }

    Vector2 targetTemp;
    public Vector2 GetTargetFromDirection(Vector2 direction)
    {
        var origin = GetOriginGun();
        direction.Normalize();
        var hit = Physics2D.Raycast(origin, direction, 1000, layerTarget);
        if (hit.collider != null)
        {
            targetTemp = hit.point;
        }
        return targetTemp;
    }
    public bool haveTarget;
    Vector2 GetTarget()
    {
        targetTemp = new Vector2(float.MaxValue, float.MaxValue);
        var dMin = float.MaxValue;
        for (int i = 0; i < GameController.instance.autoTarget.Count; i++)
        {

            var enemy = GameController.instance.autoTarget[i];

            if (!enemy.incam || enemy.currentHealth <= 0 || !enemy.gameObject.activeSelf)
            {
                haveTarget = false;
                targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                GameController.instance.targetDetectSprite.SetActive(false);
                continue;
            }


            var from = (Vector2)transform.position;
            var to = enemy.Origin();
            var d = Vector2.Distance(from, to);

            if (d < dMin)
            {
                dMin = d;

                if (d >= 0.5f)
                {
                    targetTemp = enemy.transform.position;
                    GameController.instance.targetDetectSprite.transform.position = enemy.transform.position;
                    GameController.instance.targetDetectSprite.SetActive(true);
                    haveTarget = true;
                }
                else
                {
                    targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                    GameController.instance.targetDetectSprite.SetActive(false);
                    haveTarget = false;
                }
            }
        }

        return targetTemp;
    }
    public void SelectNonTarget(Vector2 pos)
    {
        GameController.instance.targetDetectSprite.SetActive(false);
        target = GetTargetFromDirection(pos);
        haveTarget = false;
    }

    public void SelectTarget()
    {
        //if (!reload)
        //{
        target = GetTarget();
        FlipX = GetTarget().x < transform.position.x;
        //}
        //else
        //{
        //    SelectNonTarget(!FlipX ? Vector2.right : Vector2.left);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 26:
                TakeDamage(damgeGrenade / 3);
                break;
        }
    }
    public void AddHealth(float _health)
    {
        if (playerState == PlayerState.Die)
            return;

        health += _health;
        if (!effecthealth.activeSelf)
            effecthealth.SetActive(true);
        if (health > maxHealth / 100 * 10)
        {
            au.Stop();
            GameController.instance.uiPanel.lowHealth.gameObject.SetActive(false);
        }
        if (isregen)
        {
            if (health > maxHealth / 100 * 20)
            {
                isregen = false;
            }
        }
        ShowLineBlood();
        if (health >= maxHealth)
            health = maxHealth;
    }
    public void EndShot()
    {
        if (isMeleeAttack)
            return;
        if (currentGun == 2 || currentGun == 4 || currentGun == 5)
        {
            if (effectsmokeendshot.isStopped)
            {
                effectsmokeendshot.Play();
            }
        }
    }

    public void DeTectPosRevive()
    {
        posPlayerRevive.x = GetTranformXPlayer();
        posPlayerRevive.y = GetTransformPlayer().position.y + 1;
    }
    Vector3 posCamRevive;
    Vector3 posPlayerRevive;
    bool isReviving;
    public void Revive(int healthBonus)
    {
        if (GameController.instance.reviveCount == 0)
        {
            ResetPosRevive(true, healthBonus);
        }
    }
    public void ResetPosRevive(bool afterdie, int healthBonus)
    {
        var checkplatform = Physics2D.Raycast(foot.transform.position, -transform.up, 100, lm);

        if (checkplatform.collider != null)
        {

        }
        else
        {
            if (currentStand.transform.position.x < transform.position.x)
            {
                posPlayerRevive.x -= 1f;
            }
            else if (currentStand.transform.position.x > transform.position.x)
            {
                posPlayerRevive.x += 1f;
            }
        }

        rid.velocity = Vector2.zero;
        rid.gravityScale = 1;
        playerState = PlayerState.Idle;
        AnimIdle();

        isReviving = true;

        meleeAtackBox.gameObject.SetActive(false);
        isWaitStand = false;
        isfalldow = false;
        isSlow = false;
        isLowJump = false;
        isMeleeAttack = false;
        isGround = false;

        timereviving = 2;
        transform.position = posPlayerRevive;
        shield.SetActive(true);

        if (!afterdie)
            return;
        posCamRevive.y = Camera.main.transform.position.y;
        posCamRevive.x = GetTranformXPlayer() + 3;
        posCamRevive.z = Camera.main.transform.position.z;
        Camera.main.transform.position = posCamRevive;
        skeletonAnimation.AnimationState.SetAnimation(2, apc.aimTargetAnim, false);
        GameController.instance.reviveCount = 1;
        AddHealth(maxHealth / 100 * healthBonus);

        CameraController.instance.procam.enabled = true;
    }

}
