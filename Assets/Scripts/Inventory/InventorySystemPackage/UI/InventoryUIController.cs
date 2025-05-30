﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class InventoryUIController : IService
{
    private const string m_ToggleVisibilityButtonShow = "VisibilityButton-show";
    private const string m_ToggleVisibilityButtonHide = "VisibilityButton-hide";

    public List<InventorySlot> InventorySlots = new();
    public string SelectedItemGUID = "";
    public event Action ItemSelected;
    public event Action NoItemSelected;
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    private VisualElement m_Inventory;
    private VisualElement m_GhostIcon;
    private Button m_ToggleVisibilityButton;

    public Action DraggingStarted;
    public Action DraggingEnded;
    private bool m_IsDragging;
    private InventorySlot m_OriginalSlot;
    private InventorySlot m_SelectedSlot;
    private InventoryController _inventoryController;
    private ItemDataBase _db;
    public void Setup(VisualElement inventoryRoot, InventoryController inventoryController, ItemDataBase itemDataBase)
    {
        //Store the root from the UI Document component
        this._inventoryController = inventoryController;
        _db = itemDataBase;
        m_Root = inventoryRoot;
        m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");
        m_Inventory = m_Root.Query<VisualElement>("Inventory");
        m_ToggleVisibilityButton = m_Root.Query<Button>("ToggleVisibilityButton");
        m_ToggleVisibilityButton.clicked += ToggleVisibilityButton;
        //Search the root for the SlotContainer Visual Element
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");

        //Create 20 InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < inventoryController.MaxSlotsCount; i++)
        {
            InventorySlot slot = new InventorySlot();
            slot.Init(this);

            InventorySlots.Add(slot);
            m_SlotContainer.Add(slot);
        }

        //Register event listeners
        inventoryController.OnInventoryChanged += GameController_OnInventoryChanged;
        InventoryChangeDataContinues data = new InventoryChangeDataContinues()
        {
            Items = inventoryController.GetAllItemDetailsByGuid(),
            ChangeType = InventoryChangeType.Pickup
        };
        GameController_OnInventoryChanged(data);
        inventoryRoot.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
        if (inventoryController.OpenOnStart)
            m_Root.style.visibility = Visibility.Visible;
        else
            m_Root.style.visibility = Visibility.Hidden;
    }

    /// <summary>
    /// Initiate the drag
    /// </summary>
    /// <param name="ScreenPosition">Mouse Position</param>
    /// <param name="originalSlot">Inventory Slot that the player has selected</param>
    public void StartDrag(Vector2 ScreenPosition, InventorySlot originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;
        Select(originalSlot);

        //Set the new position
        //Vector2 UIToollkitPostion = new Vector2(ScreenPosition.x, Screen.height - ScreenPosition.y);

        m_GhostIcon.style.top = ScreenPosition.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = ScreenPosition.x - m_GhostIcon.layout.width / 2;

        //Set the image
        m_GhostIcon.style.backgroundImage = _inventoryController.GetItemByGuid(originalSlot.ItemGuid).Icon.texture;

        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
        DraggingStarted.Invoke();
    }
    public bool IsHoldingItem()
    {
        return m_IsDragging;
    }
    public InventorySlot GetSelectedSlot()
    {
        return m_SelectedSlot;
    }
    public void SetSprite(Sprite sprite)
    {

    }
    private void UnSelect(InventorySlot LastSelectedSlot)
    {
        if (LastSelectedSlot == null)
            return;
        LastSelectedSlot.SetUnselected();
        m_SelectedSlot = null;
        SelectedItemGUID = "";
        NoItemSelected?.Invoke();
    }

    private void Select(InventorySlot selectedSlot)
    {
        if (selectedSlot != null && m_SelectedSlot != selectedSlot)
        {
            UnSelect(m_SelectedSlot);
            selectedSlot.SetSelected();
            m_SelectedSlot = selectedSlot;
            SelectedItemGUID = selectedSlot.ItemGuid;
            ItemSelected?.Invoke();
        }
        return;
    }

    /// <summary>
    /// Perform the drag
    /// </summary>
    private void OnPointerMove(PointerMoveEvent evt)
    {
        //Only take action if the player is dragging an slot around the screen
        if (!m_IsDragging)
        {
            return;
        }
        Debug.Log("Pointer Move");
        //Vector2 UIToolkitPos = new Vector2(evt.position.x, Screen.height - evt.position.y);
        //Set the new position
        m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;

    }

    /// <summary>
    /// Finish the drag and compute whether the slot should be moved to a new slot
    /// </summary>
    private void OnPointerUp(PointerUpEvent evt)
    {
        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlot> slots = InventorySlots.Where(x => x.worldBound.Overlaps(m_GhostIcon.worldBound));
        Debug.Log(slots.Count() + " slots.Count()");
        //Found at least one
        if (slots.Count() != 0)
        {
            InventorySlot closestSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, m_GhostIcon.worldBound.position)).First();
            if (closestSlot.IsHoldingItem()) //swap
            {
               ItemDetailsSO originalItem = _db.GetItemByGuid(m_OriginalSlot.ItemGuid);
               ItemDetailsSO closestlItem = _db.GetItemByGuid(closestSlot.ItemGuid);
                bool usingResult = originalItem.TryUseOn(closestlItem);
                if (!usingResult)
                    Swap(closestSlot, m_OriginalSlot);
                closestSlot.Show();
                m_OriginalSlot.Show();
            }
            else
            {
                Move(m_OriginalSlot, closestSlot);
                closestSlot.Show();
            }

        }
        else
        {
            m_OriginalSlot.SetIcon(_inventoryController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon);
            m_OriginalSlot.Show();
        }


        //Clear dragging related visuals and data
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;
        DraggingEnded.Invoke();
    }

    private void Move(InventorySlot from, InventorySlot to)
    {
        to.HoldItem(_inventoryController.GetItemByGuid(from.ItemGuid), _inventoryController.GetItemCount(from.ItemGuid));
        Select(to);
        from.DropItem();
    }

    private void Swap(InventorySlot closestSlot, InventorySlot originalSlot)
    {
        string ClosestSlotItemGuid = closestSlot.GetGuid();
        ItemDetailsSO itemOriginalSlot = (_inventoryController.GetItemByGuid(originalSlot.ItemGuid));
        ItemDetailsSO itemClosestSlot = (_inventoryController.GetItemByGuid(closestSlot.ItemGuid));
        closestSlot.HoldItem(itemOriginalSlot, _inventoryController.GetItemCount(itemOriginalSlot.GUID));
        Select(closestSlot);
        originalSlot.HoldItem(itemClosestSlot, _inventoryController.GetItemCount(itemClosestSlot.GUID));
    }

    /// <summary>
    /// Listen for changes to the players inventory and act
    /// </summary>
    /// <param name="itemGuid">Reference ID for the Item Database</param>
    /// <param name="change">Type of change that occurred. This could be extended to handle drop logic.</param>
    private void GameController_OnInventoryChanged(InventoryChangeDataContinues data)
    {
        //Loop through each slot and if it has been picked up, add it to the next empty slot
        foreach (var item in data.Items)
        {
            ItemDetailsSO  itemDetailsSO = _inventoryController.GetItemByGuid(item.Key);
            GameController_OnInventoryChanged(new InventoryChangeData(itemDetailsSO, item.Value, data.ChangeType));
        }
    }
    private void GameController_OnInventoryChanged(InventoryChangeData data)
    {
        string itemGUID = data.ItemGUID;
        ItemDetailsSO itemDetailsSO = data.Item;
        int newAmount = _inventoryController.GetItemCount(itemGUID);
        InventorySlot slot = InventorySlots.FirstOrDefault(x => x.GetGuid() == itemGUID);
        if (data.ChangeType == InventoryChangeType.Pickup)
        {
            OnPickup(itemDetailsSO, newAmount, slot);
        }
        else if (data.ChangeType == InventoryChangeType.Drop)
        {
            //slot = m_SelectedSlot;
            OnDrop(itemDetailsSO, newAmount, slot);
        }
    }

    private void OnDrop(ItemDetailsSO itemDetailsSO, int newAmount, InventorySlot slot)
    {
        if (slot == null)
        {
            Debug.LogError("removed item not found!");
            return;
        }
        if (newAmount == 0)
        {
            slot.DropItem();
            UnSelect(slot);
        }
        else
        {
            slot.HoldItem(itemDetailsSO, newAmount);
        }
    }

    private void OnPickup(ItemDetailsSO itemDetailsSO, int newAmount, InventorySlot slot)
    {
        if (slot != null)
        {
            slot.HoldItem(itemDetailsSO, newAmount);
            return;
        }
        else
        {
            InventorySlot emptySlot = InventorySlots.FirstOrDefault(x => x.GetGuid() == "");
            if (emptySlot != null)
            {
                emptySlot.HoldItem(itemDetailsSO, newAmount);
            }
            else
                throw new System.Exception("empty ui slot not found for added inventory slot " + itemDetailsSO.name);
        }
    }

    private bool IsInventoryVisible()
    {
        return m_Inventory.style.visibility == Visibility.Visible;
    }
    private void ToggleVisibilityButton()
    {
        Debug.Log("click!");
        bool state = IsInventoryVisible();
        if (state)
        {
            m_Inventory.style.visibility = Visibility.Hidden;
            m_ToggleVisibilityButton.RemoveFromClassList(m_ToggleVisibilityButtonHide);
            m_ToggleVisibilityButton.AddToClassList(m_ToggleVisibilityButtonShow);
            //HideIcons();
        }
        else
        {
            m_Inventory.style.visibility = Visibility.Visible;
            m_ToggleVisibilityButton.RemoveFromClassList(m_ToggleVisibilityButtonShow);
            m_ToggleVisibilityButton.AddToClassList(m_ToggleVisibilityButtonHide);
            //ShowIcons();
        }
    }
    private async void HideIcons()
    {
        foreach (var slot in InventorySlots)
        {
            Debug.Log((slot.parent == m_SlotContainer).ToString() + " parented of m_Inventory ; m_Inventory visibility: " + (m_Inventory.style.visibility == Visibility.Visible)); ;
            slot.Hide();
           //await ShowIconsAsync();
        }
    }
    private void ShowIcons()
    {
        foreach (var slot in InventorySlots)
        {
            Debug.Log((slot.parent == m_SlotContainer).ToString() + " parented of m_Inventory ; m_Inventory visibility: " + (m_Inventory.style.visibility == Visibility.Visible)); ;
            slot.Show();
        }
    }
    private async Task ShowIconsAsync()
    {
        await Task.Delay(2000);
        ShowIcons();
    }
}
