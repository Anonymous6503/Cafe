using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int startingMoney = 250;
    public int currentMoney = 0;

    public static event Action OnPlayerWalletUpdate;
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
        if (amount <= 0) return;
        CurrentMoney += amount;
        Debug.Log($"Received {amount}. New balance: {CurrentMoney}");

        OnPlayerWalletUpdate?.Invoke();
    }

    public bool TrySpendMoney(int amount)
    {
        if (amount > 0 && CurrentMoney >= amount)
        {
            Debug.Log($"Spent {amount}. New balance: {CurrentMoney}");
            return true;
        }
        else
        {
            Debug.Log($"Transaction failed! Insufficient funds to spend {amount}.");
            return false;
        }
    }

    public void SpendMoney(int amount)
    {
        currentMoney -= amount;
        OnPlayerWalletUpdate?.Invoke();
    }
}