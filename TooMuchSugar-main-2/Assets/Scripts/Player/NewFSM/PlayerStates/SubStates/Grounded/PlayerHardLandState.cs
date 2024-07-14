using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerHardLandState : PlayerGroundedState
{
    public PlayerHardLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();


        Debug.Log("Hard land");

        player.SetVelocity(0,Vector2.zero, 1);
        player.healthManager.Damage(playerData.fallDamage);

        stateMachine.ChangeState(player.idleState);
    }

    public override void LogicUpdate()
    {
        // if (isAnimationFinished)
        // {
        //     stateMachine.ChangeState(player.idleState);
        // }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
