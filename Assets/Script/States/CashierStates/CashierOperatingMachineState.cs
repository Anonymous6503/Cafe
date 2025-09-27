using UnityEngine;

public class CashierOperatingMachineState : State
{
    private BaseMachine machine;

    public CashierOperatingMachineState(StateMachine stateMachine, BaseMachine machine) : base(stateMachine)
    {
        this.machine = machine;
    }

    public override void Enter()
    {
        machine.OnCraftingComplete += HandleCraftingComplete;

        var cashier = stateMachine.GetComponent<Cashier>();
        cashier.animator.SetBool("isMoving", false);
        machine.StartCrafting(cashier.currentOrder);
    }

    private void HandleCraftingComplete(Order order)
    {
        Debug.Log("Cashier sees that the machine is finished.");
        stateMachine.TransitionTo(new CashierDeliveringOrderState(stateMachine));
    }

    public override void Exit()
    {
        machine.OnCraftingComplete -= HandleCraftingComplete;
    }
}