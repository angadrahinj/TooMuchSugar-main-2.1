using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerAbilityState
{
    private int xInput;
    private float lastAttackTime;

    public PlayerCombatState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        // if(isGrounded){
        //     player.SetHorizontalVelocity(0f);
        // }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        // player.CheckIfShouldFlip(xInput);
    }

    public override void LogicUpdate()
    {
        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
        
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;

        // player.SetHorizontalVelocity(playerData.movementVelocity * xInput * 0.75f);
    }

    public void Attack()
    {
        lastAttackTime = Time.time;
    }

    public bool CheckIfCanAttack()
    {
        if (Time.time >= lastAttackTime + playerData.attackCooldownDuration)
        {
            return true;
        }
        return false;
    }

    public override void LearnAbility()
    {
        base.LearnAbility();

        playerData.learntAttack = true;
    }

}
