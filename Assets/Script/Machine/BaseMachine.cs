using UnityEngine;
using System;
public abstract class BaseMachine : MonoBehaviour
{
    public MachineType machineType;
    public int cost = 150;

    public Transform operationPoint;

    public bool isBusy { get; protected set; }

    public event Action<Order> OnCraftingComplete;

    public abstract void StartCrafting(Order order, Transform parent = null);

    protected void CraftingDone(Order order)
    {
        OnCraftingComplete?.Invoke(order);
    }
}