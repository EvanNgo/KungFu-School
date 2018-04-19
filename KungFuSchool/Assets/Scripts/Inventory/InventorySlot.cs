using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour {
    public Image icon;
    public Image selectedUI;
    public Text itemCount;
    public GameObject PurpleLine;
    public GameObject YelloLine;
    public GameObject SilverLine;
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
        switch (item.itemColor)
        {
            case Item.ItemColor.Purple:
                PurpleLine.SetActive(true);
                YelloLine.SetActive(false);
                SilverLine.SetActive(false);
                break;
            case Item.ItemColor.Yellow:
                PurpleLine.SetActive(false);
                YelloLine.SetActive(true);
                SilverLine.SetActive(false);
                break;
            case Item.ItemColor.Silver:
                PurpleLine.SetActive(false);
                YelloLine.SetActive(false);
                SilverLine.SetActive(true);
                break;
            default:
                PurpleLine.SetActive(false);
                YelloLine.SetActive(false);
                SilverLine.SetActive(false);
                break;
        }
    }

    // Clear the slot
    public void ClearSlot ()
    {   
        PurpleLine.SetActive(false);
        YelloLine.SetActive(false);
        SilverLine.SetActive(false);
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
                    DialogItemManager.instance.ShowItemInventory(item,true);
                    break;
                case InventoryManager.InventoryMode.Inventory:
                    DialogItemManager.instance.ShowItemInventory(item,false);
                    break;
            }
        }
    }
}
