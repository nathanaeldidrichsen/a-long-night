using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int numberOfSlots;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public Image draggedItemImage;
    public Item draggedItem;
    public int draggedItemQuantity;

    public InventorySlot startSlot;
    public bool isHoldingItem;


 private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keeps the InventoryManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }

                for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            slots.Add(inventorySlot);
        }
    }

    public void HoldItem (Item item, InventorySlot inventorySlot)
    {
        draggedItemQuantity = inventorySlot.quantity;
        isHoldingItem = true;
        draggedItem = item;
        draggedItemImage.sprite = item.itemIcon;
        draggedItemImage.enabled = true;
        startSlot = inventorySlot;
    }

        public void PlaceItem ()
    {
        isHoldingItem = false;
        startSlot = null;
        draggedItem = null;
        draggedItemImage.enabled = false;
        draggedItemQuantity = 0;
    }

    public bool AddItem(Item item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null && slot.item.itemName == item.itemName && slot.item.isStackable)
            {
                slot.quantity += amount;
                slot.quantityText.text = slot.quantity.ToString();
                return true;
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(item, 1);
                return true;
            }
        }

        return false; // Inventory full
    }

    public bool CheckInventoryForItem(Item item, int amount)
    {
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == item && slot.quantity >= amount)
            {
            slot.ClearSlot();
            return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null && slot.item.itemName == item.itemName)
            {
                if (slot.item.isStackable && slot.quantity > 1)
                {
                    slot.quantity --;
                    slot.quantityText.text = slot.quantity.ToString();
                    return true;
                }
                else
                {
                    slot.ClearSlot();
                    return true;
                }
            }
        }

        return false; // Item not found in inventory
    }
}