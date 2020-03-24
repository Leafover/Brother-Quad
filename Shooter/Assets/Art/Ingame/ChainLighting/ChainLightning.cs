using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainLightning : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject lineRendererPrefab;
    public GameObject lightRendererPrefab;

    [Header("Config")]
    public int chainLength;
    public int lightnings;

    private float nextRefresh;
    private float segmentLength = 0.2f;

    private List<LightningBolt> LightningBolts { get; set; }
    private List<Vector2> Targets { get; set; }

    public List<EnemyBase> targetPos;

    void Awake()
    {
        LightningBolts = new List<LightningBolt>();
        Targets = new List<Vector2>();

        LightningBolt tmpLightningBolt;
        for (int i = 0; i < chainLength; i++)
        {
            tmpLightningBolt = new LightningBolt(segmentLength, i);
            tmpLightningBolt.Init(lightnings, lineRendererPrefab, lightRendererPrefab, gameObject);
            LightningBolts.Add(tmpLightningBolt);
        }
        //  BuildChain();

    }

    //public void BuildChain()
    //{
    //    //Build a chain, in a real project this might be enemies ;)
    //    for (int i = 0; i < targetPos.Count; i++)
    //    {
    //    //    Targets.Add(new Vector2(/*Random.Range (-2f, 2f), Random.Range (-2f, 2f)*/targetPos[i].position.x, targetPos[i].position.y));
    //        LightningBolts[i].Activate();

    //   //     Debug.LogError("wtf");
    //    }
    //}
    public GameObject originPos;
    void Update()
    {
        //Debug.Log(LightningBolts.Count + ":" + targetPos.Count);
        //Refresh the LightningBolts
        //if (Time.time > nextRefresh)
        //{

        //  BuildChain();
        //if (Input.GetKey(KeyCode.Space))
        //{

        for (int i = 0; i < targetPos.Count; i++)
        {
            LightningBolts[i].Activate();
            if (i == 0)
            {
                LightningBolts[i].DrawLightning(originPos.transform.position, targetPos[i].transform.position);
            }
            else
            {
                LightningBolts[i].DrawLightning(targetPos[i - 1].transform.position, targetPos[i].transform.position);
            }
        }
        //}
        //else if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    for (int i = 0; i < targetPos.Count; i++)
        //    {
        //        LightningBolts[i].DisActive();
        //    }
        //}
        //  nextRefresh = Time.time + 0.01f;

        // }
    }
    int takecrithit;
    private void OnDisable()
    {
        for (int i = 0; i < targetPos.Count; i++)
        {
            takecrithit = Random.Range(0, 100);
            if (takecrithit <= PlayerController.instance.critRate)
            {
                targetPos[i].TakeDamage(PlayerController.instance.damageBullet / 3 + (PlayerController.instance.damageBullet / 3 / 100 * PlayerController.instance.critDamage), true,false,false);
                //if (!GameController.instance.listcirtwhambang[0].gameObject.activeSelf)
                //    SoundController.instance.PlaySound(soundGame.soundCritHit);
                GameController.instance.listcirtwhambang[0].DisplayMe(transform.position);
            }
            else
            {
                targetPos[i].TakeDamage(PlayerController.instance.damageBullet / 3,false,false,false);
            }
            LightningBolts[i].DisActive();
        }
        targetPos.Clear();
    }
    EnemyBase enemyBase;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 19)
        {
            if (collision.tag == "gunboss")
                return;
            if (targetPos.Count >= 3)
                return;
            enemyBase = collision.GetComponent<EnemyBase>();
            if (enemyBase.incam && enemyBase.gameObject != originPos)
            {
                if (targetPos.Count == 0)
                {
                    SoundController.instance.PlaySound(soundGame.soundw4truyendien);
                }
                targetPos.Add(enemyBase);
            }
        }
    }
}
