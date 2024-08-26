using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_PlayerDetectedState : PlayerDetectedState
{
    private Bee bee;
    public E3_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Bee bee) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.bee = bee;
    }

    public override void DoChecks()
    {
        bee.DetectPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performLongRangeAction)
        {
            if (bee.isPlayerInShootRange)
            {
                //Switch to charge state
                stateMachine.ChangeState(bee.chaseState);
            }
            else
            {
                //Switch to chase state
                stateMachine.ChangeState(bee.chaseState);
            }
        }
    }
}
