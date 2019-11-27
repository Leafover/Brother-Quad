using UnityEngine;
using System.Collections;

public class LineBlood : MonoBehaviour
{
    const float TIME_HIDE = 3;
    public SpriteRenderer[] lineSprite;
    private float countdown;
    private bool isAutoHide;
    private SpriteRenderer lineFx;
    private Vector3 lineFxScale;
    private Vector3 vtRate;
    public void Show(float hpCurrent, float HP, bool isAutoHide = true)
    {
        this.isAutoHide = isAutoHide;
        for (int i = 0; i < lineSprite.Length; i++)
        {
            lineSprite[i].color = Color.white;
        }
        if (lineFx != null)
        {
            lineFx.color = Color.white;
            lineFx.transform.localScale = lineFxScale = lineSprite[1].transform.localScale;
        }
        vtRate = new Vector3(Mathf.Clamp01(hpCurrent / HP), 1, 1);
        lineSprite[1].transform.localScale = vtRate;
        countdown = TIME_HIDE;
    }

    void LateUpdate()
    {
        if (countdown <= 0 || !isAutoHide || lineFx == null)
            return;
        var deltaTime = Time.deltaTime;
        countdown -= deltaTime;
        countdown = Mathf.Max(0, countdown);
        for (int i = 0; i < lineSprite.Length; i++)
        {
            lineSprite[i].color = new Color(1, 1, 1, countdown / 3);
        }
        lineFx.color = new Color(1, 1, 1, countdown / 6);
        lineFxScale.x = Mathf.MoveTowards(lineFxScale.x, vtRate.x, deltaTime / TIME_HIDE);
        lineFx.transform.localScale = lineFxScale;

      //  Debug.LogError("chay vao day lam gi?");
    }

    public void Reset()
    {
        if (!lineFx)
        {
			if (lineSprite[0].transform.childCount == 1)
			{
				lineFx = Instantiate(lineSprite[1], lineSprite[1].transform.position, lineSprite[1].transform.rotation, lineSprite[1].transform.parent);
			}
			else
			{
				lineFx = lineSprite[0].transform.GetChild(1).GetComponent<SpriteRenderer>();
			}

        }
        lineFx.transform.localScale = lineSprite[1].transform.localScale = Vector3.one;
        lineFx.color = lineSprite[0].color = lineSprite[1].color = new Color(1, 1, 1, 0);
        countdown = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
