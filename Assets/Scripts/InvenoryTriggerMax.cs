using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InvenoryTriggerMax : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement rootElement;
    [SerializeField] private bool isInventory;
    [Header("Events")]
    [SerializeField] public GameEvent onInventoryTrigger;
    [SerializeField] public GameEvent offInventoryTrigger;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        rootElement = uiDocument.rootVisualElement;

        // Скрыть UI документ
        //HideUI();
    }

/*    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            if(!PauseMenu.isPaused && !ConversationManager.Instance.IsConversationActive)
            {
                if (!isInventory)
                {
                    onInventoryTrigger.Raise(this, 0);
                    isInventory = true;
                }
                else if (isInventory)
                {
                    offInventoryTrigger.Raise(this, 0);
                    isInventory = false;
                }
            }
        }
    }*/

    public void HideUI()
    {
        rootElement.style.display = DisplayStyle.None;
    }

    public void ShowUI()
    {
        rootElement.style.display = DisplayStyle.Flex;
    }
}
