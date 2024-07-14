using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public string cutsceneName;
    public void PlayCutscene()
    {
        CutsceneManager.instance.PlayCutscene(cutsceneName);
    }
}
