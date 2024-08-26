using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_ShootProjectileState : State
{
    private Bee bee;
    public E3_ShootProjectileState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Bee bee) : base(entity, stateMachine, animBoolName)
    {
        this.bee = bee;
    }

    public override void Enter()
    {
        base.Enter();

        bee.path = null;
        bee.StopMoving();
        bee.ShootProjectile();
    }

    override public void PhysicsUpdate()
    {
        
    }

    public void ShootProjectile()
    {
        
    }
}
