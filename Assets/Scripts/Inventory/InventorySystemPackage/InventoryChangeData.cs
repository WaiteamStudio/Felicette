using System;
using System.Collections.Generic;
using UnityEditor;
[Serializable]
public class InventoryChangeData
{
    public string ItemGUID { get; set; }
    public ItemDetailsSO Item { get; set; }
    public int Amount {  get; set; }
    public InventoryChangeType ChangeType { get; set; }
    public InventorySlot InventorySlot { get; set; }
    public InventoryChangeData(ItemDetailsSO item, int amount, InventoryChangeType changeType, InventorySlot inventorySlot = null)
    {
        ItemGUID = item.GUID;
        Item = item;
        Amount = amount;
        ChangeType = changeType;
        InventorySlot = inventorySlot;
    }
    public new string ToString()
    {
        return ItemGUID + " : " + Amount;
    }
}
public class InventoryChangeDataContinues
{
    public Dictionary<string, int> Items { get; set; }
    public InventoryChangeType ChangeType { get; set; }
    public InventorySlot InventorySlot { get; set; }
}
