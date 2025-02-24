using System;
//using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemDetails", menuName = "new ItemDetails",order = 0)]
public class ItemDetailsSO : ScriptableObject
{
    public string Name;
    public string GUID;
    public Sprite Icon;
    public bool CanDrop;

    public void GenerateGUID()
    {
        GUID = System.Guid.NewGuid().ToString();
    }

    public virtual bool TryUseOn(ItemDetailsSO inventoryItemSO)
    {
        return false;
    }
    protected virtual void UseOn(ItemDetailsSO inventoryItemSO)
    {
        throw new NotImplementedException();
    }
}
/*[CustomEditor(typeof(ItemDetailsSO),true)]
public class ScriptableObjectWithGUIDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Получаем ссылку на целевой объект
        ItemDetailsSO scriptableObject = (ItemDetailsSO)target;

        // Добавляем кнопку в инспектор
        if (GUILayout.Button("Generate GUID"))
        {
            scriptableObject.GenerateGUID();

            // Обновляем изменения и сохраняем объект
            EditorUtility.SetDirty(scriptableObject);
        }
    }
}*/
