using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraController : MonoBehaviour
{
    public GameObject nextPointCheck;
    public static CameraController instance;
    public List<Transform> posEnemyV2, posMiniBoss1;
    public List<GameObject> bouders;
    public float speed;
    public List<ProCamera2DTriggerBoundaries> procam2DTriggerBoudaries;

    public ProCamera2DNumericBoundaries NumericBoundaries;

    public int currentCamBoidaries;

    private void OnValidate()
    {
        NumericBoundaries = GetComponent<ProCamera2DNumericBoundaries>();
    }
    private void Awake()
    {
        instance = this;
        bouders[0].transform.localPosition = new Vector2(bouders[0].transform.localPosition.x, Camera.main.orthographicSize + 0.5f);
        bouders[1].transform.localPosition = new Vector2(bouders[1].transform.localPosition.x, -Camera.main.orthographicSize - 0.5f);
        bouders[2].transform.localPosition = new Vector2(Camera.main.orthographicSize + 3.61f, bouders[2].transform.localPosition.y);
        bouders[3].transform.localPosition = new Vector2(-Camera.main.orthographicSize - 3.61f, bouders[3].transform.localPosition.y);

        currentCamBoidaries = 0;
        NumericBoundaries.RightBoundary = procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x;

    }

    Vector2 _cameraSize;
    float velocity;
    public void Start()
    {
        ProCamera2D.Instance.OffsetX = 0.3f;
        ProCamera2D.Instance.OffsetY = 0f;
        _cameraSize.y = Camera.main.orthographicSize;
        _cameraSize.x = Mathf.Max(1, ((float)Screen.width / (float)Screen.height)) * _cameraSize.y;
    }
    public bool setBoudariesLeft = true;
    public void NextPoint()
    {
        currentCamBoidaries++;
        nextPointCheck.gameObject.SetActive(false);
    }
    public void OnUpdate(float deltaTime)
    {
        CacheSizeAndViewPos();
        if (!nextPointCheck.activeSelf)
        {
            var x1 = NumericBoundaries.RightBoundary;
            var x2 = procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x;
            var s = Mathf.Abs(x2 - x1);
            var v = speed;
            NumericBoundaries.RightBoundary = Mathf.SmoothDamp(NumericBoundaries.RightBoundary, procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x, ref velocity, s / v, speed * speed);
        }
        if (!setBoudariesLeft)
        {
            if (GameController.instance.autoTarget.Count == 0)
            {
                nextPointCheck.gameObject.SetActive(true);
                setBoudariesLeft = true;
                //  Debug.Log("active next pos Cam");
            }
            return;
        }
        var leftBoundary = transform.position.x - Size().x;
        NumericBoundaries.LeftBoundary = leftBoundary;
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
}
[System.Serializable]
public struct ViewPos
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
}
