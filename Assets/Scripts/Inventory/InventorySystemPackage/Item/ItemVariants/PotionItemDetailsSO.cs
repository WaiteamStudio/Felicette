using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "new ItemDetails/Potion", order = 2)]
public class PotionItemDetailsSO : ItemDetailsSO
{
    [SerializeField]
    private List<ItemDetailsSO> CanBeUsedOn;
    [SerializeField]
    private ItemDetailsSO Result;

    public override bool TryUseOn(ItemDetailsSO inventoryItemSO)
    {
        if (!CanBeUsedOn.Contains(inventoryItemSO))
            return false;
        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
        //если слота не было и он не появится - исключение
        //если почему то нельзя ремувнуть - тоже баг, можно клонировать предметы
       
        if (!inventoryController.HaveSpace(Result))
        {
            Debug.LogError("no empty slot appeared after removing items!!!");
            inventoryController.TryAddItem(this);
            return false;
        }
        UseOn(inventoryItemSO);
        return true;
    }
    protected override void UseOn(ItemDetailsSO syndicateItemDetailsSO)
    {
        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
        bool resultRemoving =  inventoryController.TryRemoveItem(this);
        bool resultAdding =  inventoryController.TryAddItem(Result);
        Debug.Log("Succeful used " + this + " on " + syndicateItemDetailsSO);
    }
}
