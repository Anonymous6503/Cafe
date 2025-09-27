using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CounterSpot
{
    public Transform customerPoint;
    public Transform servePoint;
    public bool isOccupied;
    public Customer customerInSpot;

    public void FreeSpot()
    {
        isOccupied = false;
        customerInSpot = null;
    }
}

public class Counter : MonoBehaviour
{
    public List<CounterSpot> counterSpots;
}