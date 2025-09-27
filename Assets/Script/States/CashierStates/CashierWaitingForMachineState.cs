using UnityEngine;

public class CashierWaitingForMachineState : State
{
    private MachineManager machineManager;
    private Cashier cashier;
    private float checkTimer; 

    public CashierWaitingForMachineState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        cashier = stateMachine.GetComponent<Cashier>();
        cashier.animator.SetBool("isMoving", false);
        machineManager = Object.FindAnyObjectByType<MachineManager>();
        Debug.Log($"{cashier.name} is now waiting for a free machine.");
    }

    public override void Execute()
    {
        if (Time.time > checkTimer)
        {
            BaseMachine availableMachine = machineManager.FindAvailableMachine(cashier.currentOrder.menuItem.machineTypeRequired);

            if (availableMachine != null)
            {
                Debug.Log("A machine has become available! Moving towards it.");
                stateMachine.TransitionTo(new CashierMovingToMachineState(stateMachine));
            }

            checkTimer = Time.time + 0.5f;
        }
    }
}