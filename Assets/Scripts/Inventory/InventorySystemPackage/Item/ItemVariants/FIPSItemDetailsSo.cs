using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FIPS", menuName = "new ItemDetails/FIPS", order = 10)]
public class FIPSItemDetailsSo : ItemDetailsSO
{
    [SerializeField]
    private List<ItemDetailsSO> CanBeUsedOn;
    [SerializeField]
    private ItemDetailsSO Result;
    [SerializeField] private GameEvent conversationStart;

    public override bool TryUseOn(ItemDetailsSO inventoryItemSO)
    {
        if (!CanBeUsedOn.Contains(inventoryItemSO))
            return false;
        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
        //если слота не было и он не появится - исключение
        //если почему то нельзя ремувнуть - тоже баг, можно клонировать предметы

        if (!inventoryController.HaveSpace(Result))
        {
            //Debug.LogError("no empty slot appeared after removing items!!!");
            //inventoryController.TryAddItem(this);
            return false;
        }
        UseOn(inventoryItemSO);
        return true;
    }
    protected override void UseOn(ItemDetailsSO syndicateItemDetailsSO)
    {
        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
        bool resultRemoving = inventoryController.TryRemoveItem(this);
        //Debug.Log("Батарея удален");
        conversationStart.RaiseWithout(0);

        /*        InventoryController inventoryController = ServiceLocator.Current.Get<InventoryController>();
                bool resultRemoving = inventoryController.TryRemoveItem(this);
                bool resultRemoving2 = inventoryController.TryRemoveItem(syndicateItemDetailsSO);
                if (!resultRemoving)
                {
                    Debug.Log("error removing" + this);
                    return;
                }
                if (!resultRemoving2)
                {
                    Debug.Log("error removing " + syndicateItemDetailsSO);
                    return;
                }
                bool resultAdding = inventoryController.TryAddItem(Result);
                if (!resultAdding)
                {
                    Debug.Log("error adding");
                    return;
                }
                if (resultAdding)
                {
                    Debug.Log("Succeful used " + this + " on " + syndicateItemDetailsSO);
                }*/
    }
}
