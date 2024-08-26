using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIdleState : State
{
    private Bee bee;
    public D_FlyingIdleState stateData;
    private Vector3 nextWayPoint;

    private Vector2 moveDir;

    private float lastMoveTime;
    private float idleTime;

    public FlyingIdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FlyingIdleState stateData, Bee bee) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        this.bee = bee;
    }

    public override void Enter()
    {
        base.Enter();
        bee.path = null;
        FindNextWayPoint();
        IdleTimer();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        bee.DetectPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (bee.isPlayerInRange)
        {
            //Switch to player detected state
            stateMachine.ChangeState(bee.playerDetectedState);
            // Debug.Log("Hello");
        }
        else if (bee.transform.position != nextWayPoint)
        {
            bee.transform.position = Vector2.MoveTowards(bee.transform.position, nextWayPoint, stateData.moveSpeed * Time.deltaTime);
            // bee.transform.position += Vector3.Lerp(bee.transform.position, nextWayPoint, stateData.moveSpeed);
            lastMoveTime = Time.time;

            // Debug.Log("Moving");
            return;
        }
        else
        {
            if (Time.time >= lastMoveTime + idleTime)
            {
                FindNextWayPoint();
                IdleTimer();
            }
        }
    }

    public void IdleTimer()
    {
        // Debug.Log("Finding idle time");
        idleTime = Random.Range(1f, 1.5f);
    }

    public void FindNextWayPoint()
    {
        // Debug.Log("Finding waypoint");
        nextWayPoint = bee.transform.position +  (Vector3)Random.insideUnitCircle * stateData.rangeOfMotion;
        nextWayPoint.z = 0f;
        moveDir = nextWayPoint - bee.transform.position;
    }

}
