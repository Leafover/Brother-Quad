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
    public override void Start()
    {
        base.Start();
        Init();
    }
    public void CalculateHealthAllGun()
    {
        healthTemp = currentHealth - centerEnergy.currentHealth - machineGun.currentHealth - enemyGrenade.currentHealth;

        for (int i = 0; i < gunList.Count; i++)
        {
            gunList[i].currentHealth = healthTemp / (gunList.Count);
        }
    }
    public void CalculateAgainHealthAllGunWhenDie(int indexGun)
    {
        if (indexGun < 4)
        {
            PlayAnim(indexGun, dieguns[indexGun]);
            if (indexGun == 3)
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
            PlayAnim(indexGun, aec.falldown);
            if (!GameController.instance.autoTarget.Contains(centerEnergy))
            {
                if (enemyGrenade.currentHealth <= 0)
                {
                    GameController.instance.autoTarget.Add(centerEnergy);
                    centerEnergy.currentHealth = currentHealth;
                    centerEnergy.gameObject.SetActive(true);
                    enemyState = EnemyState.falldown;
                }
            }
        }
        else if (indexGun == 6)
        {
            PlayAnim(indexGun, aec.die);
            ExploOffBoss();
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
                gunList[i].index = i;
                gunList[i].incam = true;
            }
        }
        enemyGrenade.index = 4;
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
        enemyGrenade.currentHealth = currentHealth / 100 * 20;

        CalculateHealthAllGun();
    }
    public override void Active()
    {
        base.Active();
        //   SoundController.instance.PlaySound(soundGame.soundDisplayMiniBoss2);
        gunList[0].gameObject.SetActive(true);
        GameController.instance.autoTarget.Add(gunList[0]);
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

        switch (enemyState)
        {
            case EnemyState.idle: //trạng thái bắn súng máy
                timePreviousAttack -= deltaTime;
                if (timePreviousAttack <= 0)
                {
                    skeletonAnimation.AnimationState.SetAnimation(machineGun.index, aec.attack1, false);
                    timePreviousAttack = maxtimeDelayAttack1;
                    combo++;
                    if (combo == randomCombo)
                    {
                        if (gunList.Count > 0)
                        {
                            enemyState = EnemyState.attack;
                            skeletonAnimation.AnimationState.SetAnimation(gunList[0].index, shotguns[gunList[0].index], false);
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

                break;
            case EnemyState.falldown:// nổ hết sạch súng
                AttackCenterEnergy(deltaTime);
                break;
        }

        GrenadeAttack(deltaTime);
    }
    int isGrenade = 0;
    void AttackCenterEnergy(float deltaTime)
    {
        timeEnergy -= deltaTime;
        if (timeEnergy <= 0)
        {
            bulletEnemy.AddProperties(damage2, bulletspeed2);
            bulletEnemy.dir1 = new Vector2(bulletspeed2 / 3, bulletspeed2 / 2);
            bulletEnemy.rid.gravityScale = 1;
            bulletEnemy.gameObject.layer = 17;
            bulletEnemy.Init(4);
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
                timeGrenade = maxtimeDelayAttack2;
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
    // public GameObject[] gunRotate;
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.bulletMachinegunBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage1, bulletspeed1);
            dirBullet = PlayerController.instance.GetTransformPlayer().position - machineGun.transform.position;
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bulletEnemy.transform.rotation = rotation;
            bulletEnemy.transform.position = machineGun.transform.position;
            bulletEnemy.gameObject.SetActive(true);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[0].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 3, true);
            bulletEnemy.transform.position = boneGun[0].GetWorldPosition(skeletonAnimation.transform);
            //  bulletEnemy.transform.rotation = gunRotate[0].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(gunList[1].index, shotguns[gunList[1].index], false);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[1].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 3, true);
            bulletEnemy.transform.position = boneGun[1].GetWorldPosition(skeletonAnimation.transform);
            //     bulletEnemy.transform.rotation = gunRotate[1].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(gunList[2].index, shotguns[gunList[2].index], false);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[2].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 3, true);
            bulletEnemy.transform.position = boneGun[2].GetWorldPosition(skeletonAnimation.transform);
            //    bulletEnemy.transform.rotation = gunRotate[2].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            skeletonAnimation.AnimationState.SetAnimation(gunList[3].index, shotguns[gunList[3].index], false);
        }
        else if (trackEntry.Animation.Name.Equals(shotguns[3].name))
        {
            if (!incam)
                return;
            bulletEnemy = ObjectPoolManagerHaveScript.Instance.rocketBoss2Pooler.GetBulletEnemyPooledObject();
            bulletEnemy.AddProperties(damage2, bulletspeed2 / 2);
            bulletEnemy.SetDir(bulletspeed2 / 3, true);
            bulletEnemy.transform.position = boneGun[3].GetWorldPosition(skeletonAnimation.transform);
            //   bulletEnemy.transform.rotation = gunRotate[3].transform.rotation;
            bulletEnemy.gameObject.SetActive(true);
            enemyState = EnemyState.idle;
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
            grenade.SetDir(bulletspeed2 / 3, true);
            grenade.transform.position = enemyGrenade.transform.position;
            grenade.gameObject.SetActive(true);


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
}
