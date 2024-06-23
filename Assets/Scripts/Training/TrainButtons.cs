using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TrainButtons : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TrainingScreenUI GroundsScript;
    [SerializeField] private UnitScriptable _unitToTrain;
    [SerializeField] private int woodCost;
    [SerializeField] private int stoneCost;

    [Header("For UI")]
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField]private Button _trainButton;

    private UI_Manager_Script uiManager;
    private bool canTrainUnits;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UI_Manager_Script>();

        woodText.text = woodCost.ToString();
        stoneText.text = stoneCost.ToString();
    }

    public void UpdateButtonInteractability()
    {
        canTrainUnits = GroundsScript.CheckUnitWood() >= woodCost && GroundsScript.CheckUnitStone() >= stoneCost;
        _trainButton.interactable = canTrainUnits;
    }

    public void OnButtonClick()
    {

        if (_trainButton.interactable)
        {
            StartCoroutine(TrainingCoroutine(_unitToTrain));

            _unitToTrain.WoodCollected -= woodCost;
            _unitToTrain.StoneCollected -= stoneCost;
            EventManager.UPDATE_INVENTORY_UI?.Invoke();
            UpdateButtonInteractability();
        }
    }

    private IEnumerator TrainingCoroutine(UnitScriptable unitType)
    {
        canTrainUnits = false;

        EventManager.UPDATE_INVENTORY_UI?.Invoke();

        yield return new WaitForSeconds(unitType.TimeToTrain);

        Instantiate(unitType.Prefab);

        canTrainUnits = true;
    }

}
