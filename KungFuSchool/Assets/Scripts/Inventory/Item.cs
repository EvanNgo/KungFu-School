using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Items", menuName = "Inventory/Basic Item")]
public class Item : ScriptableObject {

    public int id;
    new public string name = "New Item";
    public string details;
    public Sprite icon = null;
    public bool isEquiping = false;
    public bool isStacking = false;
    public int count = 1;
    public int priceBuy = 0;
    public int priceSell = 0;
    public Option defaultOption;
    public int defaultPoint;
    public Option[] options;
    public int[] points;
    public SkinnedMeshRenderer prefab;
    public EquipmentSlot equipSlot;
    public ItemType itemType;
    public bool showInInventory = true;
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
    public enum EquipmentSlot { Head, Armor, Gloves , Pant , Foot , Weapon , Shield , Pedan, Ring , Rare}
    public enum ItemType { Equipment , HPPotion , MPPotion , Question , Gold}
}
