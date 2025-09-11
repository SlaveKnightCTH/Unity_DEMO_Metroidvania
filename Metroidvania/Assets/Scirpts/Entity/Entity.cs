using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action OnFlipped;
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    protected StateMachine stateMachine;

    public int facingDir { get; private set; } = 1;
    private bool facingDirection = true;

    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();

    }



    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDection();
        stateMachine.UpdateActiveState();
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void EntityDeath()
    {

    }

    private bool isKnockBacked;
    private Coroutine knockBackCo;

    public void ReciveKnockBack(Vector2 velocity,float duration)
    {
        if (knockBackCo != null)
            StopCoroutine(knockBackCo);

        knockBackCo = StartCoroutine(KnockBackCo(velocity,duration));
    }

    private IEnumerator KnockBackCo(Vector2 velocity, float duration)
    {
        isKnockBacked = true;
        rb.velocity = velocity;

        yield return new WaitForSeconds(duration);

        isKnockBacked = false;
        rb.velocity = Vector2.zero;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnockBacked)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (facingDirection == true && xVelocity < 0)
        {
            Flip();
        }
        else if (facingDirection == false && xVelocity > 0)
        {
            Flip();
        }

    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingDirection = !facingDirection;
        facingDir = facingDir * -1;

        OnFlipped?.Invoke();
    }

    private void HandleCollisionDection()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
                         && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
        }
        else
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance, 0));
        Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
        if (secondaryWallCheck != null)
        Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0, 0));
    }

}
