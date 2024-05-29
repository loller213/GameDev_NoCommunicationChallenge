using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum AItype
{
    MeleeAI,
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
    private UnitScript playerUnit;

    private PlayerChaseState playerChaseState = new PlayerChaseState();

    private bool isFollowing = false;

    void Start()
    {
        //Can refactor this into a singleton function call instead
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        
        enemyRender = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        playerUnit = playerTarget.GetComponent<UnitScript>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isNearPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(targetWaypoints[1].position);
        DistFromPlayer();
        DistanceFromTarget();
        CheckDist();
        CheckFlip();

        if (!playerChaseState.isChasing) return;
        if (isFollowing) return;
        isFollowing = true;
        EventManager.ON_FOLLOW_PLAYER?.Invoke(playerChaseState);
    }

    private void DistFromPlayer()
    {
        playerDist = Vector2.Distance(gameObject.transform.position, playerTarget.transform.position);
    }

    private void DistanceFromTarget()
    {
        distFromCurrentTarget = Vector2.Distance(gameObject.transform.position, targetWaypoints[wayInt].transform.position);
    }

    private void CheckFlip()
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

    private void CheckDist()
    {
        switch (typeOfAI)
        {
            case AItype.MeleeAI:
              
                Debug.Log("Near Player Melee: " + isNearPlayer);
                if (playerDist >= 3) 
                { 
                    UseTypeOfAI(); 
                    if (isNearPlayer) 
                    { 
                        agent.speed = 3; 
                        isNearPlayer = false;
                    }
                }
                
                if (playerDist <= 10 && isNearPlayer == false && !playerUnit._inSafeZone) 
                { 
                    if (agent.speed >= 3) 
                    { 
                        Debug.Log("Following Player - Melee AI"); 
                        agent.SetDestination(playerTarget.transform.position);
                        playerChaseState.isChasing = true;
                    }

                    if (playerDist <= 1.2f) 
                    { 
                        Debug.Log("Near Player - Melee AI"); 
                        isNearPlayer = true;
                    }

                }
                break;
            
            case AItype.RangedAI:
                
                Debug.Log("Near Player Ranged: " + isNearPlayer);
                if (playerDist >= 8)
                {
                    UseTypeOfAI();
                    if (isNearPlayer)
                    {
                        agent.speed = 3;
                        isNearPlayer = false;
                    }
                }

                if (playerDist <= 12 && isNearPlayer == false && !playerUnit._inSafeZone)
                {
                    if (agent.speed >= 3)
                    {
                        Debug.Log("Following Player - Ranged AI");
                        agent.SetDestination(playerTarget.transform.position);
                        playerChaseState.isChasing = true;
                    }

                    if (playerDist <= 8f)
                    {
                        Debug.Log("Near Player - Ranged AI");
                        isNearPlayer = true;
                    }

                }
                break;
        }
    }

    private void UseTypeOfAI()
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
        playerChaseState.isChasing = false;
        isFollowing = false;
    }
}
