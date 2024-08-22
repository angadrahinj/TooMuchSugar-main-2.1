using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelManager : MonoBehaviour
{
    #region Variables
    public static PanelManager instance;

    [SerializeField] PlayerInput playerInput;

    //Event to handle start and end of each comic cutscene
    public event Action OnComicCutsceneStart;
    public event Action OnComicCutsceneEnd;
    //Event to handle going from one panel page to the next
    public event Action OnComicCutsceneUpdate;

    public PanelPageMaster currentPanelCutsceneMaster;
    #endregion

    #region Unity Functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    #endregion

    public void DisplayNextPage()
    {
        currentPanelCutsceneMaster.DisplayNextPage();
    }

    
    #region Start and End of Cuscene Functions
    public void StartComicCutscene(PanelPageMaster panelPageMaster)
    {
        panelPageMaster.gameObject.SetActive(true);
        currentPanelCutsceneMaster = panelPageMaster;

        DisplayNextPage();

        SwitchToCutsceneInput();
    }
    public void FinishComicCutscene()
    {
        currentPanelCutsceneMaster = null;

        //Handle sounds + game input
        SwitchToGameplayInput();
    }
    #endregion

    #region Listening To User Input
    public void DisplayNextImage(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentPanelCutsceneMaster.DisplayNextImage();
        }
    }

    public void SkipPageInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DisplayNextPage();
        }
    }

    public void GoBackPageInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            currentPanelCutsceneMaster.DisplayPreviousPage();
        }
    }
    #endregion

    #region Switching User Input Maps
    void SwitchToCutsceneInput()
    {
        Debug.Log("Switched input");
        playerInput.SwitchCurrentActionMap("Cutscene");
    }

    void SwitchToGameplayInput()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
    }
    #endregion
}
