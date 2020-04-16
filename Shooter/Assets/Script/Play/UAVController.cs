using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVController : MonoBehaviour
{
    public float damageBullet;
    float timeShoot;
    GameObject bullet;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    float timeLive;
    public SkeletonAnimation sk;
    public AnimationReferenceAsset attack, fly;

    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals(attack.name))
        {
            sk.AnimationState.SetAnimation(0, fly, true);
            Debug.LogError("zoooooooooooooo");
        }
    }
    private void Start()
    {
        timeLive = 10;
        damageBullet = 2;
        sk.AnimationState.Complete += OnComplete;
    }
    void Shoot(float deltaTime)
    {
        timeShoot -= deltaTime;
        if (timeShoot <= 0)
        {
            timeShoot = 0.15f;
            dirBullet = target - myPos();
            bullet = ObjectPoolerManager.Instance.bulletUAVPooler.GetPooledObject();
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = transform.position;
            sk.state.SetAnimation(0, attack, false);
            bullet.SetActive(true);
        }
    }
    void Live(float deltaTime)
    {
        timeLive -= deltaTime;
        if (timeLive <= 0)
        {
            sk.AnimationState.SetAnimation(0, fly, true);
            gameObject.SetActive(false);
            timeLive = 20;
        }
    }
    private void Update()
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Die || GameController.instance.gameState == GameController.GameState.gameover)
            return;
        var deltaTime = Time.deltaTime;
        Live(deltaTime);
        SelectTarget(deltaTime);
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
