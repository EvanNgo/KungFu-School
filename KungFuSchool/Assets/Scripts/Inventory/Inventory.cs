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
    public Text txtMoney;
    public int space = 20;
    void Start(){
        LoadCurrentItem();
    }

    public void AddItem(Item itemToAdd,GameObject itemScript){
        if (!itemToAdd.isEquipment && items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name==itemToAdd.name && items[i].details == itemToAdd.details && items[i].icon == itemToAdd.icon && items[i].count<99)
                {
                    items[i].count += 1;
                    if (SQLiteCore.UpdateItem(items[i])){
                        itemScript.SendMessage("DoInteraction");
                        return;
                    }
                }
            }
        }
        if (items.Count > space) { Debug.Log("Inventory Full!!");  return; }
        if (SQLiteCore.AddItemToInventory(itemToAdd) == -1) {
            Debug.Log("Save item ERROR");
            return;
        }
        itemToAdd.id = SQLiteCore.LastAddedItem;
        items.Add(itemToAdd);
        itemScript.SendMessage("DoInteraction");
    }

    public void LoadCurrentItem()
    {
        items = SQLiteCore.getItemInInvengory();
        if (items.Count > 0) {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isEquipment)
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


    public void unEquipmentItem(Item item,int index){
        item.isEquiping = false;
        items.Insert(index, item);
        SQLiteCore.UpdateEquipment(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke ();
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
        int index = items.IndexOf(removeItem);
        SQLiteCore.RemoveItem(removeItem.id);
        if (removeItem.isEquipment)
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
}
