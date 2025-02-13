using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIMonobeh : MonoBehaviour, IService
{
    UIDocument document;
    ItemDataBase _itemDataBase;
    ItemDataBase ItemDataBase
    {
        get
        {
            if (_itemDataBase == null)
                _itemDataBase = ServiceLocator.Current.Get<ItemDataBase>();
            return _itemDataBase;
        }
    }
    InventoryController _inventoryController;
    InventoryController InventoryController
    {
        get
        {
            if (_inventoryController == null)
                _inventoryController = ServiceLocator.Current.Get<InventoryController>();
            return _inventoryController;
        }
    }
    InventoryUIController _inventoryUIController;
    InventoryUIController InventoryUIController
    {
        get
        {
            if (_inventoryUIController == null)
                _inventoryUIController = ServiceLocator.Current.Get<InventoryUIController>();
            return _inventoryUIController;
        }
    }
    SoundManager _soundManager;
    SoundManager SoundManager
    {
        get
        {
            if (_soundManager == null)
                _soundManager = ServiceLocator.Current.Get<SoundManager>();
            return _soundManager;
        }
    }

    private bool isPlayingSound;
    private void Awake()
    {
        InventoryUIController.DraggingStarted += PlayItemHoldingSound;
        InventoryUIController.DraggingEnded += StopItemHoldingSound;
    }
    private void Update()
    {
        if (!InventoryUIController.IsHoldingItem() && isPlayingSound)
        {
            StopItemHoldingSound();
            isPlayingSound = false;
        }
        else if(InventoryUIController.IsHoldingItem())
        {
            PlayItemHoldingSound();
            isPlayingSound = true;
        }
    }
    private void PlayItemHoldingSound()
    {
        SoundManager.PlaySoundInPosition(SoundManager.Sound.ItemIsHolding, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void StopItemHoldingSound()
    {
        SoundManager.StopSound(SoundManager.Sound.ItemIsHolding);

    }

    private void Start()
    {
        document = GetComponent<UIDocument>();
        InventoryUIController.Setup(document.rootVisualElement, InventoryController, ItemDataBase);
    }
}