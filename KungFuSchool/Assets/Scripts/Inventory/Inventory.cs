using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject inventoryUI;
    public GameObject regentUI;
    public DialogItem dialogItem;
    public GameObject dialogItemUI;
    private GameObject dialogBox;
    private EquipmentManager equipManager;
    public List<Item> items = new List<Item>();
    public Image[] slotIcon = new Image[20];
    public Image[] slectedUI = new Image[20];
    public Text[] itemOptionTitle = new Text[9];
    public Text[] itemOptionPoint = new Text[9];
    public Text[] itemOptionUnit = new Text[9];
    public Text[] txtCounts = new Text[20];
    public Text txtMoney;
    public int space = 20;
    int selectedPosition = -1;
    void Start(){
        LoadCurrentItem();
        for (int i = 0; i < space ; i++)
        {
            slectedUI[i].enabled = false;
            if (i >= items.Count)
            {
                slotIcon[i].enabled = false;
                txtCounts[i].enabled = false;
            }
            else
            {
                if (!items[i].isEquipment && items[i].isStacking)
                {
                    txtCounts[i].text = items[i].count + "";
                    txtCounts[i].enabled = true;
                }
                else
                {
                    txtCounts[i].enabled = false;
                }
            }
        }
        dialogBox = GameObject.FindObjectOfType<DialogueManager>().dialogBox;
        equipManager = FindObjectOfType<EquipmentManager>();
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
            if (!inventoryUI.activeSelf)
            {   
                dialogItem.OnClick();
                if (selectedPosition != -1)
                {
                    slectedUI[selectedPosition].enabled = false;
                    selectedPosition = -1;
                }
            }
        }
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
                        txtCounts[i].text = items[i].count + "";
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
        slotIcon[items.Count - 1].overrideSprite = itemToAdd.icon;
        slotIcon[items.Count - 1].enabled = true;
        if (!itemToAdd.isEquipment && itemToAdd.isStacking)
        {
            txtCounts[items.Count - 1].text = itemToAdd.count + "";
            txtCounts[items.Count - 1].enabled = true;

        }
        itemScript.SendMessage("DoInteraction");
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
    public void CloseInventory(){
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        regentUI.SetActive(!inventoryUI.activeSelf);
        if (!inventoryUI.activeSelf)
        {   
            dialogItem.OnClick();
            if (selectedPosition != -1)
            {
                slectedUI[selectedPosition].enabled = false;
                selectedPosition = -1;
            }
        }
    }

    public void ItemClick(int id)
    {   if (id == selectedPosition)
        {
            return;
        }
        if (selectedPosition != -1)
        {
            slectedUI[selectedPosition].enabled = false;
        }
        selectedPosition = id;
        slectedUI[selectedPosition].enabled = true;
        dialogItem.showItem(items[selectedPosition]);
    }
    public void EquipItem(){
        dialogItem.OnClick();
        items[selectedPosition].isEquiping = true;
        SQLiteCore.UpdateEquipment(items[selectedPosition]);
        if (equipManager.EquipItem(items[selectedPosition]))
        {
            items[selectedPosition] = equipManager.lastItem;
            items[selectedPosition].isEquiping = false;
            slotIcon[selectedPosition].overrideSprite = equipManager.lastItem.icon;
            slectedUI[selectedPosition].enabled = false;
            SQLiteCore.UpdateEquipment(items[selectedPosition]);
            equipManager.lastItem = null;
            selectedPosition = -1;
            return;
        }
        else
        {
            if (dialogItemUI.activeSelf)
            {
                dialogItemUI.SetActive(false);
            }
            for (int i = selectedPosition; i < items.Count - 1; i++)
            {
                items[i] = items[i + 1];
                slotIcon[i].overrideSprite = items[i + 1].icon;
            }
            slotIcon[items.Count - 1].overrideSprite = null;
            slotIcon[items.Count - 1].enabled = false;
            slectedUI[selectedPosition].enabled = false;
            items.RemoveAt(items.Count - 1);
            selectedPosition = -1;
        }
    }
    public void RemoveItem(){
        if (items[selectedPosition].isEquipment)
        {
            if (items[selectedPosition].options.Length > 0)
            {
                if (SQLiteCore.RemoveOptionItem(items[selectedPosition].id))
                {
                    if (!SQLiteCore.RemoveItem(items[selectedPosition].id)){
                        for (int i = 0; i < items[selectedPosition].options.Length; i++)
                        {
                            SQLiteCore.AddItemOption(items[selectedPosition].id, items[selectedPosition].points[i], items[selectedPosition].options[i]);
                        }
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (!SQLiteCore.RemoveItem(items[selectedPosition].id)){
                    return;
                }
            }
        }
        else
        {
            if (!SQLiteCore.RemoveItem(items[selectedPosition].id))
            {
                return;
            }
        }
        if (dialogItemUI.activeSelf)
        {
            dialogItemUI.SetActive(false);
        }
        for (int i = selectedPosition; i < items.Count-1; i++)
        {
            items[i] = items[i + 1];
            slotIcon[i].overrideSprite = items[i + 1].icon;
        }
        slotIcon[items.Count - 1].overrideSprite = null;
        slotIcon[items.Count - 1].enabled = false;
        slectedUI[selectedPosition].enabled = false;
        items.RemoveAt(items.Count - 1);
        selectedPosition = -1;
    }
}
