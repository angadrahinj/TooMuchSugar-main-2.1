using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool jumpInput;
    private bool abilityInput;
    private bool rollInput;
    private bool isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        rollInput = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpInput = player.inputHandler.jumpInput;
        xInput = player.inputHandler.NormInputX;
        abilityInput = player.inputHandler.abilityInput;
        rollInput = player.inputHandler.rollInput;

        if (jumpInput == true)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        // Ability input => toothbrush
        // Needs to be updated InAir as well
        else if(rollInput)
        {
            if (player.rollState.CheckIfCanRoll())
            {
                stateMachine.ChangeState(player.rollState);
            }
            else 
            {
                player.inputHandler.UseRollInput();
            }
            
        }
        else if (abilityInput)
        {
            player.inputHandler.UseAbilityInput();

            if (player.toothBrushState == Player.ToothBrushState.HEAL)
            {
                stateMachine.ChangeState(player.healState);
            }
            else if(player.combatState.CheckIfCanAttack())
            {
                stateMachine.ChangeState(player.combatState);
            }
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
