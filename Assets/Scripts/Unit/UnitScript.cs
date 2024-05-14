using System.Collections;
using System.Collections.Generic;
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

        target = transform.position;
        _inSafeZone = false;

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
            DrawPathLine();
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
        
        if (collision.CompareTag(Forest.tag))
        { 
            TypeOfState = UnitState.Cutting;
            ResourcesManager.Instance.StartAddingWood();
        }
        else if (collision.CompareTag(Quarry.tag))
        { 
            TypeOfState = UnitState.Mining; 
            ResourcesManager.Instance.StartAddingStone();
        }
        else if (collision.CompareTag(Home.tag)) 
        { 
            TypeOfState = UnitState.Resting; 
            _inSafeZone = true; 
            Debug.Log("Resting");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Forest.tag))
        {
            ResourcesManager.Instance.StopAddingWood();
        }
        else if (collision.CompareTag(Quarry.tag))
        {
            ResourcesManager.Instance.StopAddingStone();
        }

        _inSafeZone = false;
        TypeOfState = UnitState.Idle;
        targetTag = "";

    }

    public UnitState ReturnUnitState()
    {
        return TypeOfState;
    }

    public int GetSpeed()
    {
        return unit.UnitCollectionSpeed;
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

}
