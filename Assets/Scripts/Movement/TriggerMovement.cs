using UnityEngine;
using DialogueEditor;

public class TriggerMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!PauseMenu.isPaused && !ConversationManager.Instance.IsConversationActive)
            {
                GameEvents.current.MoveTo();
            }
        }
    }
}
