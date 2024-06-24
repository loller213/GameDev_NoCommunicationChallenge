using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum UnitType
{
    Villager,
    Miner,
    WoodCutter,
    Soldier
}

public enum UnitState
{
    Idle,
    Resting,
    Cutting,
    Mining,
    Fighting,
    Transferring,
    Training
}

[CreateAssetMenu(fileName = "New Unit", menuName ="Add New Unit/Add Basic Unit")]
public class UnitScriptable : ScriptableObject
{
    public int UnitMaxHP = 10;

    public int UnitLevelCap = 10;
    public int UnitLevel= 1;
    public int UnitExp;

    public int UnitAtk = 2;
    public float UnitMoveSpd = 1.5f;
    public int UnitCollectionAmount = 1; //Increases amount of resources gathered per second

    public int HP_UnitRegenAmt = 1;
    public float HP_UnitRegenSpd = 3; 

    public int WoodCollected = 0;
    public int StoneCollected = 0;

    public UnitType typeOfUnit;
    public UnitState typeOfUnitState;

    // For Training
    public float TimeToTrain;
    public GameObject Prefab;

}
