using UnityEngine;
using System;
using Unity.VisualScripting;

public class CustomerWaitingToGiveOrderState : State
{
    // UPDATE: The event now sends the entire Order object.
    public static event Action<Order> OnCustomerReadyForService;

    private Menu menu;

    public CustomerWaitingToGiveOrderState(StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if (menu == null)
        {
            menu = GameObject.FindAnyObjectByType<Menu>();
        }

        Customer customer = stateMachine.GetComponent<Customer>();
        customer.animator.SetBool("isMoving", false);
        customer.StopAgent();
        customer.LookAt(customer.assignedSpot.servePoint.position);

        MenuItemData selectedMenuItem = menu.GetRandomMenuItem();

        if (selectedMenuItem != null)
        {
            Order newOrder = new Order(customer, selectedMenuItem);

            Debug.Log($"{customer.name} is ready and wants to order {newOrder.menuItem.itemName}!");

            OnCustomerReadyForService?.Invoke(newOrder);
        }
    }

    public override void Execute() { }
}