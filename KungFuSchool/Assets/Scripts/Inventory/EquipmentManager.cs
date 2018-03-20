using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {
    public Image SlotHead;
    public Image SlotArmor;
    public Image SlotGloves;
    public Image SlotBoot;
    public Image SlotPant;
    public Image SlotWepont;
    public Image SlotShield;
    public Image SlotPedan;
    public Image SlotRing;
    public Image SlotRare;
    public Item lastItem;
    public Inventory inventory;
    public Item[] equipings;
    private void Start(){
        equipings = new Item[10];
        inventory = FindObjectOfType<Inventory>();
        if (equipings != null && equipings.Length > 0)
        {
            for (int i = 0; i < equipings.Length; i++)
            {   
                if (equipings[i] != null)
                {
                    EquipItem(equipings[i]);
                }
            }
        }
    }

    public bool EquipItem(Item item){
        switch (item.equipSlot)
        {
            case Item.EquipmentSlot.Head:
                if (equipings[0] != null)
                {
                    lastItem = equipings[0];
                }
                equipings[0] = item;
                break;
            case Item.EquipmentSlot.Armor:
                if (equipings[1] != null)
                {
                    lastItem = equipings[1];
                }
                equipings[1] = item;
                break;
            case Item.EquipmentSlot.Gloves:
                if (equipings[2] != null)
                {
                    lastItem = equipings[2];
                }
                equipings[2] = item;
                break;
            case Item.EquipmentSlot.Foot:
                if (equipings[3] != null)
                {
                    lastItem = equipings[3];
                }
                equipings[3] = item;
                break;
            case Item.EquipmentSlot.Pant:
                if (equipings[4] != null)
                {
                    lastItem = equipings[4];
                }
                equipings[4] = item;
                break;
            case Item.EquipmentSlot.Weapon:
                if (equipings[5] != null)
                {
                    lastItem = equipings[5];
                }
                equipings[5]  = item;
                break;
            case Item.EquipmentSlot.Shield:
                if (equipings[6] != null)
                {
                    lastItem = equipings[6];
                }
                equipings[6]  = item;
                break;
            case Item.EquipmentSlot.Pedan:
                if (equipings[7] != null)
                {
                    lastItem = equipings[7];
                }
                equipings[7]  = item;
                break;
            case Item.EquipmentSlot.Ring:
                if (equipings[8] != null)
                {
                    lastItem = equipings[8];
                }
                equipings[8]  = item;
                break;
            case Item.EquipmentSlot.Rare:
                if (equipings[9] != null)
                {
                    lastItem = equipings[9];
                }
                equipings[9]  = item;
                break;
        }
        if (lastItem != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadCurrentItem(Item item){
        switch (item.equipSlot)
        {
            case Item.EquipmentSlot.Head:
                equipings[0] = item;
                SlotHead.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Armor:
                equipings[1] = item;
                SlotArmor.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Gloves:
                equipings[2] = item;
                SlotGloves.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Foot:
                equipings[3] = item;
                SlotBoot.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Pant:
                equipings[4] = item;
                SlotPant.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Weapon:
                equipings[5] = item;
                SlotWepont.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Shield:
                equipings[6] = item;
                SlotShield.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Pedan:
                equipings[7] = item;
                SlotPedan.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Ring:
                equipings[8] = item;
                SlotRing.overrideSprite = item.icon;
                break;
            case Item.EquipmentSlot.Rare:
                equipings[9] = item;
                SlotRare.overrideSprite = item.icon;
                break;
        }
    }
}
