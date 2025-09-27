using UnityEngine;

public class CustomerWaitingToReceiveOrderState : State
{
    public CustomerWaitingToReceiveOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Order placed! Now waiting for my coffee.");
    }

    public override void Execute()
    {
    }
}