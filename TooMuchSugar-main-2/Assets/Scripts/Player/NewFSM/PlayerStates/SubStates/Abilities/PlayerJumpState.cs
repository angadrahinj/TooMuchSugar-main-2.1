using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName, bool hasLearntAbility, string audioSourceName) : base(player, stateMachine, playerData, animBoolName, hasLearntAbility, audioSourceName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        player.inputHandler.UseJumpInput();
        player.SetVerticalVelocity(playerData.jumpVelocity);
        AudioManager.instance.PlaySFX("Jump");
        isAbilityDone = true;
        player.inAirState.IsJumping();
    }
}
