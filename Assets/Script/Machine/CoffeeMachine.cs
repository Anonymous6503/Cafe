using System.Collections;
using UnityEngine;

public class CoffeeMachine : BaseMachine
{
    public Transform productSpawnPoint;

    public override void StartCrafting(Order order)
    {
        // Check if the machine is free and the order is for this machine type.
        if (!isBusy && order.menuItem.machineTypeRequired == this.machineType)
        {
            // Start the coffee-making process.
            StartCoroutine(ProcessOrderRoutine(order));
        }
    }

    private IEnumerator ProcessOrderRoutine(Order order)
    {
        Debug.Log($"Starting to brew {order.menuItem.itemName}...");

        isBusy = true;

        yield return new WaitForSeconds(order.menuItem.creationTime);

        if (order.menuItem.productPrefab != null && productSpawnPoint != null)
        {
            Instantiate(order.menuItem.productPrefab, productSpawnPoint.position, Quaternion.identity);
        }

        Debug.Log($"{order.menuItem.itemName} is ready!");

        isBusy = false;

        CraftingDone(order);

        yield return null;
    }
}