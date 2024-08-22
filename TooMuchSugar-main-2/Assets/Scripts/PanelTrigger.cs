using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    [SerializeField] PanelPageMaster panelPageMaster;

    public void StartCutscene()
    {
        PanelManager.instance.StartComicCutscene(panelPageMaster);
    }
}   
