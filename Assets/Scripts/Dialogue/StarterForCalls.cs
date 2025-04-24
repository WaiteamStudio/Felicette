using DialogueEditor;
using UnityEngine;

public class StarterForCalls : MonoBehaviour
{
    [SerializeField] private NPCConversation npcConversation;
    [Header("Events")]
    [SerializeField] public GameEvent onDialogue;
    SoundManager _soundManager;
    SoundManager SoundManager
    {
        get
        {
            if (_soundManager == null)
                _soundManager = ServiceLocator.Current.Get<SoundManager>();
            return _soundManager;
        }
    }

    public void StartDialogue()
    {
        onDialogue.Raise(this, 0);
        SoundManager.PlaySoundInPosition(SoundManager.Sound.DialogueNext, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ConversationManager.Instance.StartConversation(npcConversation);
    }
}
