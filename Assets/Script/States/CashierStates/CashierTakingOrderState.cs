using UnityEngine;

public class CashierTakingOrderState : State
{
    private float orderTakenTimer;
    private bool hasProcessedOrder = false;
    private Cashier cashier;

    public CashierTakingOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        hasProcessedOrder = false;
        cashier = stateMachine.GetComponent<Cashier>();

        cashier.StopAgent();
        cashier.UpdateMovementAnimation(false);
        cashier.LookAt(cashier.targetSpot.customerPoint.position);

        orderTakenTimer = Time.time + Random.Range(1f, 3f);
        Debug.Log("Cashier has arrived and is now taking the order...");
    }

    public override void Execute()
    {
        if (Time.time >= orderTakenTimer && !hasProcessedOrder)
        {
            hasProcessedOrder = true;
            Order order = cashier.currentOrder;

            if (order != null && order.customer != null)
            {
                Debug.Log($"Order for {order.menuItem.itemName} has been taken.");
                order.customer.PlaceOrder();

                stateMachine.TransitionTo(new CashierMovingToMachineState(stateMachine));
            }
            else
            {
                stateMachine.TransitionTo(new CashierIdleState(stateMachine));
            }
        }
    }
}