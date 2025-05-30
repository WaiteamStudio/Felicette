using UnityEngine;
using DialogueEditor;

public class ConverationInput : MonoBehaviour
{
    public int counter = 0;
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
    private void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive && !PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                SoundManager.PlaySoundInPosition(SoundManager.Sound.DialogueNext, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                ConversationManager.Instance.PressSelectedOption();
            }

            /*            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && counter == 0) {
                            counter++;
                            ConversationManager.Instance.ScrollSpeed = 0f;
                        }
                        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && counter == 1)
                        {
                            ConversationManager.Instance.PressSelectedOption();
                            ConversationManager.Instance.ScrollSpeed = 1f;
                            counter = 0;
                        }*/

        }
    }
}
