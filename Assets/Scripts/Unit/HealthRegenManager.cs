using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthRegenManager : MonoBehaviour
{
    [SerializeField] private UnitScriptable unit;

    [SerializeField] private float _baseRegenCD;
    
    private float _timer;
    private bool _canRegen;
    
    #region UnityMethods

    private void Start()
    {
        //Might wanna move this on the scriptable object
        _timer = _baseRegenCD;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
         
        RegenHp();
    }

    #endregion

    #region Methods

    private void RegenHp()
    {
        RunCdTimer();

        if (!_canRegen) return;
        
            EventManager.UPDATE_UNIT_HP?.Invoke(unit.HP_UnitRegenAmt);
            _timer = _baseRegenCD; // Reset CD
    }
    
    private void RunCdTimer()
    {
        _timer -= Time.deltaTime + (unit.HP_UnitRegenSpd / 10);
        _canRegen = false;

        if (_timer > 0) return;
        _canRegen = true;
    }

    #endregion
    
    

}
