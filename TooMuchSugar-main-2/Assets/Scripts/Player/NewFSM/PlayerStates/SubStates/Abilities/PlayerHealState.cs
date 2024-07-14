using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerAbilityState
{
    protected float stunModeDuration = 1f;
    private float timeSlowDown = 0.3f;
    private float normalDeltaTime;

    public PlayerHealState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        startTime = Time.unscaledTime;
        Time.timeScale = timeSlowDown;
        normalDeltaTime = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        // player.SetVelocity(0, Vector2.zero ,0);

        
        // player.SetPlayerGravity(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.unscaledTime >= startTime + stunModeDuration)
        {
            Time.timeScale = 1f;
            isAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        
        // player.SetPlayerGravity(5f);
        Time.fixedDeltaTime = normalDeltaTime;
    }

    public override void LearnAbility()
    {
        base.LearnAbility();

        playerData.learntHeal = true;
    }

}
