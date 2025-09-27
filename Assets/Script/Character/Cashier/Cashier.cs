using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class Cashier : BaseCharacter
{
    public CounterSpot targetSpot { get; private set; }
    public Order currentOrder { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        stateMachine.Initialize(new CashierIdleState(stateMachine));
    }

    public void GoToServeCustomer(CounterSpot spot, Order order)
    {
        targetSpot = spot;
        currentOrder = order; // Store the order
        stateMachine.TransitionTo(new CashierMovingToServePointState(stateMachine));
    }
   
}