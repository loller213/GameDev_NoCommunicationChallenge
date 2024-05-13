using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHP_Script : MonoBehaviour
{
    private static UnitHP_Script _instance;
    public static UnitHP_Script Instance => _instance;

    [SerializeField] private UnitScriptable unit;

    [SerializeField] private Slider HealthBar;
    [SerializeField] private int currentUnitHP;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        //Events
        EventManager.UPDATE_UNIT_HP += UpdateHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentUnitHP = unit.UnitMaxHP;
        SetUpHealthbar();
    }

    public void UpdateHealth(int amount)
    {
        currentUnitHP += amount;
        currentUnitHP = Mathf.Clamp(currentUnitHP, 0, unit.UnitMaxHP);

        HealthBar.value = currentUnitHP;

        if (currentUnitHP <= 0)
        {
            Debug.Log("Im dead tank u poreva");
        }

    }

    private void SetUpHealthbar()
    {
        HealthBar.maxValue = unit.UnitMaxHP;
        HealthBar.value = currentUnitHP;
    }

    private void OnDestroy()
    {
        EventManager.UPDATE_UNIT_HP -= UpdateHealth;
    }
}
