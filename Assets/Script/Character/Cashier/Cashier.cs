using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class Cashier : MonoBehaviour, ICharacter
{
    public StateMachine stateMachine { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public CounterSpot targetSpot { get; private set; }
    public Order currentOrder { get; private set; }

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }
}