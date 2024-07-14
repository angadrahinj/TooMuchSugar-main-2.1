using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState;
    public E2_MoveState moveState;
    public E2_PlayerDetectedState playerDetectedState;
    public E2_ChargeState chargeState;


    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;

    public override void Awake()
    {
        base.Awake();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        chargeState = new E2_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);
    }

    public override void Update()
    {
        base.Update();

        // Debug.Log(stateMachine.currentState);
    }
}
