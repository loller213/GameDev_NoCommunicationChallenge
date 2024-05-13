using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AItype
{
    WaypointAI,
    RangedAI
}

public class EnemyNavMeshAI : MonoBehaviour
{

    [Header("Type of AI")]
    [SerializeField] private AItype typeOfAI;

    [Header("Player Stuff")]
    [SerializeField] private Transform playerTarget;

    [Header("AI Values")]
    [SerializeField] private Transform[] targetWaypoints;
    [SerializeField] private int wayInt;
    [SerializeField] private bool isNearPlayer = false;

    [SerializeField] SpriteRenderer enemyRender;

    [Header("Distance")]
    [SerializeField] private float distFromCurrentTarget;
    [SerializeField] private float newDist;
    [SerializeField] private float playerDist;
 
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        enemyRender = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        
        //Can refactor this into a singleton function call instead
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isNearPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(targetWaypoints[1].position);
        distFromPlayer();
        DistanceFromTarget();
        CheckDist();
        checkFlip();

    }

    private void distFromPlayer()
    {
        playerDist = Vector2.Distance(gameObject.transform.position, playerTarget.transform.position);
    }

    private void DistanceFromTarget()
    {
        distFromCurrentTarget = Vector2.Distance(gameObject.transform.position, targetWaypoints[wayInt].transform.position);
    }

    public void checkFlip()
    {
        //Refactor into something simpler
        if (typeOfAI == AItype.WaypointAI || typeOfAI == AItype.RangedAI)
        {
            if (agent.desiredVelocity.x < 0)
            {
                enemyRender.flipX = false;
            }
            else if (agent.desiredVelocity.x > 0)
            {
                enemyRender.flipX = true;
            }
        }
    }

    public void CheckDist()
    {

        if (typeOfAI == AItype.WaypointAI)
        {

            Debug.Log("Near Player Melee: " + isNearPlayer);
            if (playerDist >= 3)
            {
                UseTypeOfAI(typeOfAI);
                if (isNearPlayer == true)
                {
                    agent.speed = 3;
                    isNearPlayer = false;
                }
            }

            if (playerDist <= 10 && isNearPlayer == false)
            {
                if (agent.speed >= 3)
                {
                    Debug.Log("Following Player - Melee AI");
                    agent.SetDestination(playerTarget.transform.position);
                }

                if (playerDist <= 1.2f)
                {
                    Debug.Log("Near Player - Melee AI");
                    isNearPlayer = true;
                }

            }
            else if (playerDist <= 1.2f && isNearPlayer == true)
            {
                //  isNearPlayer = true;
                agent.speed = 0;
            }
        }

        if (typeOfAI == AItype.RangedAI)
        {

            Debug.Log("Near Player Ranged: " + isNearPlayer);
            if (playerDist >= 8)
            {
                UseTypeOfAI(typeOfAI);
                if (isNearPlayer == true)
                {
                    agent.speed = 3;
                    isNearPlayer = false;
                }
            }

            if (playerDist <= 12 && isNearPlayer == false)
            {
                if (agent.speed >= 3)
                {
                    Debug.Log("Following Player - Ranged AI");
                    agent.SetDestination(playerTarget.transform.position);
                }

                if (playerDist <= 8f)
                {
                    Debug.Log("Near Player - Ranged AI");
                    isNearPlayer = true;
                }

            }
            else if (playerDist <= 8f && isNearPlayer == true)
            {
                //  isNearPlayer = true;
                agent.speed = 0;
            }
        }

    }

    public void UseTypeOfAI(AItype AI)
    {
        if (typeOfAI == AItype.WaypointAI || typeOfAI == AItype.RangedAI)
        {
            if (Vector2.Distance(gameObject.transform.position, targetWaypoints[wayInt].transform.position) <= 2)
            {
                wayInt++;
                if (wayInt >= targetWaypoints.Length)
                {
                    wayInt = 0;
                }
            }
            Debug.Log("Not Following Player");
            agent.SetDestination(targetWaypoints[wayInt].transform.position);
        }

    }

}
