﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

[System.Serializable]
public class AssetSpinePlayerController
{
    public AnimationReferenceAsset waitstandAnim, falldownAnim, jumpAnim, sitAnim, idleAnim, runForwardAnim, runBackAnim, aimTargetAnim, fireAnim, grenadeAnim, dieAnim, reloadAnim, winAnim, meleeAttackAnim;
}

[System.Serializable]
public class AssetSpineEnemyController
{
    public AnimationReferenceAsset attack1, attack2, attack3, idle, run, aimTargetAnim, run2, die, jumpOut, lowHPAnim, jump, falldown, standup;
}
[System.Serializable]
public class AllMap
{
    public List<MapController> listMap;
}


public class GameController : MonoBehaviour
{
    public AudioSource auBG;
    public List<AudioClip> bgClip;
    public List<TileVatPhamList> vatphamnhanduoc = new List<TileVatPhamList>();
    public int countCombo;
    public int totalDropCoin;

    public int countStar;
    public bool win;

    public UIPanel uiPanel;
    public List<AllMap> listMaps;
    public List<CritWhamBang> listcirtwhambang;
    public MapController currentMap;

    public GameObject targetDetectSprite;
    public List<AutoTarget> autoTarget;
    public List<EnemyBase> enemyLockCam;
    public List<ItemBase> itemDrops;
    public GameObject UIControll;
    public enum GameState
    {
        begin,
        play,
        gameover
    }
    public GameState gameState = GameState.begin;
    public UltimateJoystick joystickMove, joystickShot;
    public static GameController instance;
    [HideInInspector]
    public Vector2 movePosition, shootPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;

#if UNITY_EDITOR
        Application.targetFrameRate = 300;
#else
        Application.targetFrameRate = 60;
#endif
        gameState = GameState.play;
    }
    GameObject coinItem;
    public void SpawnCoin(int total, Vector2 pos)
    {
        for (int i = 0; i < total; i++)
        {
            coinItem = ObjectPoolerManager.Instance.coinItemPooler.GetPooledObject();
            coinItem.transform.position = pos;
            coinItem.SetActive(true);
        }
    }
    void AddProperties()
    {
        totalDropCoin = (int)DataController.instance.allMission[DataParam.indexStage].missionData[DataParam.indexMap].totaldropcoin;
        if (currentMap.haveMiniBoss)
        {
            totalDropCoin -= 8;
        }
        if (currentMap.haveBoss)
        {
            totalDropCoin -= 15;
        }
        uiPanel.Begin();
        SoundController.instance.PlaySound(soundGame.soundletgo);

        for (int i = 0; i < MissionController.Instance.listMissions.Count; i++)
        {
            MissionController.Instance.listMissions[i].currentValue = 0;
            MissionController.Instance.listMissions[i].isDone = false;
        }
        vatphamnhanduoc.Clear();
        for (int i = 0; i < DataController.instance.allTileVatPham[DataParam.indexStage].tilevatphamList.Count; i++)
        {
            if (DataController.instance.allTileVatPham[DataParam.indexStage].tilevatphamList[i].Level == DataParam.indexMap + 1)
                vatphamnhanduoc.Add(DataController.instance.allTileVatPham[DataParam.indexStage].tilevatphamList[i]);
        }
        //  ThemManh();
    }
    bool activeWarningEnemyLeft, activeWarningEnemyRight;
    private void Start()
    {
        currentMap = Instantiate(listMaps[DataParam.indexStage].listMap[DataParam.indexMap]);
        currentMap.transform.position = Vector2.zero;
        CameraController.instance.Init();
        PlayerController.instance.transform.position = currentMap.pointBeginPlayer.transform.position;
        Camera.main.transform.position = new Vector3(PlayerController.instance.transform.position.x + 3, Camera.main.transform.position.y, Camera.main.transform.position.z);
        uiPanel.levelText.text = "level:" + (DataParam.indexMap + 1);
        timeCountPlay = new WaitForSecondsRealtime(1);
        delaywinwait = new WaitForSeconds(2f);
        StartCoroutine(CountTimePlay());
        countCombo = 0;
        AddProperties();

        auBG.clip = bgClip[DataParam.indexStage];
        auBG.Play();
    }
    public float timeCountCombo, maxtimeCountCombo;
    public void AddCombo()
    {
        if (countCombo == 0)
        {
            uiPanel.comboDisplay.SetActive(true);
        }
        timeCountCombo = maxtimeCountCombo;
        countCombo++;
        if (countCombo == 2)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillx2);
        }
        else if (countCombo == 4)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillx4);
        }
        else if (countCombo == 6)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillx6);
        }
        else if (countCombo == 8)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillx8);
        }
        else if (countCombo == 10)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillx10);
        }
        else if (countCombo >= 12)
        {
            SoundController.instance.PlaySound(soundGame.soundmultikillmax);
        }
        uiPanel.comboNumberText.text = "X" + countCombo;
        if (countCombo >= 11)
        {
            uiPanel.comboText.text = "UNBELIEVABLE";
        }

        if (MissionController.Instance.listMissions[0].typeMission == 2 && countCombo >= MissionController.Instance.listMissions[0].valueMission)
            MissionController.Instance.DoMission(2, countCombo);
        if (MissionController.Instance.listMissions[1].typeMission == 2 && countCombo >= MissionController.Instance.listMissions[1].valueMission)
            MissionController.Instance.DoMission(2, countCombo);

        // Debug.Log("-------- show combo");
    }
    public void ResetCombo()
    {
        uiPanel.comboDisplay.SetActive(false);
        countCombo = 0;
        uiPanel.comboText.text = "Combo Kill";
    }
    //   public EnemyBase currentEnemyTarget;
    public void RemoveTarget(EnemyBase enemy)
    {
        if (autoTarget.Contains(enemy))
        {
            autoTarget.Remove(enemy);
            CheckHaveArrowLeft();
            CheckHaveArrowRight();
        }
    }
    private void JoystickMovement(UltimateJoystick joystick)
    {
        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);
        if (joystick.GetJoystickState())
        {
            PlayerController.instance.isBouderJoystickMove = joystick.GetDistance() >= 0.9f;
            OnMove(movePosition);
        }
        else
        {
            if (PlayerController.instance == null)
                return;
            StopMove();
        }
    }
    public void StopMove()
    {
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
            return;
        if (PlayerController.instance.playerState != PlayerController.PlayerState.Idle)
        {
            PlayerController.instance.playerState = PlayerController.PlayerState.Idle;
            if (PlayerController.instance.isfalldow)
            {
                PlayerController.instance.isWaitStand = true;
            }
        }
    }

    public void CheckAfterJump(UltimateJoystick joystick)
    {

        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        var angle = Mathf.Atan2(movePosition.x, movePosition.y) * Mathf.Rad2Deg;
        var h = movePosition.x;
        if (angle <= 135f && angle >= -135.5f)
        {
            PlayerController.instance.speedmove = h > 0 ? 1.5f : -1.5f;
            PlayerController.instance.dirMove = h > 0 ? true : false;
            PlayerController.instance.playerState = PlayerController.PlayerState.Run;
        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {
            PlayerController.instance.playerState = PlayerController.PlayerState.Sit;
        }
    }

    float getSpeed()
    {
        float speedMovetemp = !PlayerController.instance.isSlow ? PlayerController.instance.speedMoveMax : (PlayerController.instance.speedMoveMax - (PlayerController.instance.speedMoveMax / 100 * 50));
        return speedMovetemp;
    }
    Vector3 dustLeft = new Vector3(-180, -90, 0), dustRight = new Vector3(-180, 90, 0);
    public void OnMove(Vector2 axis)
    {

        var angle = Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg;
        var h = axis.x;
        if (angle <= 135f && angle >= -135.5f)
        {

            PlayerController.instance.speedmove = h > 0 ? 1 * getSpeed() : -1 * getSpeed();

            if (!PlayerController.instance.dustrun.activeSelf && PlayerController.instance.rid.velocity.x == 0)
            {
                PlayerController.instance.dustrun.transform.localEulerAngles = h > 0 ? dustRight : dustLeft;
                PlayerController.instance.dustrun.SetActive(true);
                PlayerController.instance.SetAnim();
            }

            PlayerController.instance.dirMove = h > 0 ? false : true;
            if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.instance.playerState = PlayerController.PlayerState.Run;

        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {
            if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.instance.playerState = PlayerController.PlayerState.Sit;
        }

        if (joystickShot.GetJoystickState())
            return;
        if (!PlayerController.instance.haveTarget && PlayerController.instance.isBouderJoystickMove)
            PlayerController.instance.FlipX = h < 0;
    }
    private void JoystickShooting(UltimateJoystick joystick)
    {
        if (PlayerController.instance == null)
            return;

        shootPosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        if (joystick.GetJoystickState())
        {
            TryShot();
            PlayerController.instance.isBouderJoystickShoot = joystick.GetDistance() >= 0.9f;


            if (autoTarget.Count == 0)
            {
                if (PlayerController.instance.isBouderJoystickShoot)
                {
                    PlayerController.instance.FlipX = shootPosition.x < 0;
                    PlayerController.instance.SelectNonTarget(shootPosition);
                }
            }
            else
            {
                PlayerController.instance.SelectTarget();
            }
        }
        else
        {
            if (autoTarget.Count == 0)
            {
                PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
            }
            else
            {
                PlayerController.instance.SelectTarget();
            }
        }
    }
    void OnUpdatePlayer(float deltaTime)
    {
        if (PlayerController.instance == null)
            return;
        PlayerController.instance.OnUpdate(deltaTime);
    }
    void OnUpdateEnemyManager(float deltaTime)
    {
        if (EnemyManager.instance == null)
            return;
        EnemyManager.instance.OnUpdate(deltaTime);
    }
    void OnUpdateCamera(float deltaTime)
    {
        if (CameraController.instance == null)
            return;
        CameraController.instance.OnUpdate(deltaTime);
    }
    //void OnUpdateCam(float deltaTime)
    //{
    //    if (CameraController.instance == null)
    //        return;
    //    CameraController.instance.OnUpdate(deltaTime);
    //}
    IEnumerator delayDisplayFinish()
    {
        yield return new WaitForSeconds(2f);
        uiPanel.DisplayFinish(countStar);
    }
    int timePlay;
    WaitForSecondsRealtime timeCountPlay;
    public void StopAll()
    {
        StopAllCoroutines();
    }
    System.TimeSpan timeSpanTemp;
    IEnumerator CountTimePlay()
    {
        yield return timeCountPlay;

        if (gameState == GameState.play)
        {
            timePlay++;
            timeSpanTemp = System.TimeSpan.FromSeconds(timePlay);
            uiPanel.timeText.text = timeSpanTemp.ToString("mm':'ss");
        }
        StartCoroutine(CountTimePlay());
    }
    public bool isDestroyBoss;
    [HideInInspector]
    public bool waitForWin;
    public int reviveCount = 0;
    int randonvictorysound;
    public void WinSound()
    {
        randonvictorysound = Random.Range(0, 2);
        if (randonvictorysound == 1)
            SoundController.instance.PlaySound(soundGame.soundwin);
        else
            SoundController.instance.PlaySound(soundGame.soundvictory1);
    }
    public void WinGame()
    {
        gameState = GameState.gameover;
        WinSound();
        PlayerController.instance.playerState = PlayerController.PlayerState.Win;
        PlayerController.instance.AnimWin();
        PlayerController.instance.rid.velocity = Vector2.zero;
        PlayerController.instance.box.enabled = false;
        PlayerController.instance.speedmove = 0;
        MissionController.Instance.DoMission(0, timePlay);
        MissionController.Instance.DoMission(3, (int)((PlayerController.instance.health / PlayerController.instance.maxHealth) * 100));
        MissionController.Instance.DoMission(6, reviveCount);
        //   Debug.Log(MissionController.Instance.listMissions[1].currentValue + ":" + MissionController.Instance.listMissions[1].valueMission);
        if (countStar == 0)
            countStar = 1;
        if (MissionController.Instance.listMissions[0].isDone)
            countStar++;
        if (MissionController.Instance.listMissions[1].isDone)
            countStar++;

        //   Debug.LogError("zooooooooooooo win");
    }



    public void DIE()
    {
        win = false;
        gameState = GameState.gameover;
    }
    void OnUpdateItemDrop(float deltaTime)
    {
        if (itemDrops.Count == 0)
            return;
        for (int i = 0; i < itemDrops.Count; i++)
        {
            itemDrops[i].CalculateDisable(deltaTime);
        }
    }
    void OnUpdateCountCombo(float deltaTime)
    {
        if (!uiPanel.comboDisplay.activeSelf)
            return;
        timeCountCombo -= deltaTime;
        if (timeCountCombo <= 0)
        {
            ResetCombo();
        }
    }
    void OnUpdateCritWhambang(float deltaTime)
    {
        for (int i = 0; i < listcirtwhambang.Count; i++)
        {
            listcirtwhambang[i].DisableMe(deltaTime);
        }
    }
    //float timeToWin;
    public void NotSoFastWin()
    {
        if (win)
        {
            win = false;
            //  timeToWin = 2;
        }
    }
    //void CalculateTimeToWin(float deltaTime)
    //{
    //    //if (gameState == GameState.play)
    //    //{
    //        if (!win)
    //            return;
    //        timeToWin -= deltaTime;
    //        if (timeToWin <= 0)
    //        {
    //            WinGame();
    //        }
    //    //}
    //}
    private void Update()
    {

        if (gameState == GameState.begin || gameState == GameState.gameover)
        {
            if (gameState == GameState.gameover)
            {
                StartCoroutine(delayDisplayFinish());
            }
            return;
        }
        var deltaTime = Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
        {
            TryShot();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TryJump();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {

        }

        if (activeWarningEnemyLeft && !uiPanel.leftwarning.activeSelf)
        {
            timecheckleft -= deltaTime;
            if (timecheckleft <= 0)
                uiPanel.leftwarning.SetActive(true);
        }
        if (activeWarningEnemyRight && !uiPanel.rightwarning.activeSelf)
        {
            timecheckright -= deltaTime;
            if (timecheckright <= 0)
                uiPanel.rightwarning.SetActive(true);
        }
        // CalculateTimeToWin(Time.deltaTime);
        OnUpdateEnemyManager(deltaTime);
        OnUpdateCamera(deltaTime);
        OnUpdateItemDrop(deltaTime);
        OnUpdateCountCombo(deltaTime);
        OnUpdateCritWhambang(deltaTime);
        uiPanel.CalculateMiniMap();

        if (PlayerController.instance.stun)
        {
            PlayerController.instance.CalculateTimeStun(deltaTime);
            return;
        }

        JoystickMovement(joystickMove);
        JoystickShooting(joystickShot);
        OnUpdatePlayer(deltaTime);
    }
    public void TryShot()
    {
        if (PlayerController.instance.stun)
            return;
        if (!PlayerController.instance.isMeleeAttack)
            PlayerController.instance.ShootDown();
        else
            PlayerController.instance.MeleeAttack();
    }
    public void TryJump()
    {
        if (PlayerController.instance.stun)
            return;
        PlayerController.instance.TryJump();
    }
    public void BtnGrenade()
    {
        if (PlayerController.instance.stun)
            return;
        PlayerController.instance.TryGrenade();
    }
    public void DelayWinFunc()
    {
        win = true;
        StartCoroutine(delayWin());
    }
    WaitForSeconds delaywinwait;
    IEnumerator delayWin()
    {
        yield return delaywinwait;
        if (win)
            WinGame();
    }

    int randomCertain;
    public void ThemManh()
    {
        for (int i = 0; i < vatphamnhanduoc.Count; i++)
        {
            for (int j = 0; j < vatphamnhanduoc[i].TotalNumber; j++)
            {
                randomCertain = Random.Range(0, 100);
                if (randomCertain < vatphamnhanduoc[i].Normal)
                {
                    //  Debug.Log(vatphamnhanduoc[i].ID + ": Normal");
                }
                else if (randomCertain >= vatphamnhanduoc[i].Normal && randomCertain < (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon))
                {
                    //  Debug.Log(vatphamnhanduoc[i].ID + ": Uncommon");
                }
                else if (randomCertain >= (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon) && randomCertain < (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon + vatphamnhanduoc[i].Rare))
                {
                    //   Debug.Log(vatphamnhanduoc[i].ID + ": Rare");
                }
                else if (randomCertain >= (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon + vatphamnhanduoc[i].Rare) && randomCertain < (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon + vatphamnhanduoc[i].Rare + vatphamnhanduoc[i].Epic))
                {
                    //  Debug.Log(vatphamnhanduoc[i].ID + ": Epic");
                }
                else if (randomCertain >= (vatphamnhanduoc[i].Normal + vatphamnhanduoc[i].Uncommon + vatphamnhanduoc[i].Rare + vatphamnhanduoc[i].Epic))
                {
                    //  Debug.Log(vatphamnhanduoc[i].ID + ": Legendary");
                }
            }
        }
    }
    public void ResetActiveLeft()
    {
        activeWarningEnemyLeft = false;
        timecheckleft = 2;
        uiPanel.leftwarning.SetActive(false);
    }
    public void ResetActiveRight()
    {
        activeWarningEnemyRight = false;
        timecheckright = 2;
        uiPanel.rightwarning.SetActive(false);
    }
    public void CheckHaveArrowLeft()
    {
        if (activeWarningEnemyLeft || autoTarget.Count > 0 || enemyLockCam.Count == 0 || CameraController.instance.setBoudariesLeft || enemyLockCam == null)
            return;

        try
        {
            for (int i = 0; i < enemyLockCam.Count; i++)
            {
                if (!enemyLockCam[i].incam && enemyLockCam[i].transform.position.x < Camera.main.transform.position.x)
                {
                    activeWarningEnemyLeft = true;
                    break;
                }

            }
        }
        catch
        {

        }
    }
    float timecheckright, timecheckleft;
    public void CheckHaveArrowRight()
    {

        if (activeWarningEnemyRight || autoTarget.Count > 0 || enemyLockCam.Count == 0 || CameraController.instance.setBoudariesLeft || enemyLockCam == null)
            return;
        try
        {
            for (int i = 0; i < enemyLockCam.Count; i++)
            {
                if (enemyLockCam[i] != null)
                {
                    if (!enemyLockCam[i].incam && enemyLockCam[i].transform.position.x > Camera.main.transform.position.x)
                    {
                        activeWarningEnemyRight = true;
                        break;
                    }
                }
            }
        }
        catch
        {

        }
    }
}
