using UnityEngine;

public class CashierTakingOrderState : State
{
    private float orderTakenTimer;
    private bool hasProcessedOrder = false;

    public CashierTakingOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        orderTakenTimer = Time.time + Random.Range(1f, 3f);
        hasProcessedOrder = false;

        Debug.Log("Cashier has arrived at the counter and is now taking the order...");
    }

    public override void Execute()
    {
        if (Time.time >= orderTakenTimer && !hasProcessedOrder)
        {
            hasProcessedOrder = true;

            var cashier = stateMachine.GetComponent<Cashier>();
            Order order = cashier.currentOrder;

            if (order != null && order.customer != null)
            {
                Debug.Log($"Order for {order.menuItem.itemName} has been taken.");
                order.customer.PlaceOrder();

                // Now that the order is officially taken, move to the machine.
                stateMachine.TransitionTo(new CashierMovingToMachineState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTo(new CashierIdleState(stateMachine));
            }
        }
    }
}