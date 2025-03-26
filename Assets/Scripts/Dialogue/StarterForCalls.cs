using DialogueEditor;
using UnityEngine;

public class StarterForCalls : MonoBehaviour
{
    [SerializeField] private NPCConversation npcConversation;
    [Header("Events")]
    [SerializeField] public GameEvent onDialogue;

    public void StartDialogue()
    {
        onDialogue.Raise(this, 0);
        ConversationManager.Instance.StartConversation(npcConversation);
    }
}
