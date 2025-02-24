using UnityEngine;
using DialogueEditor;

public class ConverationInput : MonoBehaviour
{
    private void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive && !PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                ConversationManager.Instance.PressSelectedOption();
        }
    }
}
