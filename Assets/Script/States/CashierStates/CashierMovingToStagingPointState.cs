using UnityEngine;
using UnityEngine.AI;

public class CashierMovingToStagingPointState : State
{
    private NavMeshAgent navMeshAgent;

    public CashierMovingToStagingPointState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        GameObject stagingPoint = GameObject.FindWithTag("StagingPoint");
        if (stagingPoint != null)
        {
            navMeshAgent.SetDestination(stagingPoint.transform.position);
        }
    }

    public override void Execute()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            stateMachine.TransitionTo(new CashierWaitingAtStagingPointState(stateMachine));
        }
    }
}