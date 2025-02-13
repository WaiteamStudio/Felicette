using System;
using UnityEngine;

public class DataManager : IService
{
    private const string INVENTORYFILE = "PlayerInventory";
    private InventoryController _inventoryController;
    private SaveSystem _saveSystem;
    public void Init(InventoryController inventoryController, SaveSystem saveSystem) 
    {
        _inventoryController = inventoryController;
        _saveSystem = saveSystem;
        LoadInventoryData();
        Application.quitting += OnApplicationQuit;
    }
    private void LoadInventoryData()
    {
        bool InventoryLoadResult = LoadInventory();
        if (!InventoryLoadResult)
        {
            _inventoryController.TryAddDefaultItemsToInventory();
        }
    }

    public void OnApplicationQuit()
    {
        SaveInventory();
        //SaveProgress();
        //SavePosition();
    }
    private void SaveInventory()
    {
        SaveData data = new SaveData();
        var inventoryData = _inventoryController.GetAllItemDetailsByGuid();
        foreach (var item in inventoryData)
        {
            data.itemDetailsSaveInfos.Add(new ItemDetailsSaveInfo()
            {
                ItemGUID = item.Key,
                count = item.Value
            });
        }
        _saveSystem.Save(data, INVENTORYFILE);
    }
    private bool LoadInventory()
    {
        try
        {
            SaveData data = _saveSystem.Load<SaveData>(INVENTORYFILE);
            foreach (var item in data.itemDetailsSaveInfos)
            {
                ItemDetailsSO itemDetails = _inventoryController.GetItemByGuid(item.ItemGUID);
                _inventoryController.TryAddItem(itemDetails, item.count);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("JsonSaveSystem.Load<SaveData> went wrong with ex: " + e);
            return false;
        }

    }
}
