using UnityEngine;

public abstract class EntityState 
{
    protected StateMachine stateMachine;
    protected string aniBoolName;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string aniBoolName)
    {
        this.stateMachine = stateMachine;
        this.aniBoolName = aniBoolName;
    }

    public virtual void Enter()
    {
        animator.SetBool(aniBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }

    public virtual void Exit()
    {
        animator.SetBool(aniBoolName, false);
    }
    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameters()
    {

    }
}
