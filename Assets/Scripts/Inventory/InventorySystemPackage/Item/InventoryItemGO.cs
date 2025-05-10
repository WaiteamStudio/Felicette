using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryItemGO : MonoBehaviour, ICursor, IUsableOn
{
    private GameObject player;
    private Animator playerAnimator;

    [SerializeField] private ItemDetailsSO _inventoryItemSO;

    [SerializeField] private LayerMask playerLayer;

    //[SerializeField] private GameEvent eventUsed;
    public ItemDetailsSO InventoryItemSO => _inventoryItemSO;

    [HideInInspector] public UnityEvent<ItemDetailsSO> CollectStart;

    [HideInInspector] public UnityEvent<ItemDetailsSO> CollectEnd;

    [SerializeField] private Texture2D _cursorTexture;
    public Texture2D CursorTexture => _cursorTexture;
    SoundManager _soundManager;
    SoundManager SoundManager {
        get {
            if(_soundManager == null) 
                _soundManager = ServiceLocator.Current.Get<SoundManager>(); 
            return _soundManager; }
    }
    private void Awake()
    {
        //ServiceLocator.Current.Get<InventoryItemsFabric>().itemCreated.Invoke(this);
    }
    public void OnCollectEnd()
    {

        CollectEnd?.Invoke(_inventoryItemSO);
        Debug.Log($"Предмет {_inventoryItemSO.name} собран");
        Destroy(gameObject);
    }
    public void Collect()
    {
        bool ItemAdded = ServiceLocator.Current.Get<InventoryController>().TryAddItem(_inventoryItemSO);
        if (!ItemAdded)
        {
            Debug.Log("Item did not collected");
            return;
        }
        Debug.Log($"Предмет {_inventoryItemSO.name} начинает собираться");
        SoundManager.PlaySoundInPosition(SoundManager.Sound.ItemCollected,transform.position);
        CollectStart.Invoke(_inventoryItemSO);
        OnCollectEnd();
    }
    public void Interact()
    {
        //Collect();
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
        if (playerCollider != null)
        {
            var player = playerCollider.GetComponent<IMovement>();
            if (player != null && !PauseMenu.isPaused)
            {
                Collect();
            }
        }

        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetTrigger("Pick Up");

    }

    public bool Use(ItemDetailsSO itemDetailsSO)
    {
        if (itemDetailsSO.TryUseOn(_inventoryItemSO))
        {
            Debug.Log("SuccesfullyUsed!");
            //eventUsed.Raise(this, 0);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
