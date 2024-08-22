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
        Debug.Log("Displaying next panel");
        if (count >= panels.Count)
        {
            // Refer to event in panel manager inst
            PanelManager.instance.DisplayNextPage();
            return;
        }

        panels[count].SetActive(true);
        count++;
        Debug.Log(count);
    }

    public void ResetPanelPageCounter() {
        count = 0;
    }

    public void HideAllPanels()
    {
        foreach(GameObject g in panels)
        {
            g.SetActive(false);
        }

        ResetPanelPageCounter();
    }
}
