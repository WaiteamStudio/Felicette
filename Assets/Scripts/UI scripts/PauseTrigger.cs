using UnityEngine;

public class PauseTrigger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent onPauseTrigger;
    [SerializeField] private GameEvent offPauseTrigger;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (PauseMenu.isPaused)
            {
                onPauseTrigger.Raise(this, 0);
            }
            else if (!PauseMenu.isPaused)
            {
                offPauseTrigger.Raise(this, 0);
            }
        }
    }
}
