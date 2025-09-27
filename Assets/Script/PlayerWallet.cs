using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int startingMoney = 250;
    public int currentMoney = 0;
    // A property to access the current money from other scripts safely
    public int CurrentMoney 
    { 
        get => currentMoney;
        set => currentMoney = value;
    }

    void Start()
    {
        CurrentMoney = startingMoney;
        Debug.Log($"Wallet initialized. Current balance: {CurrentMoney}");
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return; // Can't add zero or negative money
        CurrentMoney += amount;
        Debug.Log($"Received {amount}. New balance: {CurrentMoney}");
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount > 0 && CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            Debug.Log($"Spent {amount}. New balance: {CurrentMoney}");
            return true;
        }
        else
        {
            Debug.Log($"Transaction failed! Insufficient funds to spend {amount}.");
            return false;
        }
    }
}