using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackParticleController : MonoBehaviour
{
    public bool disablePanelAnim;
    public PanelAnimReward panelAnimReward;
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public void OnParticleSystemStopped()
    {
        if (panelAnimReward.objAfterEnd != null && !disablePanelAnim)
            panelAnimReward.objAfterEnd.SetActive(true);

        if (disablePanelAnim)
            panelAnimReward.gameObject.SetActive(false);
    }
}
