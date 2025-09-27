using UnityEngine;

public class CashierTakingOrderState : State
{
    private Cashier cashier;
    private float orderTakenTimer;
    private float orderTime;

    public CashierTakingOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        cashier = stateMachine.GetComponent<Cashier>();
        cashier.StopAgent();
        cashier.UpdateMovementAnimation(false);
        cashier.LookAt(cashier.targetSpot.customerPoint.position);

        // Hide the progress bar initially
        cashier.UpdateProgressBar(0);

        // Set the timer for taking the order
        orderTime = Random.Range(1f, 3f);
        orderTakenTimer = Time.time + orderTime;
        Debug.Log("Cashier is now taking the order...");
    }

    public override void Execute()
    {
        // Update the progress bar fill amount over time
        float elapsedTime = orderTime - (orderTakenTimer - Time.time);
        cashier.UpdateProgressBar(elapsedTime / orderTime);

        if (Time.time >= orderTakenTimer)
        {
            // Hide the bar once done
            cashier.UpdateProgressBar(0);

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

    public override void Exit()
    {
        // Ensure the progress bar is hidden when leaving the state
        if (cashier != null)
        {
            cashier.UpdateProgressBar(0);
        }
    }
}