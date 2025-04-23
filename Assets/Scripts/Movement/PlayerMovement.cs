using DialogueEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement, IService
{
    [SerializeField] private float speed;
    private bool isMovementEnabled = true;
    private Vector2 followSpot;
    private UnityEngine.AI.NavMeshAgent agent;

    public Vector2 velocity => new Vector2(agent.velocity.x, agent.velocity.y); //��������� �������� ��� ��������� ��������

    private void Start()
    {
        followSpot = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
}
