using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraController : MonoBehaviour
{
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
    bool setBoudariesLeft = true;
    public void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentCamBoidaries < procam2DTriggerBoudaries.Count)
            {
                currentCamBoidaries++;

            }
        }
        //  NumericBoundaries.RightBoundary = Mathf.SmoothStep(NumericBoundaries.RightBoundary, procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x, deltaTime * speed);
        NumericBoundaries.RightBoundary = Mathf.SmoothDamp(NumericBoundaries.RightBoundary, procam2DTriggerBoudaries[currentCamBoidaries].RightBoundary + procam2DTriggerBoudaries[currentCamBoidaries].transform.position.x, ref velocity, speed);
        if (!setBoudariesLeft)
            return;

        _cameraSize.y = Camera.main.orthographicSize;
        _cameraSize.x = Mathf.Max(1, ((float)Screen.width / (float)Screen.height)) * _cameraSize.y;
        var leftBoundary = transform.position.x - Size().x;
        NumericBoundaries.LeftBoundary = leftBoundary;
    }

    public Vector2 Size()
    {
        return _cameraSize;
    }
}
