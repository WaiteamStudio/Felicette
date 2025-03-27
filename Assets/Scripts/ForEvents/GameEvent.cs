using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(Component sender, object data)
    {
        for (int i = 0; i < listeners.Count; i++)
            listeners[i].OnEventRaised(sender, data);
    }

    public void RaiseWithout(object data)
    {
        for (int i = 0; i < listeners.Count; i++)
            listeners[i].OnEventRaised(null, data); // Передаём null вместо sender
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
