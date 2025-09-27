using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class CashierMovingToMachineState : State
{
    private Cashier cashier;
    private NavMeshAgent navMeshAgent;
    private BaseMachine targetMachine;

    public CashierMovingToMachineState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        cashier = stateMachine.GetComponent<Cashier>();
        cashier.animator.SetBool("isMoving", true);
        cashier.ResumeAgent();
        cashier.UpdateMovementAnimation(true);

        navMeshAgent = cashier.GetComponent<NavMeshAgent>();
        MachineManager machineManager = GameObject.FindAnyObjectByType<MachineManager>();

        targetMachine = machineManager.FindAvailableMachine(cashier.currentOrder.menuItem.machineTypeRequired);

        if (targetMachine != null)
        {
            // A machine is available, so we move to it.
            navMeshAgent.SetDestination(targetMachine.operationPoint.position);
        }
        else
        {
            // No machines are available, so we transition to the new waiting state.
            // The cashier is already at the counter, so they will just stand still.
            stateMachine.TransitionTo(new CashierWaitingForMachineState(stateMachine));
        }
    }

    public override void Execute()
    {
        if (targetMachine != null && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            stateMachine.TransitionTo(new CashierOperatingMachineState(stateMachine, targetMachine));
        }
    }
}