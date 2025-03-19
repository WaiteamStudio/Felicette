using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour, ICursor
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private Texture2D cursorTexture;
    [Header("Events")]
    [SerializeField] public GameEvent onDialogue;

    public Texture2D CursorTexture => cursorTexture;

    public void Interact()
    {
        onDialogue.Raise(this, 0);
        ConversationManager.Instance.StartConversation(myConversation);
    }
}

