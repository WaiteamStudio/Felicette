using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIMonobeh : MonoBehaviour, IService
{
    InventoryUIController inventoryUIController;
    UIDocument document;
    private void Start()
    {
        document = GetComponent<UIDocument>();
        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
        InventoryUIController inventoryUIController = ServiceLocator.Current.Get<InventoryUIController>();
        ItemDataBase itemDB = ServiceLocator.Current.Get<ItemDataBase>();
        inventoryUIController.Setup(document.rootVisualElement, inventoryController, itemDB);
    }
}