using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ChargeState : ChargeState
{
    private Enemy2 enemy;
    public E2_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isChargeTimeOver)
        {
            if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                //change to look for player states
                enemy.idleState.SetFlipAfterIdle(false);
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        else if(isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
