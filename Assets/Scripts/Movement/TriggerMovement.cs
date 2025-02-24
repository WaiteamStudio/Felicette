using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    [Header("Events")]
    public GameEvent onMovementTriggered;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMovementTriggered.Raise(this, 0);
        }
    }
}
