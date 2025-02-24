using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

    /// <summary>
    /// Загрузчик сервисов для сцены с игрой
    /// </summary>
    public class ServiceLocatorLoader_Main : MonoBehaviour
    {
        private DataManager _dataSaver;
        private SaveSystem _saveSystem;
        private InventoryUIController _inventoryUIController;
        [SerializeField]
        private InventoryController _inventoryController;
        [SerializeField]
        private InventoryUIMonobeh _inventoryUIMonobeh;
        [SerializeField]
        private ItemDataBase _itemDataBase;
        private void Awake()
        {
            Create();
            RegisterServices();
            Init();
        }

        private void Create()
        {
            _dataSaver = new();
            _saveSystem = new JsonSaveSystem();
            _inventoryUIController = new();
        }

        private void RegisterServices()
        {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_itemDataBase);
            ServiceLocator.Current.Register(_inventoryController);
            ServiceLocator.Current.Register(_dataSaver);
            ServiceLocator.Current.Register(_inventoryUIController);
            ServiceLocator.Current.Register(_inventoryUIMonobeh);

        }

        private void Init()
        {
            _dataSaver.Init(_inventoryController, _saveSystem);
        }
    }
