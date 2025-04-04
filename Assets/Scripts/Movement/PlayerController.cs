using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IService
{
    private IMovement movement;
    private Camera mainCamera;

    private void Start()
    {
        movement = GetComponent<IMovement>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    public void Move()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        movement.UpdateFollowSpot(new Vector2(mousePosition.x, mousePosition.y));
    }

    private void HandleClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float inventoryAreaHeight = Screen.height * 0.15f; // 20% высоты экрана
        float mouseY = Input.mousePosition.y;

        if (mouseY > Screen.height - inventoryAreaHeight)
        {
            Debug.Log("Клик в области инвентаря, движение отменено");
            return;
        }

        if (TeleportObject.IsClickHandled)
        {
            TeleportObject.IsClickHandled = false;
            return;
        }

        //Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            ICursor icursor = hit.collider.GetComponent<ICursor>();
            if (icursor != null)
            {
                movement.UpdateFollowSpot(hit.collider.transform.position);
                //movement.UpdateFollowSpot(mousePosition);
                StartCoroutine(WaitAndInteract(icursor, hit.collider.transform.position));
                return;
            }
        }

        movement.UpdateFollowSpot(mousePosition);
        //onMovementTriggered.Raise(this, 0);
    }

    private IEnumerator WaitAndInteract(ICursor icursor, Vector2 targetPosition)
    {
        float startTime = Time.time;
        float timeout = 5f;

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 1f) //Vector2.Distance(transform.position, targetPosition) > 3f
        {
            if (Time.time - startTime > timeout)
            {
                yield break; 
            }

            yield return null;
        }

        //Debug.Log(Vector2.Distance(transform.position, targetPosition));
        icursor.Interact();
    }
}
