using System;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public string ItemGuid => _itemGuid;
    private string _itemGuid = "";

    private VisualElement Icon;
    private Label CountLabel;

    private const string slotContainerSelectedClass = "slotContainerSelected";
    private InventoryUIController inventoryUIController;

    private string _labelText = "";

    public void Init(InventoryUIController inventoryUIController)
    {
        this.inventoryUIController = inventoryUIController;
    }

    public InventorySlot()
    {
        CountLabel = new Label();
        //Add(CountLabel); //вырубил так как нам это не надо
        CountLabel.text = _labelText;

        Icon = new VisualElement();
        Add(Icon);

        // Применяем стили
        Icon.AddToClassList("slotIcon");
        CountLabel.AddToClassList("slotCount");
        AddToClassList("slotContainer");

        // Обработчик нажатия
        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    public bool IsIconVisible() => Icon.style.visibility == Visibility.Visible;

    public void SetSelected()
    {
        AddToClassList(slotContainerSelectedClass);
    }

    public void SetUnselected()
    {
        RemoveFromClassList(slotContainerSelectedClass);
    }

    public void ToggleVisibility(bool visibility)
    {
        if (visibility)
            Show();
        else
            Hide();
    }

    public void Hide() => Icon.style.visibility = Visibility.Hidden;
    public void Show() => Icon.style.visibility = Visibility.Visible;

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0 || ItemGuid.Equals(""))
            return;

        Hide();
        inventoryUIController.StartDrag(evt.position, this);
    }

    public void HoldItem(ItemDetailsSO item, int count)
    {
        if (item == null)
        {
            Debug.LogError("Cant hold null value");
            return;
        }

        if (count == 0)
            return;

        SetIcon(item.Icon);
        _itemGuid = item.GUID;
        CountLabel.text = count == 1 ? "" : count.ToString();
    }

    public void DropItem()
    {
        _itemGuid = "";
        SetIcon( null);
        CountLabel.text = "";
        Hide();
    }

    public bool IsHoldingItem() => ItemGuid != "";
    public string GetGuid() => ItemGuid;

    internal void SetIcon(Sprite icon)
    {
        Icon.style.backgroundImage = new StyleBackground(icon);
    }

    // ✅ Настроим загрузку изображения и текста через UXML
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private UxmlStringAttributeDescription _labelText = new() { name = "label-text", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);

            InventorySlot slot = (InventorySlot)ve;

            // Получаем текст для CountLabel
            slot._labelText = _labelText.GetValueFromBag(bag, cc);
            slot.CountLabel.text = slot._labelText;
        }
    }
    #endregion
}
