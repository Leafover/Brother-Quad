using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : EnemyBase
{
    public List<AnimationReferenceAsset> shotguns, dieguns;
    Bone[] boneGun = new Bone[7];
    [SpineBone]
    public string[] strboneGun;
    public List<GunBoss2> gunList;
    public GunBoss2 enemyGrenade, machineGun, centerEnergy;
    float healthTemp;
    int countindexShot;

    public List<GunBoss2> checkGun = new List<GunBoss2>();
    private void OnValidate()
    {
        if (checkGun.Count == gunList.Count)
            return;
        checkGun.AddRange(gunList);
    }
    public override void Start()
    {
        base.Start();
        Init();
    }
    public void CalculateHealthAllGun()
    {
        healthTemp = currentHealth - centerEnergy.currentHealth - machineGun.currentHealth/* - enemyGrenade.currentHealth*/;

        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].currentHealth = healthTemp / (gunList.Count);
        }
    }
    public void CalculateAgainHealthAllGunWhenDie(int indexGun)
    {
        if (indexGun < 5 && indexGun > 0)
        {
            PlayAnim(indexGun, dieguns[indexGun - 1]);
            if (indexGun == 4)
            {
                GameController.instance.autoTarget.Add(machineGun);
                machineGun.gameObject.SetActive(true);
                if (enemyState == EnemyState.attack)
                {
                    enemyState = EnemyState.idle;
                    combo = 0;
                    randomCombo = Random.Range(3, 5);
                }
            }
            else
            {
                StartCoroutine(delayAddGun());
            }
        }
        else if (indexGun == 5)
        {
            PlayAnim(enemyGrenade.index, aec.standup);
            PlayAnim(indexGun, aec.falldown);

            GameController.instance.autoTarget.Add(centerEnergy);
            centerEnergy.currentHealth = currentHealth;
            centerEnergy.gameObject.SetActive(true);
            enemyState = EnemyState.falldown;

        }
        else if (indexGun == 6)
        {
            Dead();
        }

    }
    public void PlayAnim(int index, AnimationReferenceAsset _anim)
    {
        skeletonAnimation.AnimationState.SetAnimation(index, _anim, false);
    }
    IEnumerator delayAddGun()
    {
        yield return new WaitForSeconds(0.5f);
        if (!gunList[0].gameObject.activeSelf)
        {
            gunList[0].gameObject.SetActive(true);
            GameController.instance.autoTarget.Add(gunList[0]);
        }
    }
    public override void Init()
    {
        base.Init();
        countindexShot = 0;
        randomCombo = Random.Range(3, 6);
        maxCountGrenade = Random.Range(2, 5);
        timeGrenade = maxtimeDelayAttack2 * 3;
        timePreviousAttack = maxtimeDelayAttack1;
        if (!EnemyManager.instance.boss2s.Contains(this))
        {
            EnemyManager.instance.boss2s.Add(this);
        }


        for (int i = 0; i < strboneGun.Length; i++)
        {
            boneGun[i] = skeletonAnimation.Skeleton.FindBone(strboneGun[i]);

            if (i < strboneGun.Length - 3)
            {
                gunList[i].transform.position = boneGun[i].GetWorldPosition(skeletonAnimation.transform);
                gunList[i].index = i + 1;
                gunList[i].incam = true;
            }
        }
        enemyGrenade.index = 0;
        machineGun.index = 5;
        centerEnergy.index = 6;
        enemyGrenade.transform.position = boneGun[4].GetWorldPosition(skeletonAnimation.transform);
        machineGun.transform.position = boneGun[5].GetWorldPosition(skeletonAnimation.transform);
        centerEnergy.transform.position = boneGun[6].GetWorldPosition(skeletonAnimation.transform);
        enemyGrenade.incam = true;
        machineGun.incam = true;
        centerEnergy.incam = true;

        centerEnergy.currentHealth = currentHealth / 100 * 40;
        machineGun.currentHealth = currentHealth / 100 * 20;
        // enemyGrenade.currentHealth = currentHealth / 100 * 20;

        CalculateHealthAllGun();
    }
    public override void Active()
    {
        base.Active();
        //   SoundController.instance.PlaySound(soundGame.soundDisplayMiniBoss2);
        gunList[0].gameObject.SetActive(true);
        //  GameController.instance.autoTarget.Add(gunList[0]);
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (!isActive)
        {
            return;
        }
        if (enemyState == EnemyState.die)
            return;

        if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= Camera.main.orthographicSize + 3)
        {
            if (GameController.instance.uiPanel.CheckWarning())
            {
                GameController.instance.uiPanel.warning.SetActive(false);
                gunList[0].gameObject.SetActive(true);
                GameController.instance.autoTarget.Add(gunList[0]);

            }
        }
        if (GameController.instance.uiPanel.CheckWarning())
            return;

        switch (enemyState)
        {
            case EnemyState.idle: //trạng thái bắn súng máy
                timePreviousAttack -= deltaTime;
                if (timePreviousAttack <= 0)
                {
                    skeletonAnimation.AnimationState.SetAnimation(machineGun.index, aec.attack1, false);
                    timePreviousAttack = maxtimeDelayAttack1;
                    timeAttack = maxtimedelayChangePos;
                    combo++;
                    //  Debug.Log(combo + ":" + randomCombo);
                    if (combo == randomCombo)
                    {
                        if (gunList.Count > 0)
                        {
                            enemyState = EnemyState.attack;
                            // skeletonAnimation.AnimationState.SetAnimation(gunList[0].index, shotguns[gunList[0].index - 1], false);
                            StartCoroutine(delayShot(gunList[0].index, maxtimeDelayAttack2));
                        }
                        else
                        {
                            enemyState = EnemyState.idle;
                        }
                        combo = 0;
                        randomCombo = Random.Range(3, 5);
                    }
                }
                break;
            case EnemyState.attack: // trạng thái bắn rocket
                timeAttack -= deltaTime;
                if (timeAttack <= 0)
                    enemyState = EnemyState.idle;
                break;
            case EnemyState.falldown:// nổ hết sạch súng
                AttackCenterEnergy(deltaTime);
                break;
        }

        GrenadeAttack(deltaTime);
    }
    float timeAttack;
    int isGrenade = 0;
    void AttackCenterEnergy(float deltaTime)
    {
        timeEnergy -= deltaTime;
        if (timeEnergy <= 0)
        {
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletenergyBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2);
            bulletEnemy.dir1 = new Vector2(-bulletspeed2 / 3, bulletspeed2 / 2.5f);
            bulletEnemy.rid.gravityScale = 1;
            bulletEnemy.gameObject.layer = 17;
            bulletEnemy.Init(4);
            bulletEnemy.transform.position = centerEnergy.transform.position;          
            bulletEnemy.gameObject.SetActive(true);
            timeEnergy = maxtimeDelayAttack2;
        }

    }
    void GrenadeAttack(float deltaTime)
    {
        if (enemyState == EnemyState.falldown)
            return;
        if (isGrenade == 0)
        {
            timeGrenade -= deltaTime;
            if (timeGrenade <= 0)
            {
                skeletonAnimation.AnimationState.SetAnimation(enemyGrenade.index, aec.jump, false);
                timeGrenade = maxtimeDelayAttack2 * 2;
                isGrenade = 1;
            }
        }
        else if (isGrenade == 1)
        {
            timeGrenade -= deltaTime;
            if (timeGrenade <= 0)
            {
                skeletonAnimation.AnimationState.SetAnimation(enemyGrenade.index, aec.attack2, false);
                timeGrenade = maxtimeDelayAttack2 * 2;
            }
        }
    }
    float timeGrenade, timeEnergy;
    public void ExploOffBoss()
    {
        SoundController.instance.PlaySound(soundGame.soundexploenemy);
        exploDie = ObjectPoolerManager.Instance.boss1ExploPooler.GetPooledObject();
        exploDie.transform.position = transform.position;
        exploDie.SetActive(true);
        CameraController.instance.Shake(CameraController.ShakeType.ExplosionBossShake);
        GameController.instance.SpawnCoin(15, transform.position);

        gameObject.SetActive(false);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance == null)
            return;
        if (EnemyManager.instance.boss2s.Contains(this))
        {
            EnemyManager.instance.boss2s.Remove(this);
        }
    }

    Vector2 dirBullet;
    Quaternion rotation;
    float angle;
    Vector3 rotationBullet = new Vector3(0,0,111);
    // public GameObject[] gunRotate;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            //bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletMachinegunBoss2Pooler.GetBulletEnemyPooledObject();
            //bulletEnemy.AddProperties(damage1, bulletspeed1);
            //dirBullet = PlayerController.instance.GetTransformPlayer().position - machineGun.transform.position;
            //angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            //rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //bulletEnemy.transform.rotation = rotation;
            //bulletEnemy.transform.position = machineGun.transform.position;
            //bulletEnemy.gameObject.SetActive(true);

            SoundController.instance.PlaySound(soundGame.soundmachinegunBoss2);
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletMachinegunBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1 / 2);
            bulletEnemy.SetTimeExist(bulletimeexist);
            bulletEnemy.BeginDisplay(Vector2.zero, this);
            listMyBullet.Add(bulletEnemy);
            bulletEnemy.transform.position = machineGun.transform.position;
            bulletEnemy.transform.eulerAngles = rotationBullet;
            bulletEnemy.gameObject.SetActive(true);

        }
        else if (trackEntry.Animation.Name.Equals(shotguns[0].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 1.5f, true, false);
            bulletEnemy.transform.position = boneGun[0].GetWorldPosition(skeletonAnimation.transform);
            //  bulletEnemy.transform.rotation = gunRotate[0].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundrocketBoss2);
            //  skeletonAnimation.AnimationState.SetAnimation(2, shotguns[1], false);
            StartCoroutine(delayShot(2, maxtimeDelayAttack2 / 2));
            //Debug.LogError("------ shot 1");
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[1].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 2, true, false);
            bulletEnemy.transform.position = boneGun[1].GetWorldPosition(skeletonAnimation.transform);
            //     bulletEnemy.transform.rotation = gunRotate[1].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);

            //skeletonAnimation.AnimationState.SetAnimation(3, shotguns[2], false);
            SoundController.instance.PlaySound(soundGame.soundrocketBoss2);
            StartCoroutine(delayShot(3, maxtimeDelayAttack2 / 2));
            // Debug.LogError("------ shot 2");
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[2].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 2.5f, true, false);
            bulletEnemy.transform.position = boneGun[2].GetWorldPosition(skeletonAnimation.transform);
            //    bulletEnemy.transform.rotation = gunRotate[2].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);

            //skeletonAnimation.AnimationState.SetAnimation(4, shotguns[3], false);
            SoundController.instance.PlaySound(soundGame.soundrocketBoss2);
            StartCoroutine(delayShot(4, maxtimeDelayAttack2 / 2));
            // Debug.LogError("------ shot 3");
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[3].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 3, true, false);
            bulletEnemy.transform.position = boneGun[3].GetWorldPosition(skeletonAnimation.transform);
            //   bulletEnemy.transform.rotation = gunRotate[3].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundrocketBoss2);
            if (enemyState == EnemyState.falldown)
                return;
            enemyState = EnemyState.idle;

            //Debug.LogError("------ shot 4");
        }
        else if (trackEntry.Animation.Name.Equals(aec.jump.name))
        {
            // isGrenade = 1;
        }
        else if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            countGrenade++;
            if (!incam)
                return;
            grenade = ObjectPoolManagerHaveScript.Instance.grenadeenemyBoss2Pooler.GetBulletEnemyPooledObject();
            grenade.AddProperties(damage2, bulletspeed2 / 2);
            grenade.SetDir(bulletspeed2 / 2.5f, true);
            grenade.transform.position = boneGun[4].GetWorldPosition(skeletonAnimation.transform);
            grenade.gameObject.SetActive(true);
            SoundController.instance.PlaySound(soundGame.soundenemygrenadeBoss2);
        }
    }
    IEnumerator delayShot(int index, float time)
    {
        yield return new WaitForSeconds(time);
        if (checkGun[index - 1].currentHealth > 0)
        {
            skeletonAnimation.AnimationState.SetAnimation(index, shotguns[index - 1], false);
            Debug.Log(index + ":" + checkGun[index - 1].currentHealth);
        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack2.name))
        {
            if (countGrenade == maxCountGrenade)
            {
                countGrenade = 0;
                maxCountGrenade = Random.Range(2, 5);
                skeletonAnimation.AnimationState.SetAnimation(enemyGrenade.index, aec.jumpOut, false);
                isGrenade = 2;
            }
        }
        else if (trackEntry.Animation.Name.Equals(aec.jumpOut.name))
        {
            isGrenade = 0;
            timeGrenade = maxtimeDelayAttack2 * 3;
        }
    }
    int countGrenade, maxCountGrenade;
    BulletEnemy grenade;
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public override void Dead()
    {
        base.Dead();
        ExploOffBoss();
    }
}
