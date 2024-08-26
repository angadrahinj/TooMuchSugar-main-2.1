using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class E3_ChaseState : State
{
    private Bee bee;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;

    public float lastPathUpdate = 0f;

    public E3_ChaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Bee bee) : base(entity, stateMachine, animBoolName)
    {
        this.bee = bee;
    }

    public override void Enter()
    {
        base.Enter();

        bee.UpdatePath();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        float distanceToPlayer = Vector2.Distance(bee.transform.position, bee.target.position);

        if (Mathf.RoundToInt((bee.target.transform.position - bee.transform.position).x) >= 0 && bee.facingDirection != 1)
        {
            bee.Flip();
        }
        else if(Mathf.RoundToInt((bee.target.transform.position - bee.transform.position).x) <= 0 && bee.facingDirection != -1)
        {
            bee.Flip();
        }
        if (distanceToPlayer >= 10f)
        {
            stateMachine.ChangeState(bee.flyingIdleState);
        }

        if (Time.time >= lastPathUpdate + 0.25f)
        {
            bee.UpdatePath();
            lastPathUpdate = Time.time;
        }

        if (bee.path == null)
        {
            return;
        }

        if (bee.currentWayPoint >= bee.path.vectorPath.Count)
        {
            bee.reachedEndOfPath = true;
            return;
        }
        else
        {
            bee.reachedEndOfPath = false;
        }

        Vector2 direction = (Vector2)(bee.path.vectorPath[bee.currentWayPoint] - bee.transform.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        bee.rb.AddForce(force);

        float distance = Vector2.Distance(bee.transform.position, bee.path.vectorPath[bee.currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            bee.currentWayPoint ++;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        bee.DetectPlayer();

        if (bee.isPlayerInShootRange)
        {
            stateMachine.ChangeState(bee.shootProjectileState);
        }
    }
}
