using UnityEngine;

public class MiniGameStarter : MonoBehaviour, ICursor
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Transform cameraTargetMiniGame;
    [SerializeField] private Transform previousPositionOfCamera;
    [Header("Events")]
    [SerializeField] public GameEvent miniGameStart;

    private ICameraController cameraController;
    public Texture2D CursorTexture => cursorTexture;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void Interact()
    {
        miniGameStart.Raise(this, 0);
        cameraController.FocusOn(cameraTargetMiniGame);
    }
    
    public void EndMiniGame()
    {
        cameraController.FocusOn(previousPositionOfCamera);
    }
}
