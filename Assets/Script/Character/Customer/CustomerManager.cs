using System.Collections;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;

    public Counter counter;

    public Transform spawnPoint;

    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 15.0f;

    void Start()
    {
        StartCoroutine(SpawnCustomerRoutine());
    }

    private IEnumerator SpawnCustomerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            CounterSpot freeSpot = FindFreeCounterSpot();

            if (freeSpot != null)
            {
                freeSpot.isOccupied = true;

                GameObject customerInstance = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

                Customer customerAI = customerInstance.GetComponent<Customer>();
                freeSpot.customerInSpot = customerAI;
                customerAI.Setup(this.spawnPoint, freeSpot);

                customerAI.MoveTo(freeSpot);
            }
        }
    }

    private CounterSpot FindFreeCounterSpot()
    {
        foreach (var spot in counter.counterSpots)
        {
            if (!spot.isOccupied)
            {
                return spot;
            }
        }
        return null;
    }
}