using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState {get; private set;}
    public E1_MoveState moveState {get; private set;}

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Awake()
    {
        base.Awake();

        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);
        facingDirection = -1;
    }

    public override void Update()
    {
        base.Update();

    }
}
