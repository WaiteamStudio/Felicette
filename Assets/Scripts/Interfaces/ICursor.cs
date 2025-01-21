using UnityEngine;

public interface ICursor
{
    Texture2D CursorTexture { get; }
    void Interact();
}
