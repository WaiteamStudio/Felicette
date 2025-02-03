using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItemsFabric : MonoBehaviour,IService
{
    public UnityEvent<InventoryItemGO> itemCreated = new();
}
