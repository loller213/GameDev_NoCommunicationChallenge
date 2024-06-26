using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    private static UnitScript _instance;
    public static UnitScript Instance => _instance;

    [SerializeField] private UnitScriptable unit;

    [SerializeField] private UnitType TypeOfUnit;
    [SerializeField] private UnitState TypeOfState;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject Forest;
    [SerializeField] private GameObject Quarry;
    [SerializeField] private GameObject Home;

    [SerializeField] private string targetTag;

    //Mouse Input Related
    [SerializeField] private Vector3 target;
    [SerializeField] private LayerMask inputLayerMask;

    private LineRenderer _playerPath;
    public bool _inSafeZone;
    
    //For sound detection
    public bool _isPlayerMoving;

    //For Checking if Player has utilities, if so, it can mine stone or chop wood.
    [SerializeField] private GameObject PlayerUtilityHud;

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
        //Event
        EventManager.ON_CLICK_SET_DESTINATION += SetDestination;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Used to select layers that the mouse will interact with
        Camera.main.eventMask = inputLayerMask;

        //NavMesh 2D
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //UnitStats
        agent.speed = unit.UnitMoveSpd;
        TypeOfUnit = unit.typeOfUnit;
        
        //Visual Aids
        _playerPath = GetComponent<LineRenderer>();
        _playerPath.startWidth = .15f;
        _playerPath.endWidth = .15f;
        _playerPath.positionCount = 0;

        Forest = GameObject.FindGameObjectWithTag("Forest");
        Quarry = GameObject.FindGameObjectWithTag("Quarry");
        Home = GameObject.FindGameObjectWithTag("Home");

        PlayerUtilityHud.GetComponent<ItemUtilities>();

        target = transform.position;
        _inSafeZone = false;

        ResetResources();

    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            agent.SetDestination(target);
        }

        if (agent.hasPath)
        {
            //Enemy will follow player when inside sound detection collider even if inside home (DetectPlayerSound.cs)
            _isPlayerMoving = true;
            DrawPathLine();
        }
        else _isPlayerMoving = false;
    }

    //Used in On Clicked Manager 
    private void SetDestination(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        targetTag = target.tag;
    }

    private void DrawPathLine()
    {
        var pointCornerCount = agent.path.corners.Length;
        var pointCorners = agent.path.corners;

        _playerPath.positionCount = pointCornerCount;
        _playerPath.SetPosition(0, transform.position);
        
        if (pointCornerCount < 2) return;

        for (int i = 1; i < pointCornerCount; i++)
        {
            Vector3 pointPos = new Vector3(pointCorners[i].x, pointCorners[i].y, pointCorners[i].z);
            _playerPath.SetPosition(i, pointPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        
        if (collision.CompareTag("Forest") && PlayerUtilityHud.GetComponent<ItemUtilities>().HasAxeUtility)
        { 
            TypeOfState = UnitState.Cutting;
            ResourcesManager.Instance.StartAddingWood();
        }
        else if (collision.CompareTag("Quarry") && PlayerUtilityHud.GetComponent<ItemUtilities>().HasPickAxeUtility)
        { 
            TypeOfState = UnitState.Mining; 
            ResourcesManager.Instance.StartAddingStone();
        }
        else if (collision.CompareTag(Home.tag)) 
        { 
            TypeOfState = UnitState.Resting; 
            _inSafeZone = true;
            CraftItem craftStatus = FindObjectOfType<CraftItem>();
            craftStatus.CanCraft = true;
            
            ResourcesManager.Instance.DropItems();
            EventManager.ON_DROP_RESOURCES?.Invoke();
            Debug.Log("Resting");
        }
        else if (collision.CompareTag("Stick"))
        {
            TypeOfState = UnitState.Cutting;
            ResourcesManager.Instance.StartAddingWood();
            EventManager.UPDATE_INVENTORY_UI?.Invoke();
            ResourcesManager.Instance.StopAddingWood();

            Destroy(collision.gameObject, 1f); // add respawn
            //ResourceRespawn(collision.gameObject);
        }
        else if (collision.CompareTag("Stone"))
        {
            TypeOfState = UnitState.Mining;
            ResourcesManager.Instance.StartAddingStone();
            EventManager.UPDATE_INVENTORY_UI?.Invoke();
            ResourcesManager.Instance.StopAddingStone();
            
            Destroy(collision.gameObject, 1f);
            //ResourceRespawn(collision.GetComponent<GameObject>());
        }
        else if (collision.CompareTag("TrainingGrounds"))
        {
            TypeOfState = UnitState.Training;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Forest"))
        {
            ResourcesManager.Instance.StopAddingWood();
        }
        else if (collision.CompareTag("Quarry"))
        {
            ResourcesManager.Instance.StopAddingStone();
        }
        else if (collision.CompareTag(Home.tag))
        {
            CraftItem craftStatus = FindObjectOfType<CraftItem>();
            craftStatus.CanCraft = false;

            _inSafeZone = false;
        }
        TypeOfState = UnitState.Idle;
        targetTag = "";
    }
    
    //refactor this into a better structure
    public void CheckObjectives()
    {
        if (unit.WoodCollected >= 10)
        {
            EventManager.ON_OBJECTIVE_COMPLETE?.Invoke(ItemType.Wood);
        }
        else if (unit.StoneCollected >= 15)
        {
            EventManager.ON_OBJECTIVE_COMPLETE?.Invoke(ItemType.Stone);
        }

        if (unit.WoodCollected >= 10 && unit.StoneCollected >= 15)
        {
            EventManager.ON_GAME_CLEAR?.Invoke();
            Game_Manager.Instance._isGameOver = true;
        }
    }

    private void ResetResources()
    {
        unit.WoodCollected = 0;
        unit.StoneCollected = 0;
        EventManager.UPDATE_WOOD_UI?.Invoke();
        EventManager.UPDATE_STONE_UI?.Invoke();
    }


    //[SerializeField] private float resourceRespawnTimer;
    //private IEnumerator ResourceRespawn(GameObject resource)
    //{
    //    resource.SetActive(false);
    //    yield return new WaitForSeconds(resourceRespawnTimer);
    //    resource.SetActive(true);
    //}

    public UnitState ReturnUnitState()
    {
        return TypeOfState;
    }

    public int CollectionAmount()
    {
        return unit.UnitCollectionAmount;
    }

    public void SetWood(int wood)
    {
        unit.WoodCollected += wood;
    }

    public void SetStone(int stone)
    {
        unit.StoneCollected += stone;
    }

    private void OnDestroy()
    {
        EventManager.ON_CLICK_SET_DESTINATION -= SetDestination;
    }

    public int CheckUnitWood()
    {
        return unit.WoodCollected;
    }

    public int CheckUnitStone()
    {
        return unit.StoneCollected;
    }
}
