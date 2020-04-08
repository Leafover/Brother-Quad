using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float distance;
    public float speedmove;
    public GameObject poitRayGround;
    public float radius;
    public LayerMask lm;
    Vector2 movePos;
    public bool isGround;
    public AnimationReferenceAsset run, run2, idle, jump, die, aim, win, fire;
    public AnimationReferenceAsset currentAnim;
    public SkeletonAnimation sk;
    public Rigidbody2D rid;
    public SkeletonAnimation du;
    public float health, maxHealth;
    [SpineBone]
    public string strboneBarrelGun;
    public LineBlood lineBlood;
    bool candoublejump;
    private void OnValidate()
    {
        if (rid == null)
        {
            rid = GetComponent<Rigidbody2D>();
        }
    }
    public void AnimWin()
    {
        sk.ClearState();
        if (currentAnim == win)
            return;
        sk.AnimationState.SetAnimation(0, win, true);
        currentAnim = win;
    }
    public void ResetPosRevive()
    {
        rid.velocity = Vector2.zero;
        speedmove = 0;
        rid.gravityScale = 1;
        transform.position = new Vector2(PlayerController.instance.GetTranformXPlayer() + 0.2f, PlayerController.instance.transform.position.y + 1f);
    }
    public void Start()
    {
        sk.Initialize(true);
        boneBarrelGun = sk.Skeleton.FindBone(strboneBarrelGun);
        lineBlood.Reset();
        du.AnimationState.Complete += OnCompleteDu;
        health = maxHealth = PlayerController.instance.maxHealth;
    }

    private void OnCompleteDu(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals("1"))
        {

        }
        if (trackEntry.Animation.Name.Equals("2"))
        {
            du.gameObject.SetActive(false);
            sk.gameObject.SetActive(true);
            sk.state.SetAnimation(2, aim, false);
            layerTarget = PlayerController.instance.layerTarget;
            du.AnimationState.Complete -= OnCompleteDu;
        }
    }
    public bool FlipX
    {
        get { return sk.skeleton.FlipX; }
        set { sk.skeleton.FlipX = value; }
    }
    float timeShoot;
    GameObject bullet;
    Vector2 dirBullet;
    float angle;
    Quaternion rotation;
    Vector2 posGun()
    {
        return boneBarrelGun.GetWorldPosition(sk.transform);
    }
    Vector2 GetTargetTranform()
    {
        return targetPos.transform.position;
    }
    void Shoot()
    {
        timeShoot -= Time.deltaTime;
        if (timeShoot <= 0)
        {
            timeShoot = 1;
            sk.AnimationState.SetAnimation(1, fire, false);
            bullet = ObjectPoolerManager.Instance.bulletW2Pooler.GetPooledObject();
            dirBullet = GetTargetTranform() - posGun();
            angle = Mathf.Atan2(dirBullet.y, dirBullet.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.transform.rotation = rotation;
            bullet.transform.position = posGun();
            bullet.SetActive(true);
        }
    }

    private void Update()
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Die || GameController.instance.gameState == GameController.GameState.gameover)
            return;

        isGround = Physics2D.OverlapCircle(poitRayGround.transform.position, radius, lm);
        if (du.gameObject.activeSelf)
        {
            if (isGround)
            {
                if (du.AnimationName == "1")
                {
                    du.AnimationState.SetAnimation(0, "2", false);
                    rid.gravityScale = 1;
                }
            }
            return;
        }

        if (transform.position.x < PlayerController.instance.GetTranformXPlayer())
        {
            if (transform.position.x < PlayerController.instance.GetTranformXPlayer() - distance)
            {
                speedmove = PlayerController.instance.speedMoveMax;
            }
            else
            {
                speedmove = 0;
            }
        }
        else
        {
            if (transform.position.x > PlayerController.instance.GetTranformXPlayer() + distance)
            {
                speedmove = -PlayerController.instance.speedMoveMax;
            }
            else
            {
                speedmove = 0;
            }
        }

        if (isGround)
        {
            if (rid.velocity.y <= 0)
            {
                if (speedmove != 0)
                {
                    if (speedmove < 0)
                    {
                        if (FlipX)
                        {
                            if (currentAnim != run)
                            {
                                currentAnim = run;
                                sk.AnimationState.SetAnimation(0, run, true);
                            }
                        }
                        else
                        {
                            if (currentAnim != run2)
                            {
                                currentAnim = run2;
                                sk.AnimationState.SetAnimation(0, run2, true);
                            }
                        }
                    }
                    else
                    {
                        if (FlipX)
                        {
                            if (currentAnim != run2)
                            {
                                currentAnim = run2;
                                sk.AnimationState.SetAnimation(0, run2, true);
                            }
                        }
                        else
                        {
                            if (currentAnim != run)
                            {
                                currentAnim = run;
                                sk.AnimationState.SetAnimation(0, run, true);
                            }
                        }
                    }
                }
                else
                {
                    if (currentAnim != idle)
                    {
                        currentAnim = idle;
                        sk.AnimationState.SetAnimation(0, idle, true);
                    }
                }
            }
        }

        if (GameController.instance.autoTarget.Count > 0)
        {
            SelectTarget();
        }
        else
        {
            SelectNonTarget();
        }

        movePos = rid.velocity;
        movePos.x = speedmove;
        movePos.y = rid.velocity.y;
        rid.velocity = movePos;
        Jump();
        targetPos.position = Vector2.MoveTowards(targetPos.position, target, Time.deltaTime * 35f);
    }
    public Transform targetPos;
    public Vector2 target;
    Vector2 jumpVelo;
    public float forceJump;
    public float timeJump;
    private float force;
    public void Jump()
    {
        if (PlayerController.instance.GetTransformPlayer().position.y < transform.position.y + 0.5f)
            return;

        if (isGround && PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
        {
            StartCoroutine(JumpTemp());
        }
    }
    private IEnumerator JumpTemp()
    {
        float timeUp = timeJump * 0.5f;
        if (currentAnim != jump)
        {
            sk.AnimationState.SetAnimation(0, jump, true);
            currentAnim = jump;
            for (float t = 0; t <= timeUp; t += Time.deltaTime)
            {
                force = forceJump * (timeUp - t);
                //  jumpVelo = rid.velocity;
                jumpVelo.x = rid.velocity.x;
                jumpVelo.y = force;
                rid.velocity = jumpVelo;
                yield return null;
            }
        }
    }


    public void DIE()
    {
        GameController.instance.NPCDIE();
        if (currentAnim == die)
        {
            sk.ClearState();
            return;
        }
        sk.AnimationState.SetAnimation(0, die, false);
        currentAnim = die;
        Debug.LogError("DIE");
    }


    public Vector2 GetOriginGun()
    {
        var vt2 = new Vector2();
        vt2 = boneBarrelGun.GetWorldPosition(sk.transform);
        return vt2;
    }

    Bone boneBarrelGun;
    Vector2 targetTemp;
    public LayerMask layerTarget;
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
                    haveTarget = true;
                }
                else
                {
                    targetTemp = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
                    haveTarget = false;
                }
            }
        }

        return targetTemp;
    }
    public void SelectNonTarget()
    {

        if (PlayerController.instance.GetTranformXPlayer() < transform.position.x - distance)
        {
            if (PlayerController.instance.FlipX)
                FlipX = true;
        }
        if (PlayerController.instance.GetTranformXPlayer() > transform.position.x + distance)
        {
            if (!PlayerController.instance.FlipX)
                FlipX = false;
        }
        //  FlipX = PlayerController.instance.FlipX;
        target = GetTargetFromDirection(!FlipX ? Vector2.right : Vector2.left);
        haveTarget = false;

    }
    public void SelectTarget()
    {
        target = GetTarget();
        FlipX = GetTarget().x < transform.position.x;

        Shoot();
    }


    public void TakeDamage(float damage, bool isNotDestroyAll = true)
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Die || GameController.instance.gameState == GameController.GameState.gameover || GameController.instance.win)
            return;

        health -= damage;
        ShowLineBlood();

        if (health <= 0)
        {
            DIE();
        }
    }

    public void ShowLineBlood()
    {
        lineBlood.Show(health, maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 26:
                TakeDamage(PlayerController.instance.damgeGrenade / 3);
                break;
        }
    }
}
