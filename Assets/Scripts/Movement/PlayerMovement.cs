using DialogueEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement, IService
{
    [SerializeField] private float speed;
    private bool isMovementEnabled = true;
    private Vector2 followSpot;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    public Vector2 velocity => new Vector2(agent.velocity.x, agent.velocity.y); //��������� �������� ��� ��������� ��������

    private void Start()
    {
        followSpot = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (ConversationManager.Instance.IsConversationActive == true)
        {
            agent.isStopped = true;
        }
        else if (ConversationManager.Instance.IsConversationActive == false)
        {
            agent.isStopped = false;
        }
        FeliAnimateBasic();
    }

    public void UpdateFollowSpot(Vector2 newSpot)
    {
        if (!isMovementEnabled) return;

        followSpot = newSpot;
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
    }

    public void Teleport(Vector3 position)
    {
        agent.Warp(position);
    }

    private void FeliAnimateBasic()
    {
        animator.SetInteger("horizontal speed", (int)(velocity.x * 10));
        animator.SetInteger("vertical speed", (int)(velocity.y * 10));

        if (Mathf.Abs(velocity.y) > 1.55 * Mathf.Abs(velocity.x)) animator.SetInteger("horizontal speed", 0);
        if (Mathf.Abs(velocity.x) > 1.55 * Mathf.Abs(velocity.y)) animator.SetInteger("vertical speed", 0);

        if (Mathf.Abs(velocity.x) < 0.15) animator.SetInteger("horizontal speed", 0);
        if (Mathf.Abs(velocity.y) < 0.15) animator.SetInteger("vertical speed", 0);

    }
}
