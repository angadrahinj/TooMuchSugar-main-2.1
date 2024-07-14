using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/ Player Data")]
public class PlayerData : ScriptableObject
{
    public enum MovementState {NORMAL, STICKY}

    [Header("Player Stats")]
    public int maxHealth = 5;
    public int fallDamage = 1;

    [Header("Learnt Abilities")]
    public bool learntAttack = false;
    public bool learntHeal = false;
    public bool learntRoll = false;

    [Header("Movement State")]
    public MovementState playerMovementState;
    public float stickyMultiplier = 0.75f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("Move State Variables")]
    public float normalMovementVelocity = 10f;
    public float movementVelocity = 10f;
    public float stickyMovementVelocity => movementVelocity * stickyMultiplier;

    [Header("Jump State")]
    public float normalJumpVelocity = 16f;
    public float jumpVelocity = 16f;
    public float stickyJumpVelocity => jumpVelocity * stickyMultiplier;

    [Header("In Air State")]
    public float coyoteTime = 0.15f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float normalWallSlideVelocity = 3.0f;
    public float wallSlideVelocity = 3.0f;
    public float stickyWallSlideVelocity => wallSlideVelocity * stickyMultiplier;

    [Header("Wall Jump State")]
    public float normalWallJumpVelocity = 20f;
    public float wallJumpVelocity = 20f;
    public float stickyWallJumpVelocity => wallJumpVelocity * stickyMultiplier;
    public float wallJumpTime = 0.4f;
    public float wallJumpCoyoteTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Roll State")]
    public float rollVelocity = 15f;
    public float rollDuration = 1f;
    public float rollCooldown = 1f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Combat")]
    public int attackDamage = 2;
    public float attackRadius = 2f;
    public float attackCooldownDuration = 0.75f;
    public float knockBackStrength = 0.5f;

    
}
