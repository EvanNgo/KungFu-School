﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    #region Singleton

    public static Inventory instance;

    void Awake ()
    {
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public List<Item> items = new List<Item>();
    public Text txtGold;
    private int Gold;
    public int space = 20;
    void Start(){
        LoadCurrentItem();
        Gold = PlayerManager.instance.player.Gold;
        setTextGold();
    }

    private void setTextGold(){
        if (Gold > 9999)
        {
            int van = Gold/10000;
            int luong = Gold % 10000;
            txtGold.text = van + " vạn "+ luong + " lượng";
        }
        else
        {
            txtGold.text = Gold + " lượng";
        }
    }
    public void AddItem(Item itemToAdd,GameObject itemScript){
        if ((int)itemToAdd.itemType!=0 && items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name==itemToAdd.name && items[i].icon == itemToAdd.icon && items[i].count < 99)
                {
                    items[i].count += 1;
                    if (SQLiteCore.UpdateItem(items[i])){
                        itemScript.SendMessage("DoInteraction");
                        return;
                    }
                }
            }
        }
        if (items.Count >= space) { Debug.Log("Inventory Full!!");  return; }
        if (SQLiteCore.AddItemToInventory(itemToAdd) == -1) {
            Debug.Log("Save item ERROR");
            return;
        }
        itemToAdd.id = SQLiteCore.LastAddedItem;
        items.Add(itemToAdd);
        itemScript.SendMessage("DoInteraction");
    }

    public void BuyItem(Item itemBuy)
    {
        if ((int)itemBuy.itemType != 0 && items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == itemBuy.name && items[i].icon == itemBuy.icon && items[i].count < 99)
                {
                    if(items[i].count + itemBuy.count < 100)
                    {
                        items[i].count += itemBuy.count;
                        PlayerManager.instance.player.Gold = PlayerManager.instance.player.Gold - (itemBuy.count * itemBuy.priceBuy);
                        Gold = PlayerManager.instance.player.Gold;
                        setTextGold();
                        SQLiteCore.UpdatePlayer(PlayerManager.instance.player);
                        SQLiteCore.UpdateItem(items[i]);
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();
                        return;
                    }
                    else
                    {
                        if (items.Count >= space) { Debug.Log("Inventory Full!!"); return; }
                        itemBuy.count = itemBuy.count - (99 - items[i].count);
                        items[i].count = 99;
                        PlayerManager.instance.player.Gold = PlayerManager.instance.player.Gold - (itemBuy.count * itemBuy.priceBuy);
                        Gold = PlayerManager.instance.player.Gold;
                        setTextGold();
                        SQLiteCore.UpdatePlayer(PlayerManager.instance.player);
                        SQLiteCore.UpdateItem(items[i]);
                        if (SQLiteCore.AddItemToInventory(itemBuy) == -1)
                        {
                            Debug.Log("Save item ERROR");
                            return;
                        }
                        itemBuy.id = SQLiteCore.LastAddedItem;
                        items.Add(itemBuy);
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();
                        return;
                    }
                }
            }
        }
        if (items.Count >= space) { Debug.Log("Inventory Full!!"); return; }
        PlayerManager.instance.player.Gold = PlayerManager.instance.player.Gold - (itemBuy.count * itemBuy.priceBuy);
        Gold = PlayerManager.instance.player.Gold;
        setTextGold();
        SQLiteCore.UpdatePlayer(PlayerManager.instance.player);
        if (SQLiteCore.AddItemToInventory(itemBuy) == -1)
        {
            Debug.Log("Save item ERROR");
            return;
        }
        itemBuy.id = SQLiteCore.LastAddedItem;
        items.Add(itemBuy);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void LoadCurrentItem()
    {
        items = SQLiteCore.getItemInInvengory();
        if (items.Count > 0) {
            for (int i = 0; i < items.Count; i++)
            {
                if ((int)items[i].itemType==0)
                {
                    List<Option> list = SQLiteCore.getOptionItem(items[i].id);
                    if (list.Count > 0)
                    {
                        items[i].options = new Option[list.Count];
                        items[i].points = new int[list.Count];
                        for (int j = 0; j < list.Count; j++)
                        {
                            Option o = ScriptableObject.CreateInstance<Option>();
                            o.title = list[j].title;
                            o.tag = list[j].tag;
                            o.unit = list[j].unit;
                            items[i].options[j] = o;
                            items[i].points[j] = list[j].point;
                        }
                    }
                    else
                    {
                        items[i].options = new Option[0];
                        items[i].points = new int[0];
                    }
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke ();
                }
            }
        }
    }


    public void ChangeItemAt(Item item,int index){
        item.isEquiping = false;
        items.Insert(index, item);
        SQLiteCore.UpdateEquipment(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke ();
    }

    public bool UnEquipitem(Item item){
        if (items.Count >= space)
        {
            return false;
        }
        item.isEquiping = false;
        items.Add(item);
        SQLiteCore.UpdateEquipment(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke ();
        return true;
    }
        
    public int EquipItem(Item equipItem){
        int index = items.IndexOf(equipItem);
        equipItem.isEquiping = true;
        items.Remove(equipItem);
        SQLiteCore.UpdateEquipment(equipItem);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke ();
        return index;
    }
        
    public void Remove(Item removeItem){
        SQLiteCore.RemoveItem(removeItem.id);
        if ((int)removeItem.itemType==0)
        {
            if (removeItem.options.Length > 0)
            {   
                SQLiteCore.RemoveOptionItem(removeItem.id);
            }
        }
        items.Remove(removeItem);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke ();
    }
    public Item LookUpPotion(int itemType){
        for (int i = items.Count-1; i > 0; i--)
        {
            if ((int)items[i].itemType == itemType)
            {
                return items[i];
            }
        }
        return null;
    }
    public bool UsePotion(int itemPotion){
        if (LookUpPotion(itemPotion) != null)
        {
            Item usingItem = LookUpPotion(itemPotion);
            if (usingItem.count > 1)
            {
                usingItem.count--;
                SQLiteCore.UpdateItem(usingItem);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke ();
            }
            else
            {
                Remove(usingItem);
            }
            return true;
        }
        return false;
    }

    public void TakeGold(Item itemGold,GameObject itemScript){
        Gold = Gold + itemGold.priceSell;
        PlayerManager.instance.player.Gold = Gold;
        SQLiteCore.UpdatePlayer(PlayerManager.instance.player);
        setTextGold();
        itemScript.SendMessage("DoInteraction");
    }   
}
