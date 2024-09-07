using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    [SerializeField]
    private bool playOnSpawn = false;
    private TriggerZone triggerZone;
    [SerializeField] PanelPageMaster panelPageMaster;

    private void Start()
    {
        triggerZone = GetComponent<TriggerZone>();

        if (playOnSpawn)
        {
            StartCutscene();
            triggerZone.alreadyEntered = true;
        }
    }

    public void StartCutscene()
    {
        PanelManager.instance.StartComicCutscene(panelPageMaster);
    }
}   
