using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject inventoryUI;
    public GameObject regentUI;
    public GameObject dialogBox;
    public List<Item> items = new List<Item>();
    public Image[] slotIcon = new Image[20];
    public Image[] slectedUI = new Image[20];
    public Image[] slectedButtonUI = new Image[3];
    public Text[] itemOptionTitle = new Text[9];
    public Text[] itemOptionPoint = new Text[9];
    public Text[] itemOptionUnit = new Text[9];
    public Text manaCount;
    public Text healthCount;
    public Text moneyCount;
    public Text itemName;
    public Text itemDetail;
    public Text itemDefaultTitle;
    public Text itemDefaultPoint;
    public Text itemDefaultUnit;
    public int space = 20;
    int selectedPosition = 0;
    int selectedButton = -1;
    void Start(){
        for (int i = 0; i < space ; i++)
        {
            slectedUI[i].enabled = false;
            if (i >= items.Count)
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
        dialogBox = GameObject.FindObjectOfType<DialogueManager>().dialogBox;
    }

    void Update(){
        if (dialogBox.activeSelf)
        {
            return;
        }
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            regentUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf)
            {
                setItemDetaiToTable();
                if (selectedButton != -1)
                {
                    slectedButtonUI[selectedButton].enabled = false;
                    selectedButton = -1;
                }
                slectedUI[selectedPosition].enabled = true;
            }
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
            setItemDetaiToTable();
        }
        if (Input.GetButtonDown("MoveSelectDown") && selectedButton == -1){
            
            if (selectedPosition == 16 || selectedPosition == 17 || selectedPosition == 18 || selectedPosition == 19)
            {
                return;
            }
            slectedUI[selectedPosition].enabled = false;
            selectedPosition += 4;
            slectedUI[selectedPosition].enabled = true;
            setItemDetaiToTable();
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
                setItemDetaiToTable();
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
                setItemDetaiToTable();
            }
            else {
                if (selectedButton == 2) { return; }
                slectedButtonUI[selectedButton].enabled = false;
                selectedButton += 1;
                slectedButtonUI[selectedButton].enabled = true;
            }
        }
        if (Input.GetButtonDown("Select")) {
            SlotClicked();
        }
    }

    public void AddItem(Item itemToAdd,GameObject itemScript){
        if (items.Count > space) { Debug.Log("Inventory Full!!");  return; }
        if (SQLiteCore.AddItemToInventory(itemToAdd) == -1) {
            Debug.Log("Save item ERROR");
            return;
        }
        itemToAdd.id = SQLiteCore.LastAddedItem;
        items.Add(itemToAdd);
        slotIcon[items.Count - 1].overrideSprite = itemToAdd.icon;
        slotIcon[items.Count - 1].enabled = true;
        itemScript.SendMessage("DoInteraction");
    }

    public void RemoveItem(){
        items.RemoveAt(selectedPosition);
        slotIcon[selectedPosition].overrideSprite = null;
        slotIcon[selectedPosition].enabled = false;
    }

    public void SlotClicked() {
        if (selectedButton == -1 && items.Count > selectedPosition) {
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
            //Delete
            if (SQLiteCore.RemoveItem(items[selectedPosition].id))
            {
                SQLiteCore.RemoveOptionItem(items[selectedPosition].id);
                for (int i = selectedPosition; i < items.Count - 1; i++)
                {
                    items[i] = items[i + 1];
                    slotIcon[i].overrideSprite = items[i].icon;
                }
                items.RemoveAt(items.Count - 1);
                slotIcon[items.Count].sprite = null;
                slotIcon[items.Count].enabled = false;
                selectedPosition -= 1;
                slectedButtonUI[selectedButton].enabled = false;
                selectedButton = -1;
                slectedUI[selectedPosition].enabled = true;
                setItemDetaiToTable();
            }
            return;
        }
        if (selectedButton == 2)
        {
            slectedButtonUI[selectedButton].enabled = false;
            selectedButton = -1;
            slectedUI[selectedPosition].enabled = true;
            return;
        }
    }

    public void setItemDetaiToTable() {
        if (selectedPosition >= items.Count) {
            itemName.text = "";
            itemDefaultTitle.text = "";
            itemDefaultPoint.text = "";
            itemDefaultUnit.text = "";
            itemDetail.text = "";
            for (int i = 0; i < itemOptionTitle.Length; i++)
            {
                itemOptionTitle[i].text = "";
                itemOptionPoint[i].text = "";
                itemOptionUnit[i].text = "";
            }
            return;
        }
        itemName.text = items[selectedPosition].name;
        itemDetail.text = items[selectedPosition].details;
        if (items[selectedPosition].equipSlot != Item.EquipmentSlot.PotionHP && items[selectedPosition].equipSlot != Item.EquipmentSlot.PotionMP && items[selectedPosition].equipSlot != Item.EquipmentSlot.Question)
        {
            itemDefaultTitle.text = items[selectedPosition].defaultOption.title;
            itemDefaultPoint.text = items[selectedPosition].defaultPoint + "";
            itemDefaultUnit.text = items[selectedPosition].defaultOption.unit;
            for (int i = 0; i < itemOptionTitle.Length; i++)
            {
                if (i >= items[selectedPosition].options.Length)
                {
                    itemOptionTitle[i].text = "";
                    itemOptionPoint[i].text = "";
                    itemOptionUnit[i].text = "";
                }
                else
                {
                    itemOptionTitle[i].text = items[selectedPosition].options[i].title;
                    itemOptionPoint[i].text = items[selectedPosition].points[i] + "";
                    itemOptionUnit[i].text = items[selectedPosition].options[i].unit;
                }
            }
        }
        else
        {
            itemDefaultTitle.text = "";
            itemDefaultPoint.text = "";
            itemDefaultUnit.text = "";
            for (int i = 0; i < itemOptionTitle.Length; i++)
            {
                itemOptionTitle[i].text = "";
                itemOptionPoint[i].text = "";
                itemOptionUnit[i].text = "";
            }
        }
    }

    public void LoadCurrentItem()
    {
        items = SQLiteCore.getItemInInvengory();
        if (items.Count > 0) {
            for (int i = 0; i < items.Count; i++)
            {
                slotIcon[i].overrideSprite = items[i].icon;
                slotIcon[i].enabled = true;
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
                }
            }
        }
    }
}
