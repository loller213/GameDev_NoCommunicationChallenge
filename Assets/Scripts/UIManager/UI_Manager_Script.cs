using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager_Script : MonoBehaviour
{

    [SerializeField] private UnitScriptable unit;

    [SerializeField] private TextMeshProUGUI stoneUI;
    [SerializeField] private TextMeshProUGUI woodUI;

    private void Awake()
    {
        EventManager.UPDATE_WOOD_UI += UpdateWoodUI;
        EventManager.UPDATE_STONE_UI += UpdateStoneUI;
    }

    private void Start()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
        stoneUI.text = "Stone: " + unit.StoneCollected;
    }

    public void UpdateWoodUI()
    {
        woodUI.text = "Wood: " + unit.WoodCollected;
    }

    public void UpdateStoneUI()
    {
        stoneUI.text = "Stone: " + unit.StoneCollected;
    }

    private void OnDestroy()
    {
        EventManager.UPDATE_WOOD_UI -= UpdateWoodUI;
        EventManager.UPDATE_STONE_UI -= UpdateStoneUI;
    }

}
