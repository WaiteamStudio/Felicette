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
    [SerializeField] private bool isCameraFollow;
    [Header("Events")]
    [SerializeField] public GameEvent onTpClickOnce;
    [SerializeField] public GameEvent offTeleport;
    [SerializeField] public GameEvent cameraFollow;
    [SerializeField] public GameEvent unCameraFollow;
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

    public int numbrOfTimesUsed = 0; //������� ��� �������� ������� ������ � ���� � ������ ����

    private ICameraController cameraController;
    public Texture2D CursorTexture => cursorTexture;

    private float doubleClickTime = 0.3f;
    private float lastClickTime = 0f;
    public static bool IsClickHandled = false;

    public bool isOffice = false;
    public GameObject crimson;


    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnMouseDown()
    {
        if (!PauseMenu.isPaused && ConversationManager.Instance.IsConversationActive == false)
        {
            Debug.Log("�����");
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
        SoundManager.PlaySoundInPosition(SoundManager.Sound.Teleport, transform.position);
        ChangePerspectiveScale();
        if (isCameraFollow)
        {
            cameraFollow.Raise(this, 0);
            cameraController.FocusOn(cameraTarget);
        }
        else if (!isCameraFollow)
        {
            unCameraFollow.Raise(this, 0);
            cameraController.FocusOn(cameraTarget);
        }
        offTeleport.Raise(this, 0);
        numbrOfTimesUsed++;

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
        if (isOffice)
        {
            Animator crimanim = crimson.GetComponent<Animator>();
            if(crimanim.GetCurrentAnimatorStateInfo(0).IsName("Crimson_stand_up"))
                {
                crimanim.SetTrigger("idle");
                isOffice = false;
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
