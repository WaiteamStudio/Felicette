using DialogueEditor;
using UnityEngine;
public class TeleportObject : MonoBehaviour, ICursor
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Transform teleportPoint;
    //[SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PerspectiveAdjuster perspectiveAdjuster;
    [SerializeField] private float perspectiveScaleToChange;
    [SerializeField] private float scaleRatioToChange;
    [Header("Events")]
    [SerializeField] public GameEvent onTpClickOnce;
    [SerializeField] public GameEvent offTeleport;

    private ICameraController cameraController;
    public Texture2D CursorTexture => cursorTexture;

    private float doubleClickTime = 0.3f;
    private float lastClickTime = 0f;
    public static bool IsClickHandled = false;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnMouseDown()
    {
        if (!PauseMenu.isPaused && ConversationManager.Instance.IsConversationActive == false)
        {
            if (Time.time - lastClickTime <= doubleClickTime)
            {
                Interact();
                IsClickHandled = true;
            }
            else
            {
                IsClickHandled = false;
            }

            lastClickTime = Time.time;
        }
    }

    public void Interact()
    {
        onTpClickOnce.Raise(this, 0);
        player.Teleport(teleportPoint.position);
        cameraController.FocusOn(cameraTarget);
        ChangePerspectiveScale();
        offTeleport.Raise(this, 0);

        /*        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
                if (playerCollider != null)
                {
                    var player = playerCollider.GetComponent<IMovement>();
                    if (player != null && !PauseMenu.isPaused)
                    {
                        onTpClickOnce.Raise(this, 0);
                        player.Teleport(teleportPoint.position);
                        cameraController.FocusOn(cameraTarget);
                        ChangePerspectiveScale();
                        offTeleport.Raise(this, 0);
                    }
                }*/
    }

    public void ChangePerspectiveScale()
    {
        if (perspectiveAdjuster != null)
        {
            perspectiveAdjuster.ChangePerspectiveScale(perspectiveScaleToChange);
            perspectiveAdjuster.ChangeScaleRatio(scaleRatioToChange);
        }
    }
}
