using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class PlayerLandState : PlayerGroundedState
{
    public float lastVelocityInAir;
    private float thresholdVelocity = -35f;

    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        // Debug.Log(lastVelocityInAir);
        if (lastVelocityInAir < thresholdVelocity)
        {
            stateMachine.ChangeState(player.hardLandState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        lastVelocityInAir = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else
            {
                //else if to take care of finishing animations (bardent part 22 33:50)
                stateMachine.ChangeState(player.idleState);
            }
            
        }
    }

    public void SetLastInAirVelocity(float velocity) => lastVelocityInAir = velocity;

}
