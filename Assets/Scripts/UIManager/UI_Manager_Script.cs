using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager_Script : MonoBehaviour
{

    [SerializeField] private UnitScriptable unit;

    [SerializeField] private TextMeshProUGUI stoneUI;
    [SerializeField] private TextMeshProUGUI woodUI;
    [SerializeField] private TextMeshProUGUI invCountUI;
    [SerializeField] private TextMeshProUGUI invTypeUI;

    private void Awake()
    {
        EventManager.UPDATE_WOOD_UI += UpdateWoodUI;
        EventManager.UPDATE_STONE_UI += UpdateStoneUI;
        EventManager.UPDATE_INVENTORY_UI += UpdateInventoryUI;
    }
    private void Start()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
        stoneUI.text = "Stone: " + unit.StoneCollected;
    }

    private void UpdateWoodUI()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
    }

    private void UpdateStoneUI()
    {
        stoneUI.text = "Stone: " + unit.StoneCollected;
    }

    private void UpdateInventoryUI()
    {
        var itemCount = ResourcesManager.Instance.FetchInventory().InvCount;
        var itemType = ResourcesManager.Instance.FetchInventory().ItemTypeOnInv.ToString();

        invTypeUI.text = "Item Type: " + itemType;
        invCountUI.text = "Items in Inventory: " + itemCount;
    } 

    private void OnDestroy()
    {
        EventManager.UPDATE_WOOD_UI -= UpdateWoodUI;
        EventManager.UPDATE_STONE_UI -= UpdateStoneUI;
        EventManager.UPDATE_INVENTORY_UI -= UpdateInventoryUI;
    }

}
