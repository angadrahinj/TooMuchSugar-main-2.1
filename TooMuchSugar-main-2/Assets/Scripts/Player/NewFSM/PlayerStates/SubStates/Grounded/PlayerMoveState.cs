using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    private float idleTimer = 0f;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX("Walk");
        idleTimer = 0f;
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX("Walk");
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(xInput);

        player.SetHorizontalVelocity(playerData.movementVelocity * xInput);

        if (xInput == 0 && !isExitingState)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= 0.1f)
            {
                stateMachine.ChangeState(player.idleState); 
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
