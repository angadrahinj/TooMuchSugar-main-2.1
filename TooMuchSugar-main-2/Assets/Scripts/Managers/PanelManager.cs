using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    //Event to handle start and end of each comic cutscene

    //Event to handle going from one panel page to the next

    public PanelPageMaster currentPanelCutsceneMaster;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
