using System.Collections.Generic;
using UnityEngine;

public class CashierManager : MonoBehaviour
{
    public GameObject cashierPrefab;
    public int cashierHireCost = 100;
    public Transform hireSpawnPoint;

    private List<Cashier> allCashiers = new List<Cashier>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            HireNewCashier();
        }
    }

    private void HireNewCashier()
    {
        if (cashierPrefab == null || hireSpawnPoint == null)
        {
            Debug.LogError("Cashier Prefab or Spawn Point is not set in the CashierManager!");
            return;
        }
        if (!CafeManager.Instance.playerWallet.TrySpendMoney(cashierHireCost))
            return;

        GameObject cashierInstance = Instantiate(cashierPrefab, hireSpawnPoint.position, Quaternion.identity);
        cashierInstance.name = $"Cashier {allCashiers.Count + 1}";

        Cashier newCashier = cashierInstance.GetComponent<Cashier>();

        if (newCashier != null)
        {
            allCashiers.Add(newCashier);
            Debug.Log($"Hired {cashierInstance.name}. Total cashiers: {allCashiers.Count}");
        }
    }

    public Cashier GetNearestIdleCashier(Vector3 targetPosition)
    {
        Cashier nearestCashier = null;
        float closestDistance = float.MaxValue;

        foreach (Cashier cashier in allCashiers)
        {
            if (cashier.stateMachine.CurrentState is CashierIdleState)
            {
                float distance = Vector3.Distance(cashier.transform.position, targetPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestCashier = cashier;
                }
            }
        }

        return nearestCashier;
    }

}