using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    private SpriteRenderer sprite;
    private NavMeshAgent agent;
    private Animator animator;

    private bool isFlipped = false;
    private bool isChasing = false;

    private bool AvatarIsFacingLeft { get { return agent.velocity.x < 0; } }

    private float distance { get { return Vector2.Distance(agent.transform.position, CharacterAnimation.player.transform.position); } }
    private bool PlayerIsInRange { get { return distance <= 7.5f ? true : false; } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FlipCharacter(AvatarIsFacingLeft);
        OpenMouth(PlayerIsInRange);
    }

    private void FlipCharacter(bool state)
    {
        if (isFlipped == state) return;
        isFlipped = state;
        sprite.flipX = isFlipped;
    }

    private void OpenMouth(bool state)
    {
        if (isChasing == state) return;
        isChasing = state;
        animator.SetBool("IsChasing", isChasing);
    }

}
