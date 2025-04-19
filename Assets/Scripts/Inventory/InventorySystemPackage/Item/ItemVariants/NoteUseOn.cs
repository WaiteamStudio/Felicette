using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteUseOnItem", menuName = "new ItemDetails/NoteUseOnItem", order = 12)]
public class NoteUseOn : ItemDetailsSO
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
        //���� ����� �� ���� � �� �� �������� - ����������
        //���� ������ �� ������ ��������� - ���� ���, ����� ����������� ��������

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
        }
    }
}
