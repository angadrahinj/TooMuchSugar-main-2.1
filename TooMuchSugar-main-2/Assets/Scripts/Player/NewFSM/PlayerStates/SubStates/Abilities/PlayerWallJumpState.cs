using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.inAirState.StopWallJumpCoyoteTime();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        AudioManager.instance.PlaySFX("Jump");
        player.CheckIfShouldFlip(wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.anim.SetFloat("yVelocity", player.currentVelocity.y);

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -player.facingDirection;
        }
        else 
        {
            wallJumpDirection = player.facingDirection;
        }
    }
}
