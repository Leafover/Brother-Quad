using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Controller : EnemyBase
{
    public LineRenderer line1, line2;
    public GameObject eye1, eye2;
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
                break;
            case EnemyState.run:
                break;
            case EnemyState.falldown:
                break;
        }
    }
    void Attack1()
    {

    }
    void Attack2()
    {
        line1.SetPosition(0, eye1.transform.position);
        line2.SetPosition(0, eye2.transform.position);
    }
    void Attack3()
    {

    }
    void Attack4()
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
