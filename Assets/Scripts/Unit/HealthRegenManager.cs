using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenManager : MonoBehaviour
{
    [SerializeField] private UnitScriptable unit;

    [SerializeField] private float canRegen;

    private void Start()
    {
        canRegen = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (unit.HP_UnitRegenSpd <= canRegen)
            {
                EventManager.UPDATE_UNIT_HP?.Invoke(unit.HP_UnitRegenAmt);
                canRegen = 0f;
            }
            else
            {
                canRegen += Time.deltaTime;
            }
        }
    }

}
