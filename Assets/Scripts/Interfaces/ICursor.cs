using UnityEngine;

public interface ICursor
{
    [SerializeField] Texture2D CursorTexture { get; }
    void Interact();
}
