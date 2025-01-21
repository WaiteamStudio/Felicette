using UnityEngine;

public interface IMovement
{
    void UpdateFollowSpot(Vector2 newSpot);
    void Teleport(Vector3 position);
}
