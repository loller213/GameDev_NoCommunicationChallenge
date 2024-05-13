using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private EnemyScriptable enemyUnit;

    [SerializeField] private float enemyCanAttack;

    private void Start()
    {
        enemyCanAttack = 0;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //Maybe use a guard clause forr this one instead? or better, convert this into raycast 2d instead of collision 2d
        if (collision.gameObject.CompareTag("Player"))
        {
            //This kind of condition uses a timer wherein enemy attacks has cooldowns, might need to rework this.
            if (enemyUnit.enemyAtkSpd <= enemyCanAttack)
            {
                EventManager.UPDATE_UNIT_HP?.Invoke(-enemyUnit.enemyDamage);
                enemyCanAttack = 0f;
            }
            else
            {
                enemyCanAttack += Time.deltaTime;
            }
        }
    }

}
