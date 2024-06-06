using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem : MonoBehaviour
{
    //Attached in PlayerHUD canvas
    [SerializeField]private UnitScriptable item;
    [SerializeField] private GameObject CraftingScreenHUD;
    [SerializeField] private InventorySystem _craftingItem;
    ItemUtilities playerUtility;
    UI_Manager_Script updateHUD;
    bool isCraftHudOpen;
    public bool CanCraft = false;


    private void Start()
    {
        updateHUD = FindObjectOfType<UI_Manager_Script>();
        playerUtility = FindObjectOfType<ItemUtilities>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isCraftHudOpen && CanCraft) OpenCraftingHUD();
            else CloseCraftingHUD();
        }
    }

    public void CraftAxe()
    {
        int WoodCost = 2;
        int StoneCost = 3;

        if (item.WoodCollected >= WoodCost && item.StoneCollected >= StoneCost)
        {
            if (!playerUtility.HasAxeUtility)
            {
                playerUtility.HasAxeUtility = true;
                item.WoodCollected -= WoodCost;
                item.StoneCollected -= StoneCost;
                _craftingItem.AddItem(ItemType.Axe, 1);
                _craftingItem.HasItem(ItemType.Axe);
                Debug.Log("Crafted Axe");
                updateHUD.UpdateStoneUI();
                updateHUD.UpdateWoodUI();
            }
            else Debug.LogError("You Already have this item");
        }
        else
            Debug.Log("Not enough resources");
    }

    public void CraftPickAxe()
    {
        int WoodCost = 3;
        int StoneCost = 7;

        if (item.WoodCollected >= WoodCost && item.StoneCollected >= StoneCost)
        {
            if (!playerUtility.HasPickAxeUtility)
            {
                playerUtility.HasPickAxeUtility = true;
                item.WoodCollected -= WoodCost;
                item.StoneCollected -= StoneCost;
                _craftingItem.AddItem(ItemType.PickAxe, 1);
                _craftingItem.HasItem(ItemType.PickAxe);
                Debug.Log("Crafted Axe");
                updateHUD.UpdateStoneUI();
                updateHUD.UpdateWoodUI();
            }
            else Debug.LogError("You Already have this item");
        }
        else
            Debug.Log("Not enough resources");
    }

    void OpenCraftingHUD()
    {
        isCraftHudOpen = true;
        CraftingScreenHUD.SetActive(true);
    }
    void CloseCraftingHUD()
    {
        isCraftHudOpen = false;
        CraftingScreenHUD.SetActive(false);
    }
}
