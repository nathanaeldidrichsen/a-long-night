using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private InventorySlot inventorySlot;
    private InventoryManager inventoryManager;


    void Awake()
    {
        inventorySlot = GetComponentInParent<InventorySlot>();
        inventoryManager = InventoryManager.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (inventorySlot != null && inventorySlot.item != null)
        {
            inventoryManager.HoldItem(inventorySlot.item, inventorySlot);
            inventoryManager.draggedItemImage.transform.position = Input.mousePosition;
            inventorySlot.ClearSlot();  // Make the original slot empty
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventoryManager.isHoldingItem)
        {
            inventoryManager.draggedItemImage.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // GetComponentInParent<CanvasGroup>().blocksRaycasts = true;

        // Check if dropped in a valid slot
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<InventorySlot>() != null)
        {
            InventorySlot dropSlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            Debug.Log("Entered inventoryslot");

            if (dropSlot.item == null && inventoryManager.isHoldingItem)
            {
                // Place item in empty slot
                dropSlot.AddItem(inventoryManager.draggedItem, inventoryManager.draggedItemQuantity);
                inventorySlot.ClearSlot();
            }
            else if (dropSlot.item.itemName == inventoryManager.draggedItem.itemName && dropSlot.item.isStackable)
            {
                // Stack items
                dropSlot.quantity += inventoryManager.startSlot.quantity;
                dropSlot.quantityText.text = dropSlot.quantity.ToString();
                inventorySlot.ClearSlot();

            }
            else
            {
                // Swap items
                Item tempItem = dropSlot.item;
                dropSlot.AddItem(inventoryManager.draggedItem, inventoryManager.draggedItemQuantity);
                inventorySlot.AddItem(tempItem, dropSlot.quantity);

            }
        }
        else
        {
            // Return item to the original slot if not dropped in a valid slot
            inventorySlot.AddItem(inventoryManager.draggedItem, inventoryManager.draggedItemQuantity);
        }
        inventoryManager.PlaceItem();
    }

    public void ToggleButtons()
    {

            inventorySlot.ToggleButtonPanel();
    }
}
