using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour, ICursor
{
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private Texture2D cursorTexture;
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

    public Texture2D CursorTexture => cursorTexture;

    public void Interact()
    {
        onDialogue.Raise(this, 0);
        SoundManager.PlaySoundInPosition(SoundManager.Sound.DialogueNext, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ConversationManager.Instance.StartConversation(myConversation);
    }
}

