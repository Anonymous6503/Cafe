using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public List<MenuItemData> unlockedItems;

    public void UnlockNewItem(MenuItemData itemToUnlock)
    {
        if (itemToUnlock == null) return;

        // Check to make sure we haven't already unlocked this item.
        if (!unlockedItems.Contains(itemToUnlock))
        {
            unlockedItems.Add(itemToUnlock);
            Debug.Log($"NEW ITEM UNLOCKED: {itemToUnlock.itemName} is now available on the menu!");
        }
    }

    public MenuItemData GetRandomMenuItem()
    {
        if (unlockedItems == null || unlockedItems.Count == 0)
        {
            Debug.LogError("The menu has no unlocked items available!");
            return null;
        }

        int randomIndex = Random.Range(0, unlockedItems.Count);
        return unlockedItems[randomIndex];
    }
}