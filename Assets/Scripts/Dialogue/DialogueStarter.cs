using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour, ICursor
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private LayerMask playerLayer;
    [Header("Events")]
    [SerializeField] public GameEvent onDialogue;
    [SerializeField] public GameEvent offDialogue;

    public Texture2D CursorTexture => cursorTexture;

    public void Interact()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
        if (playerCollider != null)
        {
            var player = playerCollider.GetComponent<IMovement>();
            if (player != null && !PauseMenu.isPaused)
            {
                onDialogue.Raise(this, 0);
                ConversationManager.Instance.StartConversation(myConversation);
            }
        }
    }
}
