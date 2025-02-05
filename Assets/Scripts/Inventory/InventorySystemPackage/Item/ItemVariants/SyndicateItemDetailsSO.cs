using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Syndicate", menuName = "new ItemDetails/Syndicate", order = 3)]
public class SyndicateItemDetailsSO : ItemDetailsSO
{
    [SerializeField]
    private List<ItemDetailsSO> UsableItems;
    [SerializeField]
    private ItemDetailsSO Result;
    public override bool TryUseOn(ItemDetailsSO inventoryItemSO)
    {
        if (!UsableItems.Contains(inventoryItemSO))
        {
            return false;
        }
        Use(inventoryItemSO as PotionItemDetailsSO);
        return true;
    }
    private void Use(PotionItemDetailsSO potionItemDetailsSO)
    {
       
    }
}
