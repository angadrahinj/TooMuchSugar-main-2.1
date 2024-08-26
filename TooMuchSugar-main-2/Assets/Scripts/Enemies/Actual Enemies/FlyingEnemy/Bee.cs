using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class Bee : Entity
{
    Seeker seeker;
    public Transform target;
    public Path path;
    public int currentWayPoint = 0;
    public bool reachedEndOfPath = false;

    public FlyingIdleState flyingIdleState;
    public E3_PlayerDetectedState playerDetectedState;
    public E3_ChaseState chaseState;
    public E3_ShootProjectileState shootProjectileState;

    public bool isPlayerInRange;
    public bool isPlayerInShootRange;

    public float detectionRange = 6f;
    public float shootRange = 3f;
    

    [SerializeField]
    private D_FlyingIdleState flyingIdleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;

    [SerializeField]
    public GameObject projectile;

    public override void Awake()
    {
        base.Awake();

        seeker = GetComponent<Seeker>();

        flyingIdleState = new FlyingIdleState(this, stateMachine, "idle", flyingIdleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDeteced", playerDetectedStateData, this);
        chaseState = new E3_ChaseState(this, stateMachine, "attack", this);
        shootProjectileState = new E3_ShootProjectileState(this, stateMachine, "shoot", this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(flyingIdleState);


        facingDirection = -1;
        // UpdatePath();
        // InvokeRepeating("DetectPlayer", 1f, 1f);
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(stateMachine.currentState);
    }

    public void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(this.transform.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    public void DetectPlayer()
    {
        isPlayerInShootRange = Physics2D.OverlapCircle(this.transform.position, shootRange, entityData.whatIsPlayer);

        if (isPlayerInShootRange)
        {
            isPlayerInRange = true;
            return;
        }
            
        isPlayerInRange = Physics2D.OverlapCircle(this.transform.position, detectionRange, entityData.whatIsPlayer);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void ShootProjectile()
    {
        GameObject shotProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
        shotProjectile.GetComponent<Projectile>().Initialize(target.position - this.transform.position);
    }
}
