using UnityEngine;
using UnityEngine.UIElements;

public class UIBlocker : MonoBehaviour
{
    private static UIDocument[] uiDocuments = new UIDocument[2];
    public static bool IsPointerOverUI(Vector2 screenPosition)
    {
        // Получаем все UIDocument в сцене
        if(uiDocuments.LongLength == 0)
            uiDocuments = FindObjectsOfType<UIDocument>();
        foreach (var doc in uiDocuments)
        {
            var root = doc.rootVisualElement;
            var panel = root.panel;

            if (panel != null && panel.Pick(screenPosition) != null)
            {
                return true; // Курсор над UI, блокируем Raycast2D
            }
        }
        return false;
    }
}