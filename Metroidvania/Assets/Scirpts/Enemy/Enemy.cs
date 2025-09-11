using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;//创建在父类，赋值在子类
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;
    public Enemy_StunnedState stunnedState;

    [Header("Battle details")]
    public float battleSpeed=3;
    public float attackDistance = 2;
    public float battleTimeDuration = 5;
    public float lastTimeInBattle;
    public float inGameTime;
    public float minRetreatDistance =1;
    public Vector2 retreatVelocity;


    [Header("Stunned state details")]
    public float stunnedDuration = 1f;
    public Vector2 stunnedVelocity = new Vector2(7, 7);
    [SerializeField]protected bool canBeStunned;

    [Header("Movement datails")]
    //[SerializeField]private float moveSpeed;
    public float idleTime=2;
    public float moveSpeed=1.5f;
    [Range(0,2)]
    public float moveAniSpeedMultiplier=1;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance=10;

    protected override void Update()
    {
        base.Update();

        inGameTime = Time.time;
    }

    public Transform playerTransform { get; private set; }
    
    //激活反击
    public void EnableCounterWindow(bool enable) => canBeStunned = enable;
    public Transform GetPlyaerTransform()

    {
        if (playerTransform == null)
            return PlayerHit().transform;

        return playerTransform;
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    private void HandlePlayerDead()
    {
        stateMachine.ChangeState(idleState);
    }

    public void EnterBattleState(Transform playerTransform)
    {
        if (stateMachine.currentState == battleState)
            return;

        if (stateMachine.currentState == attackState)
            return;

        this.playerTransform = playerTransform;

        stateMachine.ChangeState(battleState);
    }

    public RaycastHit2D PlayerHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer|whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (playerCheckDistance*facingDir), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (attackDistance * facingDir), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (minRetreatDistance * facingDir), playerCheck.position.y));

    }

    private void OnEnable()
    {
        Player.OnPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        Player.OnPlayerDead -= HandlePlayerDead;
    }
}

