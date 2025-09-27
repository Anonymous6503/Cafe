using UnityEngine;
using UnityEngine.AI;

public class CustomerMovingState : State
{
    private Customer customer;
    private NavMeshAgent navMeshAgent;

    public CustomerMovingState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        customer = stateMachine.GetComponent<Customer>();
        navMeshAgent = customer.navMeshAgent;

        if (customer.assignedSpot != null)
        {
            navMeshAgent.SetDestination(customer.assignedSpot.customerPoint.position);
        }
    }
    public override void Execute()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            stateMachine.TransitionTo(new CustomerWaitingToGiveOrderState(stateMachine));
        }
    }
}