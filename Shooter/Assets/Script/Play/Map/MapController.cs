using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using PathCreation;

public class MapController : MonoBehaviour
{
    public float distanceMap;
    public bool haveMiniBoss,haveBoss;
    public bool moreTypeE = true;
    public float timeDelayMax = 1.25f, timeDelay;
    public int maxSpawn = 3,countspawn;
    public bool autoSpawn;

   // GameObject enemy;
    EnemyBase _scriptE;
    Vector2 posSpawn;
    int randomPosY;

    int randomTypeEnemy;

    public void SpawnEnemy(float deltaTime)
    {
        if (!autoSpawn || countspawn == maxSpawn)
            return;
        timeDelay -= Time.deltaTime;
      //  Debug.Log(timeDelay);
        if (timeDelay <= 0)
        {
            if (moreTypeE)
                randomTypeEnemy = Random.Range(0, 2);
            else
                randomTypeEnemy = 0;
            randomPosY = Random.Range(0, 5);

            posSpawn.x = CameraController.instance.bouders[3].transform.position.x - 3;
            posSpawn.y = CameraController.instance.transform.position.y + randomPosY;


            switch(DataParam.indexStage)
            {
                case 0:
                    switch(randomTypeEnemy)
                    {
                        case 0:
                            _scriptE = ObjectPoolManagerHaveScript.Instance.enemy1Pooler.GetEnemyPooledObject();
                            break;
                        case 1:
                            _scriptE = ObjectPoolManagerHaveScript.Instance.enemy5Pooler.GetEnemyPooledObject();
                            break;
                    }
                    break;
                case 1:
                    switch (randomTypeEnemy)
                    {
                        case 0:
                            _scriptE = ObjectPoolManagerHaveScript.Instance.enemyN1Pooler.GetEnemyPooledObject();
                            break;
                        case 1:
                            _scriptE = ObjectPoolManagerHaveScript.Instance.enemyN2Pooler.GetEnemyPooledObject();
                            break;
                    }
                    break;
                    
            }

            // enemy.transform.position = posSpawn;
            _scriptE.transform.position = posSpawn;
          //   _scriptE = enemy.GetComponent<EnemyBase>();
            _scriptE.Init();
            _scriptE.enemyAutoSpawn = true;

            if (!CameraController.instance.CheckPoint())
            {
                if (!GameController.instance.enemyLockCam.Contains(_scriptE))
                {
                    GameController.instance.enemyLockCam.Add(_scriptE);
                }
            }

            timeDelay = timeDelayMax;
            countspawn++;

            //    enemy.SetActive(true);
            _scriptE.gameObject.SetActive(true);
        }

    }
    public void ResetAutoSpawn()
    {
        autoSpawn = false;
    }
    public void BeginAutoSpawn()
    {
        autoSpawn = true;
        countspawn = 0;
        timeDelay = timeDelayMax;
    }

    public ProCamera2DTriggerBoundaries[] procam2DTriggerBoudaries;
    public AutoSpawnEnemy[] autoSpawnEnemys;
    public PathCreator[] pathCreator;
    public GameObject pointBeginPlayer,endMap;
    private void OnValidate()
    {
        procam2DTriggerBoudaries = GetComponentsInChildren<ProCamera2DTriggerBoundaries>();
        pathCreator = GetComponentsInChildren<PathCreator>();
        autoSpawnEnemys = GetComponentsInChildren<AutoSpawnEnemy>();
        distanceMap = Mathf.Abs(endMap.transform.position.x - pointBeginPlayer.transform.position.x);      
    }
}
