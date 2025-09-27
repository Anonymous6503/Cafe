using UnityEngine;
using System.Collections.Generic;

public class CafeManager : MonoBehaviour
{
    public MachineManager machineManager;

    public CustomerManager customerManager;
    public CashierManager cashierManager;
    public MenuItemData startingItem; 
    private Menu menu;

    private Queue<Order> waitingOrderQueue = new Queue<Order>();

    void Awake()
    {
        menu = GetComponent<Menu>();
    }

    void Start()
    {
        // Unlock the starting item and build its machine
        if (startingItem != null)
        {
            UnlockNewProduct(startingItem);
        }
    }

    private void OnEnable()
    {
        CustomerWaitingToGiveOrderState.OnCustomerReadyForService += HandleCustomerReady;
        CashierIdleState.OnCashierBecameIdle += HandleCashierBecameIdle;
    }

    private void OnDisable()
    {
        CustomerWaitingToGiveOrderState.OnCustomerReadyForService -= HandleCustomerReady;
        CashierIdleState.OnCashierBecameIdle -= HandleCashierBecameIdle;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            machineManager.SpawnNewMachine(MachineType.Coffee);
        }
    }

    public void UnlockNewProduct(MenuItemData productToUnlock)
    {
        if (productToUnlock == null) return;

        menu.UnlockNewItem(productToUnlock);

        MachineType requiredType = productToUnlock.machineTypeRequired;
        if (!machineManager.DoesMachineTypeExist(requiredType))
        {
            Debug.Log($"No machine of type {requiredType} found. Building a new one.");
            machineManager.SpawnNewMachine(requiredType);
        }
    }

    private void HandleCustomerReady(Order order)
    {
        Debug.Log($"CAFE MANAGER: Received order for {order.menuItem.itemName} from {order.customer.name}. Adding to queue.");
        waitingOrderQueue.Enqueue(order);
        AssignCashierToNextOrderInQueue();
    }

    private void HandleCashierBecameIdle(Cashier cashier)
    {
        Debug.Log($"CAFE MANAGER: {cashier.name} is now idle. Checking for pending orders.");
        AssignCashierToNextOrderInQueue();
    }

    private void AssignCashierToNextOrderInQueue()
    {
        if (waitingOrderQueue.Count > 0)
        {
            Order orderToProcess = waitingOrderQueue.Peek();
            CounterSpot spotToServe = orderToProcess.customer.assignedSpot;

            Cashier idleCashier = cashierManager.GetNearestIdleCashier(spotToServe.servePoint.position);

            if (idleCashier != null)
            {
                orderToProcess = waitingOrderQueue.Dequeue();
                idleCashier.GoToServeCustomer(spotToServe, orderToProcess);
            }
        }
    }
}