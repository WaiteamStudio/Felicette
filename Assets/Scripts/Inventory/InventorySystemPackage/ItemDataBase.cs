using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Inventory", menuName = "Database")]
public class ItemDataBase : ScriptableObject, IService
{
    [SerializeField]
    private List<ItemDetailsSO> _ingameItems = new List<ItemDetailsSO>();
    private Dictionary<string, ItemDetailsSO> _db = new Dictionary<string, ItemDetailsSO>();
    public Dictionary<string, ItemDetailsSO> Items {  get {
            if(_db.Count == 0)
                PopulateDataBase();
            return _db; } }
    public ItemDetailsSO GetItemByGuid(string guid)
    {
        return Items[guid];
    }
    private void PopulateDataBase()
    {
        foreach (var itemDetails in _ingameItems)
        {
            _db.Add(itemDetails.GUID, itemDetails);
        }
    } 
}