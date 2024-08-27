using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    [SerializeField]
    private bool playOnSpawn = false;
    [SerializeField] PanelPageMaster panelPageMaster;

    private void Start()
    {
        if (playOnSpawn)
        {
            StartCutscene();
        }
    }

    public void StartCutscene()
    {
        PanelManager.instance.StartComicCutscene(panelPageMaster);
    }
}   
