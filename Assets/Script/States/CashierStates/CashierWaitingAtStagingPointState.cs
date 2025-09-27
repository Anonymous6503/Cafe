using UnityEngine;

public class CashierWaitingAtStagingPointState : State
{
    private float waitTimer;
    public CashierWaitingAtStagingPointState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        waitTimer = Time.time + 3f;
    }

    public override void Execute()
    {
        if (Time.time >= waitTimer)
        {
            stateMachine.TransitionTo(new CashierDeliveringOrderState(stateMachine));
        }
    }
}