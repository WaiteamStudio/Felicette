using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovement movement;
    private Camera mainCamera;

    private void Start()
    {
        movement = GetComponent<IMovement>();
        mainCamera = Camera.main;
        GameEvents.current.moveTo += Move;
    }

    private void Move()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        movement.UpdateFollowSpot(new Vector2(mousePosition.x, mousePosition.y));
    }

    private void OnDestroy()
    {
        GameEvents.current.moveTo -= Move;
    }
}
