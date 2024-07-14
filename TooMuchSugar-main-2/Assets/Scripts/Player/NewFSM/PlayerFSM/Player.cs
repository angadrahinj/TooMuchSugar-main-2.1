using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Machine

    public PlayerStateMachine stateMachine {get; private set;}
    public PlayerIdleState idleState {get; private set;}
    public PlayerMoveState moveState {get; private set;}
    public PlayerJumpState jumpState {get; private set;}
    public PlayerInAirState inAirState {get; private set;}
    public PlayerLandState landState {get; private set;}
    public PlayerHardLandState hardLandState {get; private set;}
    public PlayerWallSlideState wallSlideState {get; private set;}
    public PlayerWallJumpState wallJumpState {get; private set;}
    public PlayerRollState rollState {get; private set;}
    public PlayerHealState healState {get; private set;}
    public PlayerCombatState combatState {get; private set;}


    [SerializeField]
    private PlayerData playerData;

    public enum ToothBrushState {HEAL, ATTACK}

    public ToothBrushState toothBrushState = ToothBrushState.HEAL;

    #endregion

    #region Components
    public PlayerHealthManager healthManager;
    public Animator anim {get; private set;}
    public PlayerInputHandler inputHandler {get; private set;}
    Rigidbody2D rb;
    #endregion

    #region Variables
    public Vector2 currentVelocity;
    public int facingDirection;
    private Vector2 movementVector;
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    
    public Transform attackPoint;
    
    #endregion

    #region Unity Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, Constants.IDLE, true, null);
        moveState = new PlayerMoveState(this, stateMachine, playerData, Constants.RUNNING, true, null);
        jumpState = new PlayerJumpState(this, stateMachine, playerData, Constants.IN_AIR, true, "Jump");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, Constants.IN_AIR, true, null);
        landState = new PlayerLandState(this, stateMachine, playerData, Constants.LAND, true, null);
        hardLandState = new PlayerHardLandState(this, stateMachine, playerData, Constants.HARD_LAND, true, "Fall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, Constants.WALL_SLIDE, true, null);
        wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, Constants.IN_AIR, true, "Jump");

        //Abilities to learn
        rollState = new PlayerRollState(this, stateMachine, playerData, Constants.ROLL, playerData.learntRoll, null);
        healState = new PlayerHealState(this, stateMachine, playerData, "ability", playerData.learntHeal, null);
        combatState = new PlayerCombatState(this, stateMachine, playerData, "attack", playerData.learntAttack, null);

        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        healthManager = GetComponent<PlayerHealthManager>();
    }

    // void OnEnable()
    // {
    //     stateMachine.Initialize(idleState);
    // }

    private void Start()
    {
        stateMachine.Initialize(idleState);

        facingDirection = 1;
        playerData.playerMovementState = PlayerData.MovementState.NORMAL;
        SetMovementVariables(playerData.normalMovementVelocity, playerData.normalJumpVelocity, playerData.normalWallJumpVelocity, playerData.normalWallSlideVelocity);
    }

    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
        // Debug.Log(stateMachine.currentState);
        currentVelocity = rb.velocity;

        CheckIfTouchingSticky();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion 

    #region Set Functions
    public void SetToothBrushState()
    {
        if (toothBrushState == ToothBrushState.HEAL)
        {
            toothBrushState = ToothBrushState.ATTACK;
        }
        else 
        {
            toothBrushState = ToothBrushState.HEAL;
        }
        Debug.Log(toothBrushState);
    }

    public void SetLearntAbility(string ability)
    {
        switch(ability)
        {
            case "Roll":
                rollState.LearnAbility();
                break;
            case "Attack":
                combatState.LearnAbility();
                break;
            case "Heal":
                healState.LearnAbility();
                break;    
        }
    }


    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        movementVector.Set(angle.x * velocity * direction, angle.y * velocity);
        
        rb.velocity = movementVector;
        currentVelocity = movementVector;
    }

    public void SetHorizontalVelocity(float velocity)
    {
        movementVector.Set(velocity, currentVelocity.y);
        rb.velocity = movementVector;
        currentVelocity = movementVector;
    }

    public void SetVerticalVelocity(float velocity)
    {
        movementVector.Set(currentVelocity.x, velocity);
        rb.velocity = movementVector;
        currentVelocity = movementVector;
    }

    public void SetMovementVariables(float movementVelocity, float jumpVelocity, float wallJumpVelocity, float wallSlideVelocity)
    {
        playerData.movementVelocity = movementVelocity;
        playerData.jumpVelocity = jumpVelocity;

        playerData.wallJumpVelocity = wallJumpVelocity;
        playerData.wallSlideVelocity = wallSlideVelocity;
    }

    public void SetPlayerGravity(float gravity)
    {
        rb.gravityScale = gravity;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void StopRollIFrames()
    {
        Physics2D.IgnoreLayerCollision(6,7, false);
    }

    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public void CheckIfTouchingSticky()
    {
        bool touchingStickyGround = CheckIfStickyGrounded();
        bool touchingStickyWall = CheckIfTouchingStickyWall();

        if ((touchingStickyGround || touchingStickyWall) && playerData.playerMovementState == PlayerData.MovementState.NORMAL)
        {
            Debug.Log("Touching sticky stuff");
            SetMovementVariables(playerData.stickyMovementVelocity, playerData.stickyJumpVelocity, playerData.stickyWallJumpVelocity, playerData.stickyWallSlideVelocity);
            playerData.playerMovementState = PlayerData.MovementState.STICKY;
        }
        else if ((!touchingStickyGround && !touchingStickyWall) && playerData.playerMovementState == PlayerData.MovementState.STICKY)
        {
            Debug.Log("Normal player");
            SetMovementVariables(playerData.normalMovementVelocity, playerData.normalJumpVelocity, playerData.normalWallJumpVelocity, playerData.normalWallSlideVelocity);
            playerData.playerMovementState = PlayerData.MovementState.NORMAL;
        }
    }

    public void CheckIfHitEnemies()
    {
        //Gets called from player combat state
        combatState.Attack();
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(attackPoint.position, playerData.attackRadius);

        foreach(Collider2D collider in hitInfo)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(playerData.attackDamage);
            }
            
            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();
            if (knockbackable != null)
            {
                knockbackable.Knockback(Vector2.one, playerData.knockBackStrength, facingDirection);
            }
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfStickyGrounded()
    {
        Collider2D colliderInfo = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);

        if (colliderInfo != null && colliderInfo.tag == "Sticky")
        {
            return true;
        }
        return false;
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingStickyWall()
    {
        RaycastHit2D hitInfo =  Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);

        if (hitInfo != false && hitInfo.collider.tag == "Sticky")
        {
            return true;
        }
        return false;
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    

    #endregion

    #region Animation Triggering

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position,playerData.groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right  *playerData.wallCheckDistance);
        Gizmos.DrawWireSphere(attackPoint.position, playerData.attackRadius);
    }

    #endregion
}
