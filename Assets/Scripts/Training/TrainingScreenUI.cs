using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject TrainScreen;
    [SerializeField] private TrainButtons[] TrainButtons;
    private UnitScript unitScript;
    private UnitScriptable _unit;

    private bool isScreenOpen;
    private bool isPlayerOnTrainingGrounds;
    private int WoodCollected;
    private int StoneCollected;

    private void Start()
    {
        isScreenOpen = false;
        TrainScreen.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isPlayerOnTrainingGrounds && !isScreenOpen)
            {
                Debug.Log("OpeningTrainingScreen");
                OpenTrainUnitScreen(); 
            }
            else { CloseTrainUnitScreen(); }
        }
    }

    private void OpenTrainUnitScreen()
    {
        for (int i = 0; i < TrainButtons.Length; i++)
        {
            TrainButtons[i].UpdateButtonInteractability();
        }
        isScreenOpen = true;
        TrainScreen.SetActive(true);
    }

    private void CloseTrainUnitScreen()
    {
        isScreenOpen = false;
        TrainScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PlayerIsOnTrainingGrounds");
            isPlayerOnTrainingGrounds = true;
            unitScript = GetComponent<UnitScript>();
            if(unitScript != null)
            {
               WoodCollected = unitScript.CheckUnitStone();
               StoneCollected = unitScript.CheckUnitWood();
               Debug.Log("Collection: " + WoodCollected + StoneCollected);
            }
        }
        else
        {
            isPlayerOnTrainingGrounds = false;
        }
    }

    public int CheckUnitWood()
    {
        return WoodCollected;
    }

    public int CheckUnitStone()
    {
        return StoneCollected;
    }

}
