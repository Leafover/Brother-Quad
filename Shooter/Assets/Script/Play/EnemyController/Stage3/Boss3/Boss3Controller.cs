using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Controller : EnemyBase
{
    public LineRenderer line1, line2;
    public GameObject eye1, eye2, endLine1, endLine2;
    public override void Start()
    {
        base.Start();
        Init();
    }
    public override void Init()
    {
        base.Init();
        if (!EnemyManager.instance.boss3s.Contains(this))
        {
            EnemyManager.instance.boss3s.Add(this);
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (EnemyManager.instance.boss3s.Contains(this))
        {
            EnemyManager.instance.boss3s.Remove(this);
        }
    }
    public override void Active()
    {
        base.Active();
        enemyState = EnemyState.idle;
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
            case EnemyState.idle:
                if (Mathf.Abs(transform.position.x - Camera.main.transform.position.x) <= 0.5f)
                {
                    if (GameController.instance.uiPanel.CheckWarning())
                    {
                        GameController.instance.uiPanel.warning.SetActive(false);
                        GameController.instance.autoTarget.Add(this);
                        takeDamageBox.enabled = true;
                        // Debug.Log("attack boss");
                    }
                    PlayAnim(1, aec.idle, true);
                    enemyState = EnemyState.attack;
                    takeDamageBox.enabled = true;
                    randomCombo = Random.Range(0, 3);
                }
                break;
            case EnemyState.attack:
                switch (typeAttack)
                {
                    case 0:
                        Attack1(deltaTime);
                        break;
                    case 1:
                        Attack2(deltaTime);
                        break;
                    case 2:
                        Attack3(deltaTime);
                        break;
                    case 3:
                        Attack4(deltaTime);
                        break;
                }
                break;
            case EnemyState.run:
                break;
            case EnemyState.falldown:
                break;
        }
    }

    void Attack1(float deltaTime)
    {

    }
    void Attack2(float deltaTime)
    {
        timePreviousAttack -= deltaTime;
        if (timePreviousAttack > 0 && timePreviousAttack <= maxtimeDelayAttack2 / 2)
        {
            PlayAnim(0, aec.attack2, false);
        }
        else if (timePreviousAttack <= 0 && timePreviousAttack > -3)
        {

            if (!eye1.activeSelf)
            {
                eye1.SetActive(true);
                eye2.SetActive(true);
                endLine1.SetActive(true);
                endLine2.SetActive(true);
            }

            line1.SetPosition(0, eye1.transform.position);
            line2.SetPosition(0, eye2.transform.position);

            line1.SetPosition(1, endLine1.transform.position);
            line2.SetPosition(1, endLine2.transform.position);
        }
        else if (timePreviousAttack <= -3)
        {
            eye1.SetActive(false);
            eye2.SetActive(false);
            endLine1.SetActive(false);
            endLine2.SetActive(false);
            enemyState = EnemyState.falldown;
        }
    }
    void Attack3(float deltaTime)
    {

    }
    void Attack4(float deltaTime)
    {

    }
    void Def()
    {

    }
    protected override void OnEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.OnEvent(trackEntry, e);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {

        }
    }
    protected override void OnComplete(TrackEntry trackEntry)
    {
        base.OnComplete(trackEntry);
        if (trackEntry.Animation.Name.Equals(aec.attack1.name))
        {

        }
    }
}
