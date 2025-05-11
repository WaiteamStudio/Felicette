using UnityEngine;
using DialogueEditor;

public class DialogueStarter : MonoBehaviour, ICursor
{

    private GameObject player;
    private Animator playerAnimator;

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
        SoundManager.PlaySoundInPosition(SoundManager.Sound.DialogueNext, transform.position);
        ConversationManager.Instance.StartConversation(myConversation);

        player = GameObject.FindWithTag("Player");
        if (transform.position.y > player.transform.position.y)
        {
            playerAnimator = player.GetComponent<Animator>();
            //playerAnimator.SetTrigger("Interact");
            playerAnimator.SetBool("IsInteracting",true);
        }
    }
}

