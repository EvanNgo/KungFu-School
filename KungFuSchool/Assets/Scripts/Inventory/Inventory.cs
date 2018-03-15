using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject inventoryUI;
    public GameObject[] inventory = new GameObject[20];
    public List<Item> items = new List<Item>();
    public Image[] slotButton = new Image[20];
    public Image[] slotIcon = new Image[20];
    public Image[] slectedUI = new Image[20];
    public Image[] slectedButtonUI = new Image[4];
    int selectedPosition = 0;
    int selectedButton = -1;
    void Start(){
        for (int i = 0; i < inventory.Length; i++)
        {
            slectedUI[i].enabled = false;
            if (inventory[i] == null)
            {
                slotIcon[i].enabled = false;
            }
        }
        for (int i = 0; i < slectedButtonUI.Length; i++)
        {
            slectedButtonUI[i].enabled = false;
        }
        slectedUI[0].enabled = true;
        LoadCurrentItem();
    }

    void Update(){
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (!inventoryUI.activeSelf) {
            return;
        }
        if (Input.GetButtonDown("MoveSelectUp") && selectedButton == -1) {
            if (selectedPosition == 0 || selectedPosition == 1 || selectedPosition == 2 || selectedPosition == 3)
            {
                return;
            }
            slectedUI[selectedPosition].enabled = false;
            selectedPosition -= 4;
            slectedUI[selectedPosition].enabled = true;
        }
        if (Input.GetButtonDown("MoveSelectDown") && selectedButton == -1){
            
            if (selectedPosition == 16 || selectedPosition == 17 || selectedPosition == 18 || selectedPosition == 19)
            {
                return;
            }
            slectedUI[selectedPosition].enabled = false;
            selectedPosition += 4;
            slectedUI[selectedPosition].enabled = true;
        }
        if (Input.GetButtonDown("MoveSelectLeft")){
            if (selectedButton == -1)
            {
                if (selectedPosition == 0 || selectedPosition == 4 || selectedPosition == 8 || selectedPosition == 12 || selectedPosition == 16)
                {
                    return;
                }
                slectedUI[selectedPosition].enabled = false;
                selectedPosition -= 1;
                slectedUI[selectedPosition].enabled = true;
            }
            else
            {
                if (selectedButton == 0) { return; }
                slectedButtonUI[selectedButton].enabled = false;
                selectedButton -= 1;
                slectedButtonUI[selectedButton].enabled = true;
            }
            
        }
        if (Input.GetButtonDown("MoveSelectRight")){
            if (selectedButton == -1)
            {
                if (selectedPosition == 3 || selectedPosition == 7 || selectedPosition == 11 || selectedPosition == 15 || selectedPosition == 19)
                {
                    return;
                }
                slectedUI[selectedPosition].enabled = false;
                selectedPosition += 1;
                slectedUI[selectedPosition].enabled = true;
            }
            else {
                if (selectedButton == 3) { return; }
                slectedButtonUI[selectedButton].enabled = false;
                selectedButton += 1;
                slectedButtonUI[selectedButton].enabled = true;
            }
               
        }
        if (Input.GetButtonDown("Select")) {
            SlotClicked();
        }
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

    public void SlotClicked() {
        if (selectedButton == -1 && inventory[selectedPosition] != null) {
            slectedUI[selectedPosition].enabled = false;
            selectedButton = 0;
            slectedButtonUI[selectedButton].enabled = true;
            return;
        }
        if (selectedButton == 0)
        {
            //Equip
            slectedButtonUI[selectedButton].enabled = false;
            selectedButton = -1;
            slectedUI[selectedPosition].enabled = true;
        }
        if (selectedButton == 1)
        {
            items[selectedPosition].Use();
            return;
        }
        if (selectedButton == 2)
        {
            //Delete
            selectedPosition -= 1;
            slectedButtonUI[selectedButton].enabled = false;
            selectedButton = -1;
            slectedUI[selectedPosition].enabled = true;
            return;
        }
        if (selectedButton == 3)
        {
            slectedButtonUI[selectedButton].enabled = false;
            selectedButton = -1;
            slectedUI[selectedPosition].enabled = true;
            return;
        }
    }
}
