using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPageMaster : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panelPages = new List<GameObject>();
    public int count = 0;

    //Reset count to 0 everytime a comic cutscene ends.

    [ContextMenu("Display next page")]
    public void DisplayNextPage()
    {
        if (count != 0)
        {
            HidePreviousPage();
        }
        else if (count >= panelPages.Count)
        {
            //Refer to panel manager cutscene finish
        }
        panelPages[count].SetActive(true);
        count++;
    }

    [ContextMenu("Hide previous page")]
    public void HidePreviousPage()
    {
        panelPages[count - 1].SetActive(false);
    }

    [ContextMenu("Reset counter")]
    public void ResetPanelPageCounter() {
        count = 0;
    }
}
