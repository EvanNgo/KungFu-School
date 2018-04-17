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
        if (options == null || options.Length == 0)
        {
            return Color.white;
        }
        else if (options.Length > 0 && options.Length < 3)
        {
            return Color.green;
        }
        else if (options.Length >= 3 && options.Length < 5)
        {
            return new Color32(3, 169, 244, 255);
        }
        else if (options.Length >= 5 && options.Length < 7)
        {
            return new Color32(156, 39, 176, 255);
        }
        else if (options.Length == 7 || options.Length == 8)
        {
            return Color.yellow;
        }
        else
        {
            return Color.gray;
        }
    }

    public string GetEquipName(){
        switch(equipSlot){
            case EquipmentSlot.Head:
                return "Mũ";
                break;
            case EquipmentSlot.Armor:
                return "Áo Giáp";
                break;
            case EquipmentSlot.Gloves:
                return "Găng Tay";
                break;
            case EquipmentSlot.Pant:
                return "Quần";
                break;
            case EquipmentSlot.Foot:
                return "Giầy";
                break;
            case EquipmentSlot.Weapon:
                return "Vũ Khí Chính";
                break;
            case EquipmentSlot.Shield:
                return "Vũ Khí Phụ";
                break;
            case EquipmentSlot.Pedan:
                return "Dây Chuyền";
                break;
            case EquipmentSlot.Ring:
                return "Nhẫn";
                break;
            case EquipmentSlot.Rare:
                return "Bảo Vật";
                break;
            default:
                return "";
        }
    }

    public string GetTypeName(){
        switch(itemType){
            case ItemType.HPPotion:
                return "Dược Phẩm";
                break;
            case ItemType.MPPotion:
                return "Dược Phẩm";
                break;
            case ItemType.Question:
                return "Vật Phẩm Nhiệm Vụ";
                break;
            default:
                return "";
        }
    }
    public enum EquipmentSlot { Head, Armor, Gloves , Pant , Foot , Weapon , Shield , Pedan, Ring , Rare}
    public enum ItemType { Equipment , HPPotion , MPPotion , Question , Gold}
}
