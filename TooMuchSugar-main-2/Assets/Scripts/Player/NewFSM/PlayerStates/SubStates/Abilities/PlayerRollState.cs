using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class PlayerRollState : PlayerAbilityState
{

    protected int facingDirection;
    protected bool jumpInput;
    private int xInput;
    private float rollStartTime;
    private float lastRollTime = -100;
    public bool canRoll;

    private Vector2 lastAIPos;

    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();

        CheckIfShouldPlaceAfterImage();
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseRollInput();
        facingDirection = player.facingDirection;
        xInput = player.inputHandler.NormInputX;

        rollStartTime = Time.time;

        PlaceAfterImage();

        Physics2D.IgnoreLayerCollision(6,7, true);
    }

    public override void Exit()
    {
        base.Exit();

        lastRollTime = Time.time;
        canRoll = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (!isGrounded)
            {
                // isAbilityDone = true;
                player.inAirState.StartCoyoteTime();
            }
            else if (jumpInput)
            {
                isAbilityDone = true;
                stateMachine.ChangeState(player.jumpState);
            }
            
            if(Time.time >= rollStartTime + playerData.rollDuration)
            {
                isAbilityDone = true;
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Roll();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public bool CheckIfCanRoll()
    {
        if (Time.time >= lastRollTime + playerData.rollCooldown)
        {
            return true;
        }
        return false;
    }

    public void Roll()
    {
        if (xInput != 0)
        {
            player.SetHorizontalVelocity(xInput * playerData.rollVelocity);
        }
        else
        {
            player.SetHorizontalVelocity(facingDirection * playerData.rollVelocity);
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(lastAIPos, player.transform.position) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }

    public override void LearnAbility()
    {
        base.LearnAbility();

        playerData.learntRoll = true;
    }
}
