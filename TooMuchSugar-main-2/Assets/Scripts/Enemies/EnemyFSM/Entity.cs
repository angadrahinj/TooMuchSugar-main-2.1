using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable, IKnockbackable
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb {get; private set;}
    public Animator anim {get; private set;}
    public GameObject aliveGO;

    public int facingDirection;
    public float health;
    public bool canSetVelocity = true;
    public bool isKnockbackable;
    private bool isKnockbackActive = false;
    private float lastKnockbackTime = -10;
    public float knockBackDuration = 0.25f;

    private Vector2 velocityWorkspace;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    public virtual void Awake()
    {
        stateMachine = new FiniteStateMachine();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = entityData.entityMaxHealth;
    }

    public virtual void Start()
    {
        isKnockbackable = entityData.isKnockbackable;
        facingDirection = 1;
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if(isKnockbackable)
        {
            CheckKnockback();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        if (canSetVelocity)
        {
            velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
            rb.velocity = velocityWorkspace;
        }
    }   

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right * new Vector2(-1, 0), entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAggroDistance));
    }

    public virtual void Damage(float damage)
    {
        health -= damage;
        // Spawn hit particle?
        // Knockback 

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Knockback(Vector2 angle, float strength, int direction)
    {
        if (isKnockbackable && !isKnockbackActive)
        {
            isKnockbackActive = true;
            canSetVelocity = false;

            velocityWorkspace.Set(angle.x * strength * direction, rb.velocity.y);
            rb.velocity = velocityWorkspace;

            lastKnockbackTime = Time.time;

        }
    }

    public virtual void CheckKnockback()
    {
        if (isKnockbackActive && Time.time >= lastKnockbackTime + knockBackDuration)
        {
            isKnockbackActive = false;
            canSetVelocity = true;
        }
    }

    public virtual void Die()
    {
        // Change state to dead
        Debug.Log("Died");
        Destroy(gameObject);
    }
}
