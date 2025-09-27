using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MachinePrefabMapping
{
    public MachineType machineType;
    public GameObject machinePrefab;
}

public class MachineManager : MonoBehaviour
{
    public List<MachinePrefabMapping> machinePrefabs;
    public List<Transform> machinePlacementPoints;

    private List<BaseMachine> allMachines = new List<BaseMachine>();
    private bool[] isPointOccupied;

    public int machinecost = 0;

    void Awake()
    {
        isPointOccupied = new bool[machinePlacementPoints.Count];
    }

    private void OnEnable()
    {
        PlayerWallet.OnPlayerWalletUpdate += UpdateButtonInteractivity;
    }

    private void OnDisable()
    {
        PlayerWallet.OnPlayerWalletUpdate -= UpdateButtonInteractivity;
    }

    private void UpdateButtonInteractivity()
    {
        CafeManager.Instance.uiManager.ManageAddMachineButton(CafeManager.Instance.playerWallet.CurrentMoney >= machinecost);
    }

    public void SpawnNewMachine(MachineType typeToSpawn)    
    {
        GameObject prefabToSpawn = GetPrefabForType(typeToSpawn);
        if (prefabToSpawn == null)
        {
            Debug.LogError($"No prefab found for machine type: {typeToSpawn}");
            return;
        }
        int machineCost = prefabToSpawn.GetComponent<BaseMachine>().cost;
        machinecost = machineCost;
        if (!CafeManager.Instance.playerWallet.TrySpendMoney(machineCost))
            return;

        CafeManager.Instance.playerWallet.SpendMoney(machineCost);

        for (int i = 0; i < machinePlacementPoints.Count; i++)
        {
            if (!isPointOccupied[i])
            {
                Transform spawnPoint = machinePlacementPoints[i];
                GameObject machineInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
                BaseMachine newMachine = machineInstance.GetComponent<BaseMachine>();

                allMachines.Add(newMachine);
                isPointOccupied[i] = true;

                Debug.Log($"Spawned a new {newMachine.machineType} machine.");
                return;
            }
        }
        Debug.LogWarning("No empty machine points available!");
    }

    // to check if we already have a certain type of machine.
    public bool DoesMachineTypeExist(MachineType type)
    {
        foreach (var machine in allMachines)
        {
            if (machine.machineType == type)
            {
                return true;
            }
        }
        return false;
    }

    // Helper function to find the right prefab from our list.
    private GameObject GetPrefabForType(MachineType type)
    {
        foreach (var mapping in machinePrefabs)
        {
            if (mapping.machineType == type)
            {
                return mapping.machinePrefab;
            }
        }
        return null;
    }

    public BaseMachine FindAvailableMachine(MachineType type)
    {
        foreach (BaseMachine machine in allMachines)
        {
            if (machine.machineType == type && !machine.isBusy)
            {
                return machine;
            }
        }
        return null;
    }
}