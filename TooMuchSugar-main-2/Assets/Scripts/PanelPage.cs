using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPage : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panels = new List<GameObject>();
    public int count = 0;

    [ContextMenu("Display next panel")]
    public void DisplayNextPanel()
    {
        panels[count].SetActive(true);
        count++;

        if (count >= panels.Count)
        {
            // Refer to event in panel manager inst
            // PanelManager.instance.OnPanelPageUpdate?.Invoke();
        }
    }
}
