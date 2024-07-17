using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public GameObject buttonsPanel;
    public int quantity = 0;
    private CanvasGroup canvasGroup; // Reference to CanvasGroup component


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // button.onClick.AddListener(OnClick);
        ClearSlot();
    }

    public void AddItem(Item newItem, int amount)
    {
        item = newItem;
        icon.enabled = true;
        icon.sprite = newItem.itemIcon;
        quantity += amount;

        if (item.isStackable && quantity > 0)
        {
            quantityText.enabled = true;
            quantityText.text = quantity.ToString();
        }
        else
        {
            quantityText.enabled = false;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quantity = 0;
        quantityText.enabled = false;
    }

    public void UseItem()
    {
        if (item != null)
        {
            // Implement item use logic here based on item type
            Debug.Log("Using item: " + item.itemName);

            if (item.GetConsumable() != null)
            {
                ConsumableItem consumable = (ConsumableItem)item;
                //consumable.healthAdded
                //Add health
                
            }
            else if (item.GetTool() != null)
            {

            }
            else if (item.GetMisc() != null)
            {

            }

            // After using the item, clear the slot if necessary
            if (item.isStackable)
            {
                quantity--;
                if (quantity <= 0)
                {
                    ClearSlot();
                }
                else
                {
                    quantityText.text = quantity.ToString();
                }
            }
            else
            {
                ClearSlot();
            }
        }
    }

        public void ToggleButtonPanel()
    {
        // Toggle the visibility of the button panel
        if (buttonsPanel != null && item != null)
        {
            buttonsPanel.SetActive(true);
        }
    }

            public void DeactivateButtons()
    {
        // Toggle the visibility of the button panel
        if (buttonsPanel != null)
        {
            buttonsPanel.SetActive(false);
        }
    }


    public void DropItem()
    {
        if (item != null)
        {
            // Implement item drop logic here
            Debug.Log("Dropping item: " + item.itemName);
            ClearSlot();
        }
    }
}
