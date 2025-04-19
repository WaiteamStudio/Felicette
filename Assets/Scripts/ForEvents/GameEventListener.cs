using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { }

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        if (response != null)
        {
            response.Invoke(sender, data);
        }
        else
        {
            Debug.LogWarning($"[GameEventListener] Event {gameEvent?.name} raised, but no response set on {gameObject.name}");
        }
    }
}
