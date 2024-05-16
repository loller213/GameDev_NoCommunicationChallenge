using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private EnemyScriptable enemyUnit;

    //Might wanna move this on the scriptable object
    [SerializeField] private float enemyBaseAttackCd;

    private float _timer;
    private bool _canAttack;

    private void Start()
    {
        _timer = enemyBaseAttackCd;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        
        AttackPlayer();
    }
    
    #region Methods

    private void AttackPlayer()
    {
        RunCdTimer();

        if (!_canAttack) return;
        
        EventManager.UPDATE_UNIT_HP?.Invoke(-enemyUnit.enemyDamage);
        _timer = enemyBaseAttackCd; // Reset CD
    }
    
    private void RunCdTimer()
    {
        _timer -= Time.deltaTime + (enemyUnit.enemyAtkSpd / 10);
        _canAttack = false;

        if (_timer > 0) return;
        _canAttack = true;
    }

    #endregion

}
