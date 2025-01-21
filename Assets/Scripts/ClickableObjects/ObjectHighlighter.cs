using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ObjectHighlighter : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    private void Start()
    {
        if (items != null || items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Highlight object is not assigned!");
        }
    }

    private void Update()
    {
        if (!PauseMenu.isPaused && !ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKey(KeyCode.F))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].SetActive(false);
                }
            }
        }
    }
}
