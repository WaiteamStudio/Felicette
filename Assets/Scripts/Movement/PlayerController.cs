using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private Camera mainCamera;

    private void Start()
    {
        movement = GetComponent<IMovement>();
        mainCamera = Camera.main;
    }

    public void Move()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        movement.UpdateFollowSpot(new Vector2(mousePosition.x, mousePosition.y));
    }
}
