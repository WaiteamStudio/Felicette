using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "new ItemDetails/Poison", order = 1)]
public class PoisonItemDetailsSO : ItemDetailsSO
{
    [SerializeField]
    private List<ItemDetailsSO> UsableItems;

    public override bool TryUseOn(ItemDetailsSO inventoryItemSO)
    {
        throw new NotImplementedException();
        return false;
    }
}
