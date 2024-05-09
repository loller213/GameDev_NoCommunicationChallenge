using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager _instance;
    public static ResourcesManager Instance => _instance;

    private UnitState state;

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
    }

    public void StartAddingWood()
    {
        StartCoroutine(AddWood());
    }

    public void StopAddingWood()
    {
        StopCoroutine(AddWood());
    }

    IEnumerator AddWood()
    {
        Debug.Log("Cutting");
        while (UnitScript.Instance.ReturnUnitState() == UnitState.Cutting)
        {
            yield return new WaitForSeconds(1);
            UnitScript.Instance.SetWood(UnitScript.Instance.GetSpeed());
            Debug.Log("Getting Wood: " + UnitScript.Instance.GetWood());
        }
    }

    public void StartAddingStone()
    {
        StartCoroutine(AddStone());
    }

    public void StopAddingStone()
    {
        StopCoroutine(AddStone());
    }

    IEnumerator AddStone()
    {
        Debug.Log("Mining");
        while (UnitScript.Instance.ReturnUnitState() == UnitState.Mining)
        {
            yield return new WaitForSeconds(1);
            UnitScript.Instance.SetStone(UnitScript.Instance.GetSpeed());
            Debug.Log("Getting Stone: " + UnitScript.Instance.GetStone());
        }
    }

}
