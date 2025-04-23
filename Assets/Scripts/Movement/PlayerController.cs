using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IService
{
    private IMovement movement;
    private Camera mainCamera;
    private bool isMovingSoundPlaying = false;
    private Vector2 lastPosition;
    SoundManager _soundManager;
    SoundManager SoundManager
    {
        get
        {
            if (_soundManager == null)
                _soundManager = ServiceLocator.Current.Get<SoundManager>();
            return _soundManager;
        }
    }

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

        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, lastPosition);
        if (distance > 0.001f) // движется
        {
            if (!isMovingSoundPlaying)
            {
                SoundManager.PlaySoundInPosition(SoundManager.Sound.PlayerMove, transform.position);
                isMovingSoundPlaying = true;
            }
            else
            {
                // обновляем позицию и проверяем, что звук ещё играет
                var source = SoundManager.GetSource(SoundManager.Sound.PlayerMove);
                if (source != null)
                {
                    source.transform.position = transform.position;

                    if (!source.isPlaying)
                    {
                        source.Play();
                    }
                }
            }
        }
        else // стоит
        {
            if (isMovingSoundPlaying)
            {
                SoundManager.StopSound(SoundManager.Sound.PlayerMove);
                isMovingSoundPlaying = false;
            }
        }

        lastPosition = currentPosition;
    }

    public void Move()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        movement.UpdateFollowSpot(new Vector2(mousePosition.x, mousePosition.y));
    }

    private void HandleClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        float inventoryAreaHeight = Screen.height * 0.15f; // 20% ������ ������
        float mouseY = Input.mousePosition.y;

        if (mouseY > Screen.height - inventoryAreaHeight)
        {
            Debug.Log("���� � ������� ���������, �������� ��������");
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
