using UnityEngine;
using UnityEngine.AI;

public class CustomerExitingState : State
{
    private NavMeshAgent navMeshAgent;

    public CustomerExitingState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if (stateMachine == null) return;

        var customer = stateMachine.GetComponent<Customer>();
        navMeshAgent = customer.navMeshAgent;

        if (customer.homePoint != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(customer.homePoint.position);
        }
        else
        {
            Debug.LogWarning("Customer has no home point! Destroying immediately.");
            Object.Destroy(stateMachine.gameObject);
        }
    }

    public override void Execute()
    {
        if (stateMachine == null || navMeshAgent == null) return; 

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Object.Destroy(stateMachine.gameObject);
        }
    }
}