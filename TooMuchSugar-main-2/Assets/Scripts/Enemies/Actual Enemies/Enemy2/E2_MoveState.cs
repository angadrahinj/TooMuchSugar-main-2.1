using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;
    public E2_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true); 
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
