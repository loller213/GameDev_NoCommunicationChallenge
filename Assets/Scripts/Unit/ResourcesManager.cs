using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager _instance;
    public static ResourcesManager Instance => _instance;

    [SerializeField] private InventorySystem _inventorySystem;

    private bool _canAddStone;
    private bool _canAddWood;

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

    #region Wood

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
        
        //Can only collect one type of item at a time
        if (_inventorySystem.GetItemType() == ItemType.Stone){Debug.LogError("Clear your inventory first!"); yield break;}; 
        
        while (UnitScript.Instance.ReturnUnitState() == UnitState.Cutting)
        {
            yield return new WaitForSeconds(1);
            _inventorySystem.AddItem(ItemType.Wood, UnitScript.Instance.CollectionAmount());
            EventManager.UPDATE_INVENTORY_UI?.Invoke();
        }
    }

    #endregion

    #region Stone

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
        
        //Can only collect one type of item at a time
        if (_inventorySystem.GetItemType() == ItemType.Wood){Debug.LogError("Clear your inventory first!"); yield break;}
        
        while (UnitScript.Instance.ReturnUnitState() == UnitState.Mining)
        {
            yield return new WaitForSeconds(1);
            _inventorySystem.AddItem(ItemType.Stone, UnitScript.Instance.CollectionAmount());
            EventManager.UPDATE_INVENTORY_UI?.Invoke();
        }
    }

    #endregion
    
    public void DropItems()
    {
        switch (_inventorySystem.GetItemType())
        {
            case ItemType.Stone:
                UnitScript.Instance.SetStone(_inventorySystem.InvCount);
                EventManager.UPDATE_STONE_UI?.Invoke();
                
                UnitScript.Instance.CheckObjectives();
                break;
            
            case ItemType.Wood:
                UnitScript.Instance.SetWood(_inventorySystem.InvCount);
                EventManager.UPDATE_WOOD_UI?.Invoke();
                
                UnitScript.Instance.CheckObjectives();
                break;
            
            default:
                return;
        }
        
        _inventorySystem.ClearInventory();
        EventManager.UPDATE_INVENTORY_UI?.Invoke();
    }

    public InventorySystem FetchInventory()
    {
        return _inventorySystem;
    }
}
