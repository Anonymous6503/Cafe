using UnityEngine;

[CreateAssetMenu(fileName = "NewMenuItem", menuName = "Cafe/New Menu Item")]
public class MenuItemData : ScriptableObject
{
    public string itemName;
    public MachineType machineTypeRequired;
    public float creationTime = 3f;

    public GameObject productPrefab;
}