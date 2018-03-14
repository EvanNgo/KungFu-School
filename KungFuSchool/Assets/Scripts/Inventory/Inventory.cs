using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject[] inventory = new GameObject[20];
    public List<Item> items = new List<Item>();
    public Image[] slotButton = new Image[20];
    public Image[] slotIcon = new Image[20];
    void Start(){
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                slotIcon[i].enabled = false;
            }
        }
        LoadCurrentItem();
    }

    public void LoadCurrentItem(){
        items = SQLiteCore.getItemInInvengory();
        for (int i = 0; i < items.Count; i++)
        {
            GameObject myGameObject = new GameObject();
            myGameObject.SetActive(false);
            myGameObject.AddComponent<SpriteRenderer>();
            myGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(items[i].itemIcon);
            myGameObject.name = items[i].itemName;
            inventory[i] = myGameObject;
            slotIcon[i].overrideSprite = myGameObject.GetComponent<SpriteRenderer>().sprite;
            slotIcon[i].enabled = true;
        }
    }

    public void AddItem(GameObject itemToAdd){
        bool itemAdded = false;
        for (int i = 0; i < inventory.Length ; i++)
        {  
            if (inventory[i] == null)
            {
                Item mItem = new Item();
                mItem.itemName = itemToAdd.name;
                mItem.itemIcon = itemToAdd.GetComponent<SpriteRenderer>().sprite.name;
                bool adding = SQLiteCore.AddItemToInventory(mItem);
                if (adding)
                {      
                    mItem.id = SQLiteCore.LastAddedItem;
                    items.Add(mItem);
                    inventory[i] = itemToAdd;
                    slotIcon[i].overrideSprite = itemToAdd.GetComponent<SpriteRenderer>().sprite;
                    slotIcon[i].enabled = true;
                    itemAdded = true;
                    itemToAdd.SendMessage("DoInteraction");
                }
                return;
            }
        }
        if (!itemAdded)
        {
            Debug.Log("Inventory Full!!");
        }
    }

    public void RemoveItem(GameObject itemToRemove){
        for (int i = 0; i < inventory.Length ; i++)
        {
            if (inventory[i] == itemToRemove)
            {
                inventory[i] = null;
                return;
            }
        }
    }
}
