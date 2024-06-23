using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SubsystemsImplementation;

public class UI_Manager_Script : MonoBehaviour
{
    [SerializeField] private UnitScriptable unit;

    [Header("HUD UI")]
    [SerializeField] private TextMeshProUGUI stoneUI;
    [SerializeField] private TextMeshProUGUI woodUI;
    [SerializeField] private TextMeshProUGUI invCountUI;
    [SerializeField] private TextMeshProUGUI invTypeUI;
    [SerializeField] private TextMeshProUGUI objWood;
    [SerializeField] private TextMeshProUGUI objStone;

    [Header("Panels")]
    [SerializeField] private GameObject _gameClearPanel;
    
    
    private void Awake()
    {
        EventManager.UPDATE_WOOD_UI += UpdateWoodUI;
        EventManager.UPDATE_STONE_UI += UpdateStoneUI;
        EventManager.UPDATE_INVENTORY_UI += UpdateInventoryUI;
        EventManager.ON_OBJECTIVE_COMPLETE += UpdateObjectives;
        EventManager.ON_GAME_CLEAR += DisplayClearScreen;
    }
    private void Start()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
        stoneUI.text = "Stone: " + unit.StoneCollected;
        
        _gameClearPanel.SetActive(false);
    }

    public void UpdateWoodUI()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
    }

    public void UpdateStoneUI()
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

    private void UpdateObjectives(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Wood:
                objWood.fontStyle = FontStyles.Strikethrough;
                break;
            case ItemType.Stone:
                objStone.fontStyle = FontStyles.Strikethrough;
                break;
        }
    }

    private void DisplayClearScreen()
    {
        _gameClearPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.UPDATE_WOOD_UI -= UpdateWoodUI;
        EventManager.UPDATE_STONE_UI -= UpdateStoneUI;
        EventManager.UPDATE_INVENTORY_UI -= UpdateInventoryUI;
        EventManager.ON_OBJECTIVE_COMPLETE -= UpdateObjectives;
        EventManager.ON_GAME_CLEAR -= DisplayClearScreen;
    }
}
