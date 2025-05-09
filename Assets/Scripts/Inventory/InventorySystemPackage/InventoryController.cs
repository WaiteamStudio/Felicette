using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum InventoryChangeType
{
    Pickup,
    Drop
}
public class SaveData
{
    public List<ItemDetailsSaveInfo> itemDetailsSaveInfos = new();
}
/// <summary>
/// Generates and controls access to the Item Database and Inventory Data
/// </summary>
public class InventoryController : MonoBehaviour, IService
{
    private Dictionary<ItemDetailsSO, int> m_PlayerInventory = new Dictionary<ItemDetailsSO, int>();
    public event Action<InventoryChangeData> OnInventoryChanged = delegate { };
    public int MaxSlotsCount = 24;
    [SerializeField]
    public bool OpenOnStart;
    private ItemDataBase _db;
    private bool stackableItems = false;

    private void Start() // max added
    {
        Clear();
    }

    public ItemDataBase DB
    {
        get
        {
            if (_db == null)
                _db = ServiceLocator.Current.Get<ItemDataBase>();
            return _db;
        }
    }
    [ContextMenu("ClearInventory")]
    public void Clear()
    {
        foreach (var key in m_PlayerInventory.Keys.ToList())
        {
            RemoveItem(key, m_PlayerInventory[key]);
        }
    }

    [ContextMenu("RemoveFirstItem")]
    public void RemoveFirstItem()
    {
        RemoveItem(m_PlayerInventory.Keys.First());
    }
    public int GetItemCount(string guid)
    {
        ItemDetailsSO itemDetails = DB.GetItemByGuid(guid);
        if (m_PlayerInventory.Keys.Contains(itemDetails))
        {
            return m_PlayerInventory[itemDetails];
        }
        return 0;
    }
    public ItemDetailsSO GetItemByGuid(string itemGuid)
    {
        return DB.GetItemByGuid(itemGuid);
    }
    public Dictionary<ItemDetailsSO,int> GetAllItemDetails()
    {
        return m_PlayerInventory;
    }
    public Dictionary<string,int> GetAllItemDetailsByGuid()
    {
        var Items = m_PlayerInventory.ToDictionary(
        pair => pair.Key.GUID,
        pair => pair.Value );
        return Items;
    }
    public bool TryAddDefaultItemsToInventory()
    {
        Dictionary<ItemDetailsSO, int> items = new();
        foreach (var itemDetails in DB.Items)
        {
            items.Add(itemDetails.Value, 1);
        }
        return TryAddItems(items);
    }
    public bool TryAddDebugItem()
    {
        ItemDetailsSO itemDetails = DB.Items.First().Value;
        return TryAddItem(itemDetails);
    }
    public bool TryAddItems(Dictionary<ItemDetailsSO, int> items)
    {
        foreach (var item in items)
        {
            if (!TryAddItem(item.Key, item.Value))
                return false;
        }
        return true;
    }
    public bool TryAddItem(ItemDetailsSO itemDetails, int amount = 1)
    {
        if (HaveSpace(itemDetails))
        { 
            AddItem(itemDetails, amount);
            return true;    
        }
        return false;
    }
    public bool TryRemoveItem(ItemDetailsSO item)
    {
        if(m_PlayerInventory.ContainsKey(item))
        {
            return RemoveItem(item);
        }
        return false;
    }

    public bool HaveSpace(ItemDetailsSO itemDetails)
    {
       if(HaveEmptySlot())
            return true;
       else if(m_PlayerInventory.Keys.Contains(itemDetails))
            return true;
       else
            return false;
    }
    public bool HaveEmptySlot()
    {
        if(m_PlayerInventory.Count <= MaxSlotsCount)
        {
            return true;
        }
        else
        {

            return false;
        }
    }

    private void AddItem(ItemDetailsSO item, int amount = 1)
    {
        Debug.Log("added ietem " + item.Name);
        if(m_PlayerInventory.ContainsKey(item))
            m_PlayerInventory[item] += amount;
        else
            m_PlayerInventory.Add(item, amount);
        InventoryChangeData inventoryChangeData = new InventoryChangeData(item, amount, InventoryChangeType.Pickup);
        OnInventoryChanged.Invoke(inventoryChangeData);
    }
    private bool RemoveItem(ItemDetailsSO item, int count = 1)
    {
        m_PlayerInventory[item] -= count;
        if (m_PlayerInventory[item] == 0)
            m_PlayerInventory.Remove(item);
        InventoryChangeData inventoryChangeData = new InventoryChangeData(item, 1, InventoryChangeType.Drop);
        OnInventoryChanged.Invoke(inventoryChangeData);
        return true;
    }
}
