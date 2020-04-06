using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraController : MonoBehaviour
{
    public enum ShakeType
    {
        ExplosionShake,
        ExplosionBossShake
    }
    public ShakeType shakeType;
    public SpriteRenderer nextPointCheck;
    public static CameraController instance;
    public List<Transform> posMove, posBossMove;
    public List<GameObject> bouders;
    public float speed;
    public ProCamera2DNumericBoundaries NumericBoundaries;
    public ProCamera2DShake prcShake;
    public ProCamera2D procam;
    public int currentCamBoidaries, currentCheckPoint;

    public List<GameObject> effectstage;

    public GameObject objRedZone;
    public SpriteRenderer redzone;
    public Transform[] pointlineredzone;
    public LineRenderer lineredzone;

    public bool activeRedZone;

    private void OnValidate()
    {
        NumericBoundaries = GetComponent<ProCamera2DNumericBoundaries>();
        prcShake = GetComponent<ProCamera2DShake>();
        procam = GetComponent<ProCamera2D>();
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        Debug.LogError(height + ":" + width);

        bouders[0].transform.localPosition = new Vector2(bouders[0].transform.localPosition.x, /*Camera.main.orthographicSize + 0.5f*/height / 2 + 0.5f);
        bouders[1].transform.localPosition = new Vector2(bouders[1].transform.localPosition.x, /*-Camera.main.orthographicSize - 0.5f*/-height / 2 - 0.5f);
        bouders[2].transform.localPosition = new Vector2(/*Camera.main.orthographicSize + 4.5f*/width / 2 + 0.5f, bouders[2].transform.localPosition.y);
        bouders[3].transform.localPosition = new Vector2(/*-Camera.main.orthographicSize - 4.5f*/-width / 2 - 0.5f, bouders[3].transform.localPosition.y);
        currentCamBoidaries = 0;
        NumericBoundaries.enabled = false;
        prcShake.enabled = false;
        procam.enabled = false;


    }
    public void Init()
    {
        NumericBoundaries.enabled = true;
        prcShake.enabled = true;
        procam.enabled = true;
        procam.AddCameraTarget(PlayerController.instance.transform);
        NumericBoundaries.RightBoundary = GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x;
        //  NumericBoundaries.TopBoundary = 4;

        if (!GameController.instance.currentMap.isRedZone)
        {
        }
        else
        {

            pointlineredzone[0].transform.localPosition = new Vector2(pointlineredzone[0].transform.localPosition.x, bouders[0].transform.localPosition.y);
            pointlineredzone[1].transform.localPosition = new Vector2(pointlineredzone[1].transform.localPosition.x, bouders[1].transform.localPosition.y);
            redzone.transform.localPosition = new Vector2(bouders[3].transform.localPosition.x, redzone.transform.localPosition.y);
            lineredzone.SetPosition(0, pointlineredzone[0].position);
            lineredzone.SetPosition(1, pointlineredzone[1].position);
            lineredzone.transform.position = GameController.instance.currentMap.pointBeginPlayer.transform.position;
            BeginRedZone();
        }
    }

    public void BeginRedZone()
    {
        ActiveRedZone();
        objRedZone.SetActive(true);
    }
    Color red = new Color(1, 0.03798597f, 0, 0.2235294f), blue = new Color(0, 0.8888178f, 1, 0.2235294f);
    public void ActiveRedZone()
    {
        redzone.color = red;
        lineredzone.SetColors(Color.red, Color.red);
        activeRedZone = true;
    }
    public void DisableRedZone()
    {
        redzone.color = blue;
        lineredzone.SetColors(Color.blue, Color.blue);
        activeRedZone = false;
    }
    Vector2 posLineZone;
    public float speedRedZone = 0.2f;
    public void SetPosRedZone(float deltaTime)
    {
        if (!activeRedZone || !objRedZone.activeSelf || lineredzone.transform.position.x >= bouders[2].transform.position.x)
            return;

        lineredzone.SetPosition(0, pointlineredzone[0].position);
        lineredzone.SetPosition(1, pointlineredzone[1].position);

        posLineZone.x = lineredzone.transform.position.x;
        posLineZone.y = lineredzone.transform.position.y;
        posLineZone.x += deltaTime * speedRedZone;

        lineredzone.transform.position = posLineZone;
    }
    Vector2 _cameraSize;
    float velocity;
    public void Start()
    {
        //ProCamera2D.Instance.OffsetX = 0.3f;
        //ProCamera2D.Instance.OffsetY = 0f;
        _cameraSize.y = Camera.main.orthographicSize;
        _cameraSize.x = Mathf.Max(1, ((float)Screen.width / (float)Screen.height)) * _cameraSize.y;

        if (DataParam.indexStage == 0 && DataParam.indexMap > 4)
            return;
        effectstage[DataParam.indexStage].SetActive(true);
    }
    public bool setBoudariesLeft = true;

    public bool CheckPoint()
    {
        return currentCamBoidaries < GameController.instance.currentMap.procam2DTriggerBoudaries.Length - 1;
    }

    public void NextPoint()
    {

        if (CheckPoint())
        {
            currentCamBoidaries++;
            nextPointCheck.gameObject.SetActive(true);
            lockCamPos = Camera.main.transform.position.x;
            nextPointCheck.enabled = true;
            SoundController.instance.PlaySound(soundGame.soundletgo);

            GameController.instance.ResetActiveLeft();
            GameController.instance.ResetActiveRight();

            GameController.instance.currentMap.ResetAutoSpawn();

            setBoudariesLeft = true;
        }
        else
        {
            if (GameController.instance.isDestroyBoss)
            {
                return;
            }
            //   GameController.instance.waitForWin = true;
            GameController.instance.DelayWinFunc();
        }
    }
    private void LateUpdate()
    {
        CacheSizeAndViewPos();

        var x1 = NumericBoundaries.RightBoundary;
        var x2 = GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x;
        var s = Mathf.Abs(x2 - x1);
        var v = speed * 500;
        // NumericBoundaries.RightBoundary = Mathf.SmoothStep(NumericBoundaries.RightBoundary, GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x, speed);
        NumericBoundaries.RightBoundary = Mathf.SmoothDamp(NumericBoundaries.RightBoundary, GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + GameController.instance.currentMap.procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x, ref velocity, /*speed * 100*/s / v);

        if (!setBoudariesLeft || prcShake.CheckShaking())
            return;

        var leftBoundary = transform.position.x - Size().x;
        NumericBoundaries.LeftBoundary = leftBoundary;
    }
    float lockCamPos;
    public void OnUpdate(float deltaTime)
    {
        if (GameController.instance.win || PlayerController.instance.playerState == PlayerController.PlayerState.Die)
            return;


        if (!setBoudariesLeft)
        {
            if (GameController.instance.enemyLockCam.Count == 0)
            {
                NextPoint();
            }
            GameController.instance.currentMap.SpawnEnemy(deltaTime);
        }
        else
        {
            if (Camera.main.transform.position.x - lockCamPos > 1)
            {
                nextPointCheck.gameObject.SetActive(false);
            }
        }

        SetPosRedZone(deltaTime);
    }

    public Vector2 Size()
    {
        return _cameraSize;
    }
    private void CacheSizeAndViewPos()
    {
        _cameraSize.y = Camera.main.orthographicSize;
        _cameraSize.x = Mathf.Max(1, ((float)Screen.width / (float)Screen.height)) * _cameraSize.y;
        viewPos.minX = transform.position.x - _cameraSize.x;
        viewPos.minY = transform.position.y - _cameraSize.y;
        viewPos.maxX = transform.position.x + _cameraSize.x;
        viewPos.maxY = transform.position.y + _cameraSize.y;
    }
    public ViewPos viewPos;



    public void Shake(ShakeType type = ShakeType.ExplosionShake)
    {
        if (GameController.instance.gameState == GameController.GameState.gameover)
            return;
        var shakePreset = ProCamera2DShake.Instance.ShakePresets[(int)type];
        ProCamera2DShake.Instance.Shake(shakePreset);
    }

}
[System.Serializable]
public struct ViewPos
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}



