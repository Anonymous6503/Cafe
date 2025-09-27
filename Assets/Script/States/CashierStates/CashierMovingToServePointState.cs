using UnityEngine.AI;

public class CashierMovingToServePointState : State
{
    private Cashier cashier;
    private NavMeshAgent navMeshAgent;

    public CashierMovingToServePointState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        cashier = stateMachine.GetComponent<Cashier>();
        navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();

        cashier.MoveTo(cashier.targetSpot.servePoint.position);
    }

    public override void Execute()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            stateMachine.TransitionTo(new CashierTakingOrderState(stateMachine));
        }
    }
}