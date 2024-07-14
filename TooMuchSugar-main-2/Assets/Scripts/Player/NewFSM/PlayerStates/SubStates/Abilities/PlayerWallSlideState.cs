using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    private float wallSlideVelocity;
    private bool isStickyWall = false;
    private float stickyWallSlideSpeed = 0.1f;

    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        // if (isStickyWall)
        // {
        //     SetWallSlideSpeed(stickyWallSlideSpeed);
        // }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVerticalVelocity(-this.wallSlideVelocity);

            if (jumpInput)
            {           
                player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
                stateMachine.ChangeState(player.wallJumpState);
            }
            else if((!isTouchingWall || xInput == -player.facingDirection) && playerData.playerMovementState != PlayerData.MovementState.STICKY)
            {
                stateMachine.ChangeState(player.inAirState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        this.wallSlideVelocity = playerData.wallSlideVelocity;
        player.inAirState.StartWallJumpCoyoteTime();
        isStickyWall = false;
    }

    // public void SetWallSlideSpeed(float wallSlideVelocity) => this.wallSlideVelocity = wallSlideVelocity;

    // public void IsStickyWall() => isStickyWall = true;

}
