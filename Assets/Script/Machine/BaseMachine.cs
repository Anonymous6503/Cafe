using UnityEngine;
using System;
using UnityEngine.UI;
public abstract class BaseMachine : MonoBehaviour
{
    public MachineType machineType;
    public int cost = 150;

    public Transform operationPoint;

    public GameObject progressBarContainer;
    public Image progressBarFillImage;

    public bool isBusy { get; protected set; }

    public event Action<Order> OnCraftingComplete;

    public abstract void StartCrafting(Order order, Transform parent = null);

    protected void CraftingDone(Order order)
    {
        OnCraftingComplete?.Invoke(order);
    }

    protected void UpdateProgressBar(float progress) // progress should be between 0 and 1
    {
        if (progressBarFillImage != null)
        {
            progressBarFillImage.fillAmount = progress;
        }
        if (progressBarContainer != null)
        {
            progressBarContainer.SetActive(progress > 0 && progress < 1); // Only show when crafting
        }
    }
}