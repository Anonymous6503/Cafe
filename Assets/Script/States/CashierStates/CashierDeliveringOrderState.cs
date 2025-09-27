using UnityEngine.AI;
using UnityEngine;
public class CashierDeliveringOrderState : State
{
    private NavMeshAgent navMeshAgent;
    private Cashier cashier;

    public CashierDeliveringOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        cashier = stateMachine.GetComponent<Cashier>();
        navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(cashier.targetSpot.servePoint.position);
    }

    public override void Execute()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            Debug.Log("Order delivered!");
            cashier.targetSpot.customerInSpot.CompleteOrder();

            cashier.targetSpot.FreeSpot();
            stateMachine.TransitionTo(new CashierIdleState(stateMachine));
        }
    }
}