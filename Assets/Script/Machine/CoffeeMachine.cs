using System.Collections;
using UnityEngine;

public class CoffeeMachine : BaseMachine
{
    public Transform productSpawnPoint;

    private void Start()
    {
        UpdateProgressBar(0);
    }

    public override void StartCrafting(Order order, Transform parent = null)
    {
        // Check if the machine is free and the order is for this machine type.
        if (!isBusy && order.menuItem.machineTypeRequired == this.machineType)
        {
            // Start the coffee-making process.
            StartCoroutine(ProcessOrderRoutine(order, parent));
        }
    }

    private IEnumerator ProcessOrderRoutine(Order order, Transform parent)
    {
        Debug.Log($"Starting to brew {order.menuItem.itemName}...");

        isBusy = true;

        float timer = 0f;
        while (timer < order.menuItem.creationTime)
        {
            timer += Time.deltaTime;
            // Update the UI progress bar during crafting
            UpdateProgressBar(timer / order.menuItem.creationTime);
            yield return null;
        }
        UpdateProgressBar(1);
        /*if (order.menuItem.productPrefab != null && productSpawnPoint != null)
        {
            Instantiate(order.menuItem.productPrefab, productSpawnPoint.position, Quaternion.identity);
        }*/

        if (parent != null)
        {
            Instantiate(order.menuItem.productPrefab,parent);
        }

        Debug.Log($"{order.menuItem.itemName} is ready!");

        isBusy = false;

        CraftingDone(order);

        yield return null;
    }
}