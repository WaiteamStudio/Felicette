using System;
using UnityEngine;

public class MoveEvent : MonoBehaviour
{
    public static MoveEvent current;

    private void Awake()
    {
        current = this;
    }

    public event Action moveTo;
    public void MoveTo()
    {
        if (moveTo != null)
        {
            moveTo();
        }
    }
}
