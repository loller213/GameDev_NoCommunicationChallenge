using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Villager,
    Miner,
    WoodCutter,
    Soldier
}

public enum UnitState
{
    Resting,
    Farming,
    Fighting,
    Transferring
}

[CreateAssetMenu(fileName = "New Unit", menuName ="Add New Unit/Add Basic Unit")]
public class UnitScriptable : ScriptableObject
{
    public int UnitMaxHP = 10;
    public int UnitMaxInventorySpace = 2;

    public int UnitLevelCap = 10;
    public int UnitLevel= 1;
    public int UnitExp;

    public int UnitAtk = 2;
    public float UnitMoveSpd = 1.5f;
    public float UnitCollectionSpeed = 0.5f;

    public int WoodCollected = 0;
    public int StoneCollected = 0;

    public UnitType typeOfUnit;
    public UnitState typeOfUnitState;
}
