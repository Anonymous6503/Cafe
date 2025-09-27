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
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Debug.Log("Order delivered!");

            Order order = cashier.currentOrder;

            CafeManager.Instance.playerWallet.AddMoney(order.menuItem.price);
            order.customer.CompleteOrder();

            if (cashier.coffeeSpawnPoint.transform.childCount > 0)
            {
                GameObject.Destroy(cashier.coffeeSpawnPoint.GetChild(0).gameObject);
            }

            cashier.targetSpot.FreeSpot();
            stateMachine.TransitionTo(new CashierIdleState(stateMachine));
        }
    }
}