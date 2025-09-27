using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MasterItemDatabase", menuName = "Cafe/Item Database")]
public class MenuItemDatabase : ScriptableObject
{
    public List<MenuItemData> allPossibleItems;
}