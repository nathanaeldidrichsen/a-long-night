using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public GameObject requiredItemPrefab;
    public Transform requiredItemParent;
    public GameObject itemClickedObject;
    public Image itemClickedImage;
    public TextMeshProUGUI craftItemName;
    [SerializeField] private Recipe recipeToCraft;


    public void SetRequiredItems(Recipe recipe)
    {
        craftItemName.text = recipe.recipeName;

        itemClickedImage.sprite = recipe.craftedItem.itemIcon;
        // Clear previous required items
        foreach (Transform child in requiredItemParent)
        {
            Destroy(child.gameObject);
        }

        recipeToCraft = recipe;

        // Instantiate new required items
        foreach (Item reqItem in recipe.requiredItems)
        {
            GameObject reqItemObject = Instantiate(requiredItemPrefab, requiredItemParent);
            RequiredItem requiredItem = reqItemObject.GetComponent<RequiredItem>();
            requiredItem.SetRequiredItem(reqItem);
        }
    }

    public void ShowRequiredItems()
    {
        itemClickedObject.SetActive(true);
    }

    public void CloseRequiredItems()
    {
        itemClickedObject.SetActive(false);
    }

    public void CraftItem()
    {
        if (recipeToCraft == null)
        {
            return;
        }

        // Add crafted item to the inventory (assuming you have a method to add the crafted item)
        InventoryManager.Instance.AddItem(recipeToCraft.craftedItem, 1);
        // For example: InventoryManager.Instance.AddItem(craftedItem);

        // Remove required items from the inventory
        RemoveRequiredItemsFromInventory();

        // Disable the item clicked object
        //itemClickedObject.SetActive(false);

        // Update which craftable items the player can click on
        // Implement any additional logic to update craftable items
        recipeToCraft = null;
    }

    public void CheckIfHaveRequiredItems()
    {
        if (recipeToCraft != null)
        {
            foreach(Item requiredRecipeItem in recipeToCraft.requiredItems)
            {
                if (InventoryManager.Instance.CheckInventoryForItem(requiredRecipeItem, 1) == false)
                {
                    //you didn't have one of the items needed
                    // Disable the item clicked object
                    itemClickedObject.SetActive(false);

                    // Update which craftable items the player can click on
                    // Implement any additional logic to update craftable items
                    recipeToCraft = null;
                    CloseRequiredItems();

                }
                
            }
        }
    }

    public void RemoveRequiredItemsFromInventory()
    {
        foreach (Transform child in requiredItemParent)
        {
            RequiredItem requiredItem = child.GetComponent<RequiredItem>();
            if (requiredItem != null)
            {
                InventoryManager.Instance.RemoveItem(requiredItem.GetRequiredItem());
            }
        }
    }
}
