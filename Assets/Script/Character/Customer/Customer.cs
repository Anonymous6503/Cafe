using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class Customer : BaseCharacter
{

    public CounterSpot assignedSpot { get; private set; }
    public Transform homePoint { get; set; }


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        stateMachine.Initialize(new CustomerMovingState(stateMachine));
    }

    public void MoveTo(CounterSpot spot)
    {
        assignedSpot = spot;
    }

    public void Setup(Transform spawnPoint, CounterSpot destinationSpot)
    {
        this.homePoint = spawnPoint;
        this.assignedSpot = destinationSpot;
    }

    // This method is called by the Cashier. It tells our state machine to proceed.
    public void PlaceOrder()
    {
        if (stateMachine.CurrentState is CustomerWaitingToGiveOrderState)
        {
            stateMachine.TransitionTo(new CustomerWaitingToReceiveOrderState(stateMachine));
        }
    }

    // This method is called by the Cashier to finalize the transaction.
    public void CompleteOrder()
    {
        // This line tells the customer: "Here is your coffee."
        if (stateMachine.CurrentState is CustomerWaitingToReceiveOrderState)
        {
            stateMachine.TransitionTo(new CustomerReceivingOrderState(stateMachine));
        }
    }
}