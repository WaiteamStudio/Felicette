using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
public class InventorySlot : VisualElement
{
    public Image Icon;
    public Label CountLabel;
    public string ItemGuid = "";
    private const string slotContainerSelectedClass = "slotContainerSelected";
    private InventoryUIController inventoryUIController;
    public void Init(InventoryUIController inventoryUIController)
    {
        this. inventoryUIController = inventoryUIController;
    }
    public InventorySlot()
    {
        CountLabel = new Label();
        Add(CountLabel);
        CountLabel.text = "";

        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);

            

        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        CountLabel.AddToClassList("slotCount");
        AddToClassList("slotContainer");

        //Register event listeners
        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }
    public void SetSelected()
    {
        AddToClassList(slotContainerSelectedClass);
        Debug.Log(ClassListContains(slotContainerSelectedClass));
    }
    public void SetUnselected()
    {
        RemoveFromClassList(slotContainerSelectedClass);
    }
    public void Hide()
    {
        Icon.style.visibility = Visibility.Hidden;
    }
    public void Show()
    {
        Icon.style.visibility = Visibility.Visible;
    }
    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button or this is an empty slotIn
        if (evt.button != 0 || ItemGuid.Equals(""))
        {
            return;
        }

        //Clear the image
        Hide();
        //Start the drag
        inventoryUIController.StartDrag(evt.position, this);
            
    }
    /// <summary>
    /// Sets the Icon and GUID properties
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"> Count of items</param>
    ///
    public void HoldItem(ItemDetailsSO item,int count)
    {
        if (item == null)
        {
            Debug.LogError("Cant hold null value");
            return;
        }
        if (count == 0)
            return;
        Icon.image = item.Icon.texture;
        Show();
        ItemGuid = item.GUID;
        if (count == 1)
            CountLabel.text = "";
        else
            CountLabel.text = count.ToString();
    }

    /// <summary>
    /// Clears the Icon and GUID properties
    /// </summary>
    public void DropItem()
    {
        ItemGuid = "";
        Icon.image = null;
        CountLabel.text = "";
    }
    public bool IsHoldingItem()
    {
        return ItemGuid != "";
    }

    public string GetGuid()
    {
        return ItemGuid;
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion

}

