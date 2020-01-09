using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : AutoTarget
{

    public GameObject foot;
    //  [HideInInspector]
    public bool isGround = true;
    public AudioSource au;
    public List<float> healthFill;

    [HideInInspector]
    public BulletEnemy bulletEnemy;
    public bool haveHealhItem;
    public float percentHealthForPlayer = 10;
    bool haveCoin;
    [HideInInspector]
    public bool enemyAutoSpawn;
    public int levelBase = 1;

    public bool activeFar;
    [HideInInspector]
    public bool jumpOut = false, frameOn;
    public Transform leftFace, rightFace;
    //  [HideInInspector]
    public List<BulletEnemy> listMyBullet;
    public LineBlood lineBlood;
    public bool isBoss, isMiniBoss, moveFollowPath;
    public System.Action<float> acOnUpdate;
    public bool canoutcam, isMachine = false;
    public enum EnemyState
    {
        idle,
        run,
        attack,
        falldown,
        die
    }
    public EnemyState enemyState = EnemyState.idle;
    public bool canmove;

    public float radius;
    public Collider2D boxAttack1, boxAttack2, boxAttack3;
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

    public LayerMask lm = 13, lmground;
    [HideInInspector]
    public Rigidbody2D rid;

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

    public Vector2 PosBegin
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
            //if(anim == aec.falldown && index == 4)
            //Debug.Log("zopoooooooooooo");
        }
    }

    public virtual void OnValidate()
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
    public void CheckFallDown()
    {
        isGround = Physics2D.OverlapCircle(foot.transform.position, 0.115f, lmground);
        if (!isGround && enemyState != EnemyState.falldown)
        {
            enemyState = EnemyState.falldown;
            PlayAnim(0, aec.falldown, false);
            //if (currentAnim == aec.idle)
            //    PlayAnim(0, aec.falldown, false);
            //else if (currentAnim == aec.run)
            //{
            //    if (aec.jump != null)
            //        PlayAnim(0, aec.jump, false);
            //    else
            //        PlayAnim(0, aec.falldown, false);
            //}
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
            //if (currentAnim != anim)
            //    currentAnim = anim;

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
        if (skeletonAnimation != null && skeletonAnimation.AnimationState != null)
        {
            skeletonAnimation.AnimationState.Event -= Event;
            skeletonAnimation.AnimationState.Complete -= Complete;
        }
        acOnUpdate -= OnUpdate;
        if (GameController.instance != null)
        {
            GameController.instance.RemoveTarget(this);

            if (GameController.instance.enemyLockCam.Contains(this))
                GameController.instance.enemyLockCam.Remove(this);
        }
        if (takeDamageBox != null)
            takeDamageBox.enabled = false;

        enemyAutoSpawn = false;
        incam = false;
    }
    float healthTotalTempBoss;
    public virtual void Start()
    {

        skeletonAnimation.Initialize(true);

        //  Debug.Log("init =====");
    }
    public virtual void Init()
    {
        isGround = true;
        isShield = false;
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
        if (takeDamageBox != null)
            takeDamageBox.enabled = true;
        enemyState = EnemyState.idle;


        acOnUpdate += OnUpdate;
        if (skeletonAnimation != null && skeletonAnimation.AnimationState != null)
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

            randomHaveCoin = Random.Range(0, 2);
            haveCoin = randomHaveCoin == 1 ? true : false;
        }
        else
        {
            distanceActive = Camera.main.orthographicSize * 2 + 3;
        }

        AddProperties();

        currentHealth = health;

        skeletonAnimation.gameObject.SetActive(false);
        frameOn = false;
    }
    int randomHaveCoin;
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

        if (isBoss || isMiniBoss)
            CalculateBenginHealthBoss();

    }
    [HideInInspector]
    public int numberCountLayerHelthBarBoss = 0;
    public void CalculateBenginHealthBoss()
    {

        healthTotalTempBoss = health;

        if (isBoss)
        {
            numberCountLayerHelthBarBoss = 10;
        }
        else if (isMiniBoss)
        {
            numberCountLayerHelthBarBoss = 5;
        }
        float addhealth = healthTotalTempBoss / numberCountLayerHelthBarBoss;
        for (int i = 0; i < numberCountLayerHelthBarBoss; i++)
        {
            if (i < numberCountLayerHelthBarBoss - 1)
            {
                healthFill.Add(addhealth);
                healthTotalTempBoss -= addhealth;
            }
            else
            {
                healthFill.Add(healthTotalTempBoss);

            }

        }
        indexHealthFill = 0;
        currenthealthfill = healthFill[indexHealthFill];
    }

    public int CheckDirFollowPlayer(float posX)
    {

        if (transform.position.x < posX)
        {
            FlipX = true;
            dir = /*!isSlow ? */(int)speed /*: (int)(speed / 2)*/;
        }
        else
        {
            FlipX = false;
            dir = /*!isSlow ?*/ -(int)speed /*: -(int)(speed / 2)*/;
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
    [HideInInspector]
    public GameObject exploDie, healthItem;
    int randomCoin;
    public void AfterDead()
    {
        if (haveHealhItem)
        {
            healthItem = ObjectPoolerManager.Instance.healthItemPooler.GetPooledObject();
            healthItem.transform.position = gameObject.transform.position;
            healthItem.GetComponent<ItemBase>().AddNumberTemp(percentHealthForPlayer);
            healthItem.SetActive(true);
        }
        if (!isBoss && !isMiniBoss)
        {
            if (isMachine)
            {
                SoundController.instance.PlaySound(soundGame.soundexploenemy);

                exploDie = ObjectPoolerManager.Instance.enemyMachineExploPooler.GetPooledObject();
                exploDie.transform.position = gameObject.transform.position;
                exploDie.SetActive(true);
            }
            else
            {
                SoundController.instance.PlaySound(soundGame.soundexploenemy);

                exploDie = ObjectPoolerManager.Instance.enemyExploPooler.GetPooledObject();
                exploDie.transform.position = gameObject.transform.position;
                exploDie.SetActive(true);
            }

            if (!haveHealhItem)
            {
                if (haveCoin && GameController.instance.totalDropCoin > 0)
                {
                    randomCoin = Random.Range(3, 5);
                    if (GameController.instance.totalDropCoin - randomCoin < 0)
                    {
                        randomCoin = GameController.instance.totalDropCoin;
                    }
                    GameController.instance.totalDropCoin -= randomCoin;
                    GameController.instance.SpawnCoin(randomCoin, transform.position);
                }
            }
        }
        else if (isMiniBoss)
        {
            SoundController.instance.PlaySound(soundGame.soundexploenemy);
            exploDie = ObjectPoolerManager.Instance.exploMiniBoss1Pooler.GetPooledObject();
            posExplo.x = gameObject.transform.position.x;
            posExplo.y = gameObject.transform.position.y;
            exploDie.transform.position = posExplo;
            exploDie.SetActive(true);
            CameraController.instance.Shake(CameraController.ShakeType.ExplosionBossShake);
            GameController.instance.SpawnCoin(8, transform.position);
        }
    }
    protected virtual void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry == null)
            return;
        if (aec.die != null)
        {
            if (trackEntry.Animation.Name.Equals(aec.die.name))
            {
                if (!isBoss)
                    gameObject.SetActive(false);

                AfterDead();
            }
        }


    }
    [HideInInspector]
    public Vector2 posExplo;


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
      //  Debug.LogError("zooooo day");
        if (isBoss || isMiniBoss)
        {
            if (takeDamageBox != null)
                takeDamageBox.enabled = false;
            GameController.instance.uiPanel.warning.SetActive(true);
            GameController.instance.uiPanel.healthBarBoss.healthbossText.text = "X" + numberCountLayerHelthBarBoss;
            //      GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currenthealthfill, healthFill[indexHealthFill], indexHealthFill);
            for (int i = 0; i < numberCountLayerHelthBarBoss; i++)
            {
                GameController.instance.uiPanel.healthBarBoss.healthFill[i].fillAmount = 1;
            }
            if (isMiniBoss)
            {
                switch(DataParam.indexStage)
                {
                    case 0:
                        GameController.instance.uiPanel.healthBarBoss.DisplayBegin("ASSAULT COPTER");
                        break;
                    case 1:
                        GameController.instance.uiPanel.healthBarBoss.DisplayBegin("EYE-BIO HAZARD");
                        break;
                }

            }
            if (isBoss)
            {
                switch (DataParam.indexStage)
                {
                    case 0:
                        GameController.instance.uiPanel.healthBarBoss.DisplayBegin("MEGATRON");
                        break;
                    case 1:
                        GameController.instance.uiPanel.healthBarBoss.DisplayBegin("ALIEN BASE");
                        break;
                }           
            }
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
            if (tempXBegin - tempCamX <= distanceActive || enemyAutoSpawn)
            {
                Active();
            }
        }
        else
        {
            if (isBoss || isMiniBoss || moveFollowPath)
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

    public virtual void Dead()
    {
        if (enemyState == EnemyState.die)
            return;
      //  isSlow = false;
        // DisableAllBullet();
        if (lineBlood != null)
        {
            lineBlood.Hide();
        }
        if (rid != null)
            rid.velocity = Vector2.zero;

        if (takeDamageBox != null)
            takeDamageBox.enabled = false;
        GameController.instance.targetDetectSprite.SetActive(false);
        skeletonAnimation.ClearState();
        if (!isBoss)
        {
            if (!frameOn)
                if (aec.die != null)
                    PlayAnim(0, aec.die, false);
                else
                {
                    AfterDead();
                    gameObject.SetActive(false);
                }
            else
            {
                PlayAnim(0, aec.run2, true);
            }
        }
        else
        {
            PlayAnim(0, aec.die, true);
        }

        enemyState = EnemyState.die;
        GameController.instance.RemoveTarget(this);
        PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
        DisableAllBullet();
        GameController.instance.AddCombo();
        if (isBoss || isMiniBoss)
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
    [HideInInspector]
    public bool isShield;
    [HideInInspector]
    public int indexHealthFill;
    [HideInInspector]
    public float currenthealthfill;
    public virtual void TakeDamage(float damage, bool crit = false, bool iamGunboss = false)
    {
        if (iamGunboss)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Dead();
            }
            currenthealthfill -= damage;
            GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currenthealthfill, healthFill[indexHealthFill], indexHealthFill);
            if (currenthealthfill <= 0)
            {
                numberCountLayerHelthBarBoss--;
                GameController.instance.uiPanel.healthBarBoss.healthbossText.text = "X" + numberCountLayerHelthBarBoss;
                if (indexHealthFill < healthFill.Count - 1)
                {
                    indexHealthFill++;
                    currenthealthfill += healthFill[indexHealthFill];
                    GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currenthealthfill, healthFill[indexHealthFill], indexHealthFill);
                }
            }
            return;
        }
        if (isShield)
        {
            hitPosTemp = 0.05f;
            posHitTemp.x = transform.position.x + Random.Range(-hitPosTemp, hitPosTemp);
            posHitTemp.y = transform.position.y + Random.Range(-hitPosTemp, hitPosTemp);

            hiteffect = ObjectPoolerManager.Instance.hitshieldEffectPooler.GetPooledObject();
            hiteffect.transform.position = posHitTemp;
            hiteffect.SetActive(true);
            SpawnNumberMissionText();

            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
        if (lineBlood != null)
        {
            lineBlood.Show(currentHealth, health);
        }
        else
        {
            if (isBoss || isMiniBoss)
            {

                currenthealthfill -= damage;
                GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currenthealthfill, healthFill[indexHealthFill], indexHealthFill);
                if (currenthealthfill <= 0)
                {
                    numberCountLayerHelthBarBoss--;
                    GameController.instance.uiPanel.healthBarBoss.healthbossText.text = "X" + numberCountLayerHelthBarBoss;
                    if (indexHealthFill < healthFill.Count - 1)
                    {
                        indexHealthFill++;
                        currenthealthfill += healthFill[indexHealthFill];
                        GameController.instance.uiPanel.healthBarBoss.DisplayHealthFill(currenthealthfill, healthFill[indexHealthFill], indexHealthFill);
                    }
                }

            }
        }

        if (isBoss || isMiniBoss || isMachine)
        {

            SpawnHitEffect();
        }

        SpawnNumberDamageText((int)damage, crit);


    }
    void SpawnNumberMissionText()
    {
        numberText = ObjectPoolManagerHaveScript.Instance.numberDamgageTextPooler.GetNumberDamageTextPooledObject();
        numberText.transform.position = transform.position;
        numberText.Display("Miss", false);
        numberText.tmp.color = Color.gray;
        numberText.gameObject.SetActive(true);
    }
    void SpawnNumberDamageText(int damage, bool crit)
    {
        numberText = ObjectPoolManagerHaveScript.Instance.numberDamgageTextPooler.GetNumberDamageTextPooledObject();
        numberText.transform.position = transform.position;
        numberText.Display("" + (int)damage * 10, crit);
        numberText.gameObject.SetActive(true);

        // Debug.LogError("zooooo wtf text ");
    }
    void SpawnHitEffect()
    {
        hitPosTemp = 0.2f;
        posHitTemp.x = transform.position.x + Random.Range(-hitPosTemp, hitPosTemp);
        posHitTemp.y = transform.position.y + Random.Range(-hitPosTemp, hitPosTemp);

        hiteffect = ObjectPoolerManager.Instance.hitMachinePooler.GetPooledObject();
        hiteffect.transform.position = posHitTemp;
        hiteffect.SetActive(true);

        //  Debug.LogError("zooooo wtf effect");
    }



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.layer)
        {
            case 11:
                if (!incam || enemyState == EnemyState.die)
                    return;

                takecrithit = Random.Range(0, 100);
                if (takecrithit <= 3)
                {
                    TakeDamage(PlayerController.instance.damageBullet * 2, true);
                    if (!GameController.instance.listcirtwhambang[0].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundCritHit);
                    GameController.instance.listcirtwhambang[0].DisplayMe(transform.position);
                }
                else
                {
                    TakeDamage(PlayerController.instance.damageBullet);
                    //  Debug.Log("Take Dâmgwe");
                }

                collision.gameObject.SetActive(false);
                break;
            case 14:
                if (!incam || enemyState == EnemyState.die)
                    return;

                TakeDamage(PlayerController.instance.damgeGrenade);

                if (currentHealth <= 0)
                {
                    if (!GameController.instance.listcirtwhambang[1].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundGrenadeKill);
                    GameController.instance.listcirtwhambang[1].DisplayMe(transform.position);
                    MissionController.Instance.DoMission(1, 1);
                }
                break;
            case 26:
                if (!incam || enemyState == EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damgeGrenade);
                break;
            case 27:
                if (!incam || enemyState == EnemyState.die)
                    return;
                TakeDamage(PlayerController.instance.damageBullet * 1.5f);
                SoundController.instance.PlaySound(soundGame.sounddapchao);
                if (currentHealth <= 0)
                {
                    if (!GameController.instance.listcirtwhambang[2].gameObject.activeSelf)
                        SoundController.instance.PlaySound(soundGame.soundWham);
                    GameController.instance.listcirtwhambang[2].DisplayMe(transform.position);
                    MissionController.Instance.DoMission(5, 1);
                }
                break;
            case 20:
                gameObject.SetActive(false);
                break;

        }
    }
    //bool isSlow = false; 

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("slowdamage"))
    //        isSlow = true;
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("slowdamage"))
    //        isSlow = false;
    //}

}

