using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPageMaster : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> panelPages = new List<GameObject>();
    public int count = 0;

    [ContextMenu("Display Next Page")]
    public void DisplayNextPage()
    {
        if (count != 0)
        {
            HidePreviousPage();
        }
        if (count >= panelPages.Count)
        {
            //Refer to panel manager cutscene finish
            PanelManager.instance.FinishComicCutscene();
            // Debug.Log(count);
            return;
        }
        panelPages[count].SetActive(true);
        count++;
        // Debug.Log(count);
    }

    public void DisplayPreviousPage()
    {
        if (count > 1)
        {
            panelPages[count - 1].GetComponent<PanelPage>().HideAllPanels();
            panelPages[count - 1].SetActive(false);
            count--;

            panelPages[count - 1].SetActive(true);
        }
    }

    [ContextMenu("Display Next Image")]
    public void DisplayNextImage()
    {
        panelPages[count - 1].GetComponent<PanelPage>().DisplayNextPanel();
    }

    [ContextMenu("Hide previous page")]
    public void HidePreviousPage()
    {
        panelPages[count - 1].GetComponent<PanelPage>().HideAllPanels();
        panelPages[count - 1].SetActive(false);
    }

    [ContextMenu("Reset counter")]
    public void ResetPanelPagesCounter() {
        count = 0;
    }
}
