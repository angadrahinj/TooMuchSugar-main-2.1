using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    public Player player;


    public Vector2 rawMovementInput {get; private set;}
    public float xInputThreshold = 0.125f;

    public int NormInputX {get; private set;}
    public int NormInputY {get; private set;}

    [SerializeField]
    private float inputHoldTime = 0.2f;
    public bool jumpInput {get; private set;}
    private float jumpInputStartTime;
    public bool jumpInputStopped;

    public bool rollInput {get; private set;}
    public float rollInputStartTime {get; private set;}

    public bool abilityInput {get; private set;}
    public float abilityInputStartTime {get; private set;}

    [SerializeField]
    public float swapAbilityCoolDown = 2f;
    private float lastSwapTime = -100f;


    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        CheckJumpInputHoldTime();
        CheckRollInputPressTime();
        CheckAbilityInputPressTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();

        if (rawMovementInput.x >= xInputThreshold || rawMovementInput.x <= -xInputThreshold)
        {
            NormInputX = (int)(rawMovementInput *Vector2.right).normalized.x;
        }
        else {
            NormInputX = 0;  
        }

        NormInputY = (int)(rawMovementInput *Vector2.right).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpInputStopped = false;
            jumpInput = true;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            jumpInputStopped = true;
        }
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rollInput = true;
            rollInputStartTime = Time.time;
        }
    }

    public void OnAbilityInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            abilityInput = true;
            abilityInputStartTime = Time.time;
        }
    }

    public void OnSwapToothBrushModeInput(InputAction.CallbackContext context)
    {
        if (context.started && Time.time >= lastSwapTime + swapAbilityCoolDown)
        {
            lastSwapTime = Time.time;
            player.SetToothBrushState();
        }
    }

    public void UseJumpInput() => jumpInput = false;

    public void UseRollInput() => rollInput = false;

    public void UseAbilityInput() => abilityInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            jumpInput = false;
        }
    }

    private void CheckRollInputPressTime()
    {
        if (Time.time >= rollInputStartTime + inputHoldTime)
        {
            rollInput = false;
        }
    }

    private void CheckAbilityInputPressTime()
    {
        if (Time.time >= abilityInputStartTime + inputHoldTime)
        {
            abilityInput = false;
        }
    }
}
