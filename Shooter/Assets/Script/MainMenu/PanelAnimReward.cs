using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelAnimReward : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject objAfterEnd;
    public Image iconImg;
    public void EventAnim()
    {
        particle.Play();
        Debug.LogError("play");
    }
    public void ActiveMe(GameObject g,Sprite _sp)
    {
        objAfterEnd = g;
        iconImg.sprite = _sp;
        gameObject.SetActive(true);
    }
}
