using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool jumpInput;
    private bool abilityInput;
    private bool rollInput;
    private bool jumpInputStopped;
    private bool coyoteTime = false;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    private float currentInAirVelocity;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();

        if (!wallJumpCoyoteTime && !isTouchingWall && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();


        xInput = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.jumpInput;
        jumpInputStopped = player.inputHandler.jumpInputStopped;
        abilityInput = player.inputHandler.abilityInput;
        rollInput = player.inputHandler.rollInput;


        if (!isGrounded && (int)player.currentVelocity.y != 0)
        {
            currentInAirVelocity = (int)player.currentVelocity.y;
        }
        

        CheckJumpMultiplier();

        if (!isExitingState)
        {
            if (isGrounded && player.currentVelocity.y <= 0.01f)
            {
                player.landState.SetLastInAirVelocity(currentInAirVelocity);
                stateMachine.ChangeState(player.landState);
            }
            else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                isTouchingWall = player.CheckIfTouchingWall();
                player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
                stateMachine.ChangeState(player.wallJumpState);
            }
            else if(jumpInput && coyoteTime)
            {   
                coyoteTime = false;
                CoyoteTimeJump();
            }
            //no facing direction input
            else if (isTouchingWall && player.currentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
            else if (abilityInput)
            {
                player.inputHandler.UseAbilityInput();

                if (player.toothBrushState == Player.ToothBrushState.HEAL)
                {
                    stateMachine.ChangeState(player.healState);
                }
                else 
                {
                    stateMachine.ChangeState(player.combatState);
                }
            }
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
            else
            {
                player.anim.SetFloat("yVelocity", player.currentVelocity.y);
                player.CheckIfShouldFlip(xInput);
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetHorizontalVelocity(playerData.movementVelocity * xInput);
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if(jumpInputStopped)
            {
                player.SetVerticalVelocity(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public void CoyoteTimeJump() {
        stateMachine.ChangeState(player.jumpState);
    }


    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime() 
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    } 
        

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void IsJumping() => isJumping = true;
}
