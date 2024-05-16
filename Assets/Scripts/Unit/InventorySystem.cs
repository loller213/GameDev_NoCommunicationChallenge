using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Wood,
    Stone,
    None
}

public class InventorySystem : MonoBehaviour
{
    public ItemType ItemTypeOnInv;
    public int InvCount;
    
    [SerializeField] private int _inventoryLimit;

    public void AddItem(ItemType itemType, int qty)
    {
        if (InvCount > _inventoryLimit){Debug.LogError("Inventory Full"); return;}
        
        ItemTypeOnInv = itemType;
        InvCount += qty;
    }

    public void ClearInventory()
    {
        ItemTypeOnInv = ItemType.None;
        InvCount = 0;
    }

    public ItemType GetItemType()
    {
        return ItemTypeOnInv;
    }
}
