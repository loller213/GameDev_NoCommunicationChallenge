using System;
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

    private void Awake()
    {
        EventManager.ON_ENEMY_HIT += RemoveItem;
    }

    private void Start()
    {
        ClearInventory();
    }

    private void OnDestroy()
    {
        EventManager.ON_ENEMY_HIT -= RemoveItem;
    }

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

    private void RemoveItem()
    {
        if (InvCount <= 0) return;

        InvCount -= 1;
        if (InvCount == 0)
            ItemTypeOnInv = ItemType.None;
        
        EventManager.UPDATE_INVENTORY_UI?.Invoke();
    }
}
