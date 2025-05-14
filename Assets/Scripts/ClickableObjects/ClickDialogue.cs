using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ClickDialogue : MonoBehaviour, ICursor
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private GameEvent pylDialogue;
    [SerializeField] private Animator animator;
    private BoxCollider2D m_collider2D;
    

    public Texture2D CursorTexture => cursorTexture;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ConversationManager.Instance.IsConversationActive == false && PauseMenu.isPaused == false) // левая кнопка мыши
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (m_collider2D.OverlapPoint(mouseWorldPos))
            {
                Interact();
            }
        }
    }

    private void Awake()
    {
        m_collider2D = GetComponent<BoxCollider2D>();
    }

    public void Interact()
    {
        pylDialogue.Raise(this, 0);
    }

    public void PylesosAnim()
    {
        animator.SetBool("t_leave", true);
    }
}
