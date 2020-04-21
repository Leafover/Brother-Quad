using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVController : MonoBehaviour
{
    public float damageBullet,maxTimeLive,maxTimeShoot;

    float timeShoot;
    GameObject bullet;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    float timeLive;
    public SkeletonAnimation sk;
    public AnimationReferenceAsset attack, fly, die;
    public enum STAGE
    {
        Begin, Normal, Die
    }
    public STAGE stage = STAGE.Begin;
    Bone boneBarrelGun, boneHandGrenade;
    [SpineBone]
    public string strboneBarrelGun;

    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(attack.name))
        {
            sk.AnimationState.SetAnimation(0, fly, true);
            Debug.LogError("zoooooooooooooo");
        }

    }
    Vector2 posGun()
    {
        return boneBarrelGun.GetWorldPosition(sk.transform);
    }
    private void Start()
    {
        timeLive = maxTimeLive;
        sk.AnimationState.Complete += OnComplete;
        stage = STAGE.Begin;
        boneBarrelGun = sk.Skeleton.FindBone(strboneBarrelGun);
    }
    void Shoot(float deltaTime)
    {
        if (stage != STAGE.Normal)
            return;
        timeShoot -= deltaTime;
        if (timeShoot <= 0)
        {
            timeShoot = maxTimeShoot;
            dirBullet = target - myPos();
            bullet = ObjectPoolerManager.Instance.bulletUAVPooler.GetPooledObject();
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = posGun();
            sk.state.SetAnimation(0, attack, false);
            bullet.SetActive(true);
        }

    }
    void Live(float deltaTime)
    {
        if (stage != STAGE.Normal)
            return;
        timeLive -= deltaTime;
        if (timeLive <= 0)
        {
            sk.AnimationState.SetAnimation(0, die, true);
            stage = STAGE.Die;
        }
    }
    public void CallMe()
    {
        stage = STAGE.Begin;
        transform.position = PlayerController.instance.transform.position;
        gameObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        Move(Time.deltaTime);
    }
    void Die(float deltaTime)
    {
        if (stage != STAGE.Die)
            return;
        timeLive -= deltaTime;
        if (timeLive <= -1f)
        {
            timeLive = maxTimeLive;
            sk.AnimationState.SetAnimation(0, fly, true);
            gameObject.SetActive(false);
            GameObject explo = ObjectPoolerManager.Instance.enemyExploPooler.GetPooledObject();
            explo.transform.position = gameObject.transform.position;
            explo.SetActive(true);
        }

    }
    private void Update()
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Die || GameController.instance.gameState == GameController.GameState.gameover)
            return;
        var deltaTime = Time.deltaTime;
        Live(deltaTime);
        SelectTarget(deltaTime);
        Die(deltaTime);
    }
    Vector2 posFollow;
    Vector2 posFollowPlayer()
    {
        posFollow.x = PlayerController.instance.GetTranformXPlayer() - 0.3f;
        posFollow.y = PlayerController.instance.GetTransformPlayer().position.y + 1f;
        return posFollow;
    }
    private void Move(float deltaTime)
    {
        if (stage == STAGE.Die)
            return;
        if (stage == STAGE.Begin)
        {
            transform.position = Vector2.MoveTowards(transform.position, posFollowPlayer(), deltaTime * 2);
            if (transform.position.y >= posFollow.y)
                stage = STAGE.Normal;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, posFollowPlayer(), deltaTime * 2);
        }
    }
    Vector2 myPos()
    {
        return transform.position;
    }
    public Vector2 target;
    Vector2 targetTemp;

    Vector2 GetTarget()
    {
        targetTemp = new Vector2(float.MaxValue, float.MaxValue);
        var dMin = float.MaxValue;
        for (int i = 0; i < GameController.instance.autoTarget.Count; i++)
        {
            var enemy = GameController.instance.autoTarget[i];
            if (!enemy.incam || enemy.currentHealth <= 0 || !enemy.gameObject.activeSelf)
            {
                continue;
            }
            var from = myPos();
            var to = enemy.Origin();
            var d = Vector2.Distance(from, to);
            if (d < dMin)
            {
                dMin = d;
                if (d >= 0.5f)
                {
                    targetTemp = enemy.transform.position;
                    GameController.instance.targetDetectSprite.transform.position = enemy.transform.position;
                }
            }
        }
        return targetTemp;
    }
    public void SelectTarget(float deltaTime)
    {
        if (GameController.instance.autoTarget.Count == 0)
        {
            FlipX = PlayerController.instance.FlipX;
            return;
        }
        target = GetTarget();
        FlipX = GetTarget().x < transform.position.x;
        Shoot(deltaTime);
    }
    public bool FlipX
    {
        get { return sk.skeleton.FlipX; }
        set { sk.skeleton.FlipX = value; }
    }
}
