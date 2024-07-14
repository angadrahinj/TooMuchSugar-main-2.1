using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    private string animBoolName;
    protected string audioSourceName;

    public bool hasLearntAbility;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        this.hasLearntAbility = hasLearntAbility;

        this.audioSourceName = audioSourceName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(animBoolName, true);

        if (audioSourceName != null)
        {
            AudioManager.instance.PlaySFX(audioSourceName);
        }

        startTime = Time.time;
        isExitingState = false;
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        // Debug.Log(animBoolName);
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger() {}
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
