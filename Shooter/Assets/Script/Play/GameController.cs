using System.Collections;
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
    public AnimationReferenceAsset attack1, attack2, attack3, idle, run, aimTargetAnim, run2, die, jumpOut, lowHPAnim;
}

public class GameController : MonoBehaviour
{
    public int countCombo;
    public int totalDropCoin;

    public int countStar;
    public bool win;

    public UIPanel uiPanel;
    public List<MapController> listMap;
    public List<CritWhamBang> listcirtwhambang;
    public MapController currentMap;

    public GameObject targetDetectSprite;
    public List<EnemyBase> autoTarget, enemyLockCam;
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
        totalDropCoin = (int)DataController.instance.missions[DataParam.indexMap].totaldropcoin;
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
    }

    private void Start()
    {
        currentMap = Instantiate(listMap[DataParam.indexMap]);
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
        if(countCombo == 2)
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
        else if(countCombo == 8)
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
            autoTarget.Remove(enemy);
    }
    private void JoystickMovement(UltimateJoystick joystick)
    {
        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);
        if (joystick.GetJoystickState())
        {
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
        //   PlayerController.instance.FlipX = h > 0 ? false : true;
        if (angle <= 135f && angle >= -135.5f)
        {
            PlayerController.instance.speedmove = h > 0 ? 1 * getSpeed() : -1 * getSpeed();

            if (!PlayerController.instance.dustrun.activeSelf && PlayerController.instance.rid.velocity.x == 0)
            {
                PlayerController.instance.dustrun.transform.localEulerAngles = h > 0 ? dustRight : dustLeft;
                PlayerController.instance.dustrun.SetActive(true);
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
        if (!PlayerController.instance.haveTarget)
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
            PlayerController.instance.isBouderJoystick = joystick.GetDistance() >= 0.9f;


            if (autoTarget.Count == 0)
            {
                if (PlayerController.instance.isBouderJoystick)
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
    float timePlay;
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

    public void WinGame()
    {
        gameState = GameState.gameover;
        PlayerController.instance.AnimWin();
        PlayerController.instance.rid.velocity = Vector2.zero;
        PlayerController.instance.box.enabled = false;
        if (countStar == 0)
            countStar = 1;

        if (PlayerController.instance.health >= PlayerController.instance.maxHealth / 2)
        {
            countStar++;
        }
        if (timePlay <= 120)
        {
            countStar++;
        }
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
        if(timeCountCombo <= 0)
        {
            ResetCombo();
        }
    }
    void OnUpdateCritWhambang(float deltaTime)
    {
        for(int i = 0; i < listcirtwhambang.Count; i ++)
        {
            listcirtwhambang[i].DisableMe(deltaTime);
        }
    }
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
        JoystickMovement(joystickMove);
        JoystickShooting(joystickShot);
        OnUpdatePlayer(deltaTime);
        OnUpdateEnemyManager(deltaTime);
        OnUpdateCamera(deltaTime);
        OnUpdateItemDrop(deltaTime);
        OnUpdateCountCombo(deltaTime);
        OnUpdateCritWhambang(deltaTime);
        uiPanel.CalculateMiniMap();
        if (Input.GetKey(KeyCode.S))
        {
            TryShot();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TryJump();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {

        }

    }
    public void TryShot()
    {
        if (!PlayerController.instance.isMeleeAttack)
            PlayerController.instance.ShootDown();
        else
            PlayerController.instance.MeleeAttack();
    }
    public void TryJump()
    {
        PlayerController.instance.TryJump();
    }
    public void BtnGrenade()
    {
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
        WinGame();
    }
}
