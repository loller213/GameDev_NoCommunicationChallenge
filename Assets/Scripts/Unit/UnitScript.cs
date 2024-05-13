using System.Collections;
using System.Collections.Generic;
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

        Forest = GameObject.FindGameObjectWithTag("Forest");
        Quarry = GameObject.FindGameObjectWithTag("Quarry");
        Home = GameObject.FindGameObjectWithTag("Home");

        target = transform.position;

    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            agent.SetDestination(target);
        }
    }

    //Used in On Clicked Manager 
    public void SetDestination(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        targetTag = target.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");

        if (collision.CompareTag(targetTag) && !collision.CompareTag("Enemy"))
        {
            agent.ResetPath();

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
                Debug.Log("Resting");
            }
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
