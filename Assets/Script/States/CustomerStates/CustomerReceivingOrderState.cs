using UnityEngine;

public class CustomerReceivingOrderState : State
{
    public CustomerReceivingOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Customer has received the order. Thank you!");

        stateMachine.TransitionTo(new CustomerExitingState(stateMachine));
    }
}