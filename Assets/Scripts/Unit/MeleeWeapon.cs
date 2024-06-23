using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UnitScript _unit;
    [SerializeField] private float _cooldownTimer = 3.0f;
    private bool _isEnemyWithinRange;
    private bool _canAttack;
    private EnemyScriptable _enemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && _canAttack)
        {
            // play animation
            // check if enemy is within attack range

            if (_isEnemyWithinRange)
            {
                AttackEnemy();
            }

            _animator.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            _enemy = collision.gameObject.GetComponent<EnemyScriptable>();
            _isEnemyWithinRange = true; 
        }
    }

    private void AttackEnemy()
    {
        if(_enemy != null)
        {
            _enemy.enemyMaxHP -= 1;
            StartCoroutine(AttackCDHandler(_cooldownTimer));
        }
    }

    IEnumerator AttackCDHandler(float _timer)
    {
        _canAttack = false;
        yield return new WaitForSeconds(_timer);
        _canAttack = true;
    }

}
