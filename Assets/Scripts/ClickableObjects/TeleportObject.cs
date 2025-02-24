using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TeleportObject : MonoBehaviour, ICursor
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private LayerMask playerLayer;
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

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void Interact()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
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
        }
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
