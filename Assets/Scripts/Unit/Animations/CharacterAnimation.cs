using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerChaseState
{
    public bool isChasing = false;
}

public class CharacterAnimation : MonoBehaviour
{
    //if you see the animation having extra jumps before character goes idle, make it higher, if the character slides, make it lower
    [SerializeField] private float idleMagnitude = 2f;

    [SerializeField] private Sprite normalExpression;
    [SerializeField] private Sprite scaredExpression;

    [SerializeField] private List<PlayerChaseState> pursuers = new List<PlayerChaseState>();

    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer sr;

    private bool isRunning = false;
    private bool isFlipped = false;
    private bool isScared = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();

        EventManager.ON_FOLLOW_PLAYER += ChasePlayer;
    }

    private void Update()
    {
        MakeCharacterRun(PlayerIsRunning());
        FlipCharacter(PlayerIsFacingLeft());
        ScarePlayer(PlayerIsBeingChased());
    }

    private void OnDestroy()
    {
        EventManager.ON_FOLLOW_PLAYER -= ChasePlayer;
    }

    private void MakeCharacterRun(bool state)
    {
        if(isRunning == state) return;
        isRunning = state;
        animator.SetBool("isRunning", state);
    }

    private bool PlayerIsRunning()
    {
        return agent.remainingDistance >= idleMagnitude;
    }

    private void FlipCharacter(bool state)
    {
        if(isFlipped == state) return;
        isFlipped = state;
        sr.flipX = isFlipped;
    }
    
    private bool PlayerIsFacingLeft()
    {
        return agent.velocity.x < 0;
    }

    private void ScarePlayer(bool state)
    {

        if(isScared == state) return;
        isScared = state;
        sr.sprite = isScared ? scaredExpression : normalExpression;
    }

    private bool PlayerIsBeingChased()
    {
        return pursuers.Count > 0;
    }

    private void ChasePlayer(PlayerChaseState state)
    {
        Debug.Log("Chasing Player!");
        if (pursuers.Contains(state)) return;

        pursuers.Add(state);

        StartCoroutine(WaitToFinishChase(state));
    }

    private IEnumerator WaitToFinishChase(PlayerChaseState state)
    {
        while(state.isChasing)
        {
            yield return null;
        }

        pursuers.Remove(state);
    }
}
