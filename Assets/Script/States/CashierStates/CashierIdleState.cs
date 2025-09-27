using System; // Required for Action

public class CashierIdleState : State
{
    public static event Action<Cashier> OnCashierBecameIdle;

    public CashierIdleState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        var cashier = stateMachine.GetComponent<Cashier>();

        OnCashierBecameIdle?.Invoke(cashier);
    }
}