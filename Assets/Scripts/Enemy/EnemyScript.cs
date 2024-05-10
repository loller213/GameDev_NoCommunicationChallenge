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
        if (collision.gameObject.CompareTag("Player"))
        {
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
