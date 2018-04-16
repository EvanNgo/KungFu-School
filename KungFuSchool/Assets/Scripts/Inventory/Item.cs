using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {
    //Basic item's information
    public int id;
    new public string name = "New Item";
    public string details;
    public Sprite icon = null;
    public bool isStacking = false;
    public int count = 1;
    public int priceBuy = 0;
    public int priceSell = 0;
    public ItemType itemType;
    public bool isEquiping = false;
    //Equipment Item's information
    public EquipmentSlot equipSlot;
    public Option defaultOption;
    public int defaultPoint;
    public Option[] options;
    public int[] points;
    public Color itemColor;
    public virtual void Use()
    {
        if (itemType == ItemType.Equipment)
        {
            EquipmentManager.instance.EquipItem(this,EquipItem ());
            DialogItemManager.instance.CloseDialog();
        }
    }

    public virtual void Buy()
    {
        Inventory.instance.BuyItem(this);
        DialogItemManager.instance.CloseDialog();
    }

    public int EquipItem(){
        return Inventory.instance.EquipItem(this);
    }

    public void RemoveFromInventory ()
    {
       Inventory.instance.Remove(this);
       DialogItemManager.instance.CloseDialog();
    }

    public Color GetItemColor(){
        if (itemColor != null)
        {
            itemColor = itemColor;
        }
        if (options == null)
        {
            itemColor = Color.white;
        }
        else if (options.Length > 0 && options.Length < 3)
        {
            itemColor = Color.green;
        }
        else if (options.Length >= 3 && options.Length < 5)
        {
            itemColor = Color.blue;
        }
        else if (options.Length >= 5 && options.Length < 7)
        {
            itemColor = new Color32(156, 39, 176, 255);
        }
        else if (options.Length == 7 || options.Length == 8)
        {
            itemColor = Color.yellow;
        }
        else if (options.Length == 9)
        {
            itemColor = Color.gray;
        }
        return itemColor;
    }
    public enum EquipmentSlot { Head, Armor, Gloves , Pant , Foot , Weapon , Shield , Pedan, Ring , Rare}
    public enum ItemType { Equipment , HPPotion , MPPotion , Question , Gold}
}
