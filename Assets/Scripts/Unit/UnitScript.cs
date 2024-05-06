using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{

    [SerializeField] private UnitScriptable unit;

    [SerializeField] private int MaxHP;
    [SerializeField] private int MaxInventorySpace;

    [SerializeField] private int MaxLevel;
    [SerializeField] private int CurrentLevel;
    [SerializeField] private int CurrentExp;

    [SerializeField] private int Damage;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float CollectionSpeed;

    [SerializeField] private UnitType TypeOfUnit;
    [SerializeField] private UnitState TypeOfState;

    [SerializeField] private NavMeshAgent agent;

    private GameObject Forest;
    private GameObject Quarry;
    private GameObject Home;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        MaxHP = unit.UnitMaxHP;
        MaxInventorySpace = unit.UnitMaxInventorySpace;

        MaxLevel = unit.UnitLevelCap;
        CurrentLevel = unit.UnitLevel;
        CurrentExp = unit.UnitExp;

        Damage = unit.UnitAtk;
        MoveSpeed = unit.UnitMoveSpd;
        agent.speed = MoveSpeed;
        CollectionSpeed = unit.UnitCollectionSpeed;

        TypeOfUnit = unit.typeOfUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I am a " + TypeOfUnit.ToString());

        

    }
}
