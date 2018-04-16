using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour {
    public Image icon;
    public Image selectedUI;
    public Text itemCount;
    Item item;  // Current item in the slot
    // Add item to the slot
    public void AddItem (Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        if (newItem.isStacking)
        {
            itemCount.text = newItem.count + "";
            itemCount.enabled = true;
        }
        else
        {
            itemCount.enabled = false;
        }
    }

    // Clear the slot
    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemCount.enabled = false;
    }

    // Use the item
    public void UseItem ()
    {
        if (item != null)
        {
            switch (InventoryManager.instance.inventoryMode)
            {
                case InventoryManager.InventoryMode.Shop:
                    DialogItemManager.instance.ShowItemInShop(item);
                    break;
                case InventoryManager.InventoryMode.Inventory:
                    DialogItemManager.instance.ShowDialog(item);
                    break;
            }
        }
    }
}
