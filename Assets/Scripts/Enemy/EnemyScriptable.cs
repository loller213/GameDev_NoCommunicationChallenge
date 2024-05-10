using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Meele,
    Ranged,
    Tower
}

public enum EnemyState
{
    Idle,
    Roaming,
    Attacking,
    Dead
}

[CreateAssetMenu(fileName = "New Enemy", menuName = "Add New Unit/Add Enemy Unit")]
public class EnemyScriptable : ScriptableObject
{

    public EnemyType typeOfEnemy;
    public EnemyState stateOfEnemy;

    public int enemyMaxHP = 5;

    public int enemyDamage = 1;
    public float enemyAtkSpd = 1;

    public float enemyMoveSpd = 2f;

}
