﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
public class InventoryUIController : IService
{
    public List<InventorySlot> InventorySlots = new();
    public string SelectedItemGUID = "";
    public event Action ItemSelected;
    public event Action NoItemSelected;
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    private VisualElement m_GhostIcon;

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
        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
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

    }
    public InventorySlot GetSelectedSlot()
    {
        return m_SelectedSlot;
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
        //Only take action if the player is dragging an item around the screen
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
    /// Finish the drag and compute whether the item should be moved to a new slot
    /// </summary>
    private void OnPointerUp(PointerUpEvent evt)
    {

        if (!m_IsDragging)
        {
            return;
        }

        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlot> slots = InventorySlots.Where(x => x.worldBound.Overlaps(m_GhostIcon.worldBound));
        Debug.Log(slots.Count() + " slots.Count()");
        //Found at least one
        if (slots.Count() != 0)
        {
            InventorySlot closestSlot = slots.OrderBy(x => Vector2.Distance(x.worldBound.position, m_GhostIcon.worldBound.position)).First();
            if (closestSlot.IsHoldingItem()) //swap
            {
                Swap(closestSlot, m_OriginalSlot);
            }
            else
            {
                Move(m_OriginalSlot, closestSlot);

            }

        }
        else
        {
            m_OriginalSlot.Icon.image = _inventoryController.GetItemByGuid(m_OriginalSlot.ItemGuid).Icon.texture;
        }


        //Clear dragging related visuals and data
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;
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
        //Loop through each item and if it has been picked up, add it to the next empty slot
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
        if (data.ChangeType == InventoryChangeType.Pickup)
        {
            InventorySlot slot = InventorySlots.FirstOrDefault(x => x.GetGuid() == itemGUID);
            if (slot != null)
            {
                slot.HoldItem(itemDetailsSO, _inventoryController.GetItemCount(itemGUID));
                return;
            }
            else
            {
                InventorySlot emptySlot = InventorySlots.FirstOrDefault(x => x.GetGuid() == "");
                if (emptySlot != null)
                {
                    emptySlot.HoldItem(itemDetailsSO, _inventoryController.GetItemCount(itemGUID));
                }
                else
                    throw new System.Exception("empty ui slot not found for added inventory item " + itemDetailsSO.name);
            }
        }
        else if (data.ChangeType == InventoryChangeType.Drop)
        {
            if (m_SelectedSlot.ItemGuid == itemGUID)
            {
                if (data.Amount == 0)
                {
                    m_SelectedSlot.DropItem();
                    UnSelect(m_SelectedSlot);
                }
                else
                {
                    m_SelectedSlot.HoldItem(itemDetailsSO,_inventoryController.GetItemCount(itemGUID));
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
