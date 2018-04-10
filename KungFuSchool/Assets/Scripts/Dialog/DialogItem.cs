using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogItem : MonoBehaviour {
    #region Singleton


    public static DialogItem instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<DialogItem> ();
            }
            return _instance;
        }
    }
    static DialogItem _instance;

    void Awake ()
    {
        _instance = this;
    }

    #endregion
    private EquipmentManager eManager;
    private Item EquipingItem;
    private Item ShowingItem;
    [Header("Non Equip Item")]
    public GameObject NonEquipBox;
    public Image neIcon;
    public Text neName;
    public Text neType;
    public Text neDetail;
    [Header("Equip Item")]
    public GameObject EquipBox;
    public Text eName;
    public Text eDetail;
    public Text eDefaultTitle;
    public Text eDefaultPoint;
    public Text eDefaultUnit;
    public Text[] eOpTitles;
    public Text[] eOpPoints;
    public Text[] eOpUnits;
    [Header("Equiping Item")]
    public GameObject EquipingBox;
    public Text epName;
    public Text epDetail;
    public Text epDefaultTitle;
    public Text epDefaultPoint;
    public Text epDefaultUnit;
    public Text[] epOpTitles;
    public Text[] epOpPoints;
    public Text[] epOpUnits;

    [Header("Unequip Item")]
    public GameObject UnequipBox;
    public Text ueName;
    public Text ueDetail;
    public Text ueDefaultTitle;
    public Text ueDefaultPoint;
    public Text ueDefaultUnit;
    public Text[] ueOpTitles;
    public Text[] ueOpPoints;
    public Text[] ueOpUnits;

    public void showItem(Item item){
        ShowingItem = item;
        int slotIndex = (int)item.equipSlot;
        EquipBox.SetActive(false);
        EquipingBox.SetActive(false);
        NonEquipBox.SetActive(false);
        UnequipBox.SetActive(false);
        if ((int)item.itemType!=0)
        {
            neIcon.overrideSprite = item.icon;
            neName.text = item.name;
            neType.text = ((int)item.itemType == 1 || (int)item.itemType == 2) ? "Dược Phẩm" : "Vật Phẩm Nhiệm Vụ";
            neDetail.text = item.details;
            NonEquipBox.SetActive(true);
        }
        else
        {
            eName.text = item.name;
            eDetail.text = item.details;
            eDefaultTitle.text = item.defaultOption.title;
            eDefaultPoint.text = item.defaultPoint + "";
            eDefaultUnit.text = item.defaultOption.unit;
            for (int i = 0; i < eOpTitles.Length; i++)
            {
                if (i >= item.options.Length)
                {
                    eOpTitles[i].text = "";
                    eOpPoints[i].text = "";
                    eOpUnits[i].text = "";
                }
                else
                {
                    eOpTitles[i].text = item.options[i].title;
                    eOpPoints[i].text = item.points[i] + "";
                    eOpUnits[i].text = item.options[i].unit;
                }
            }
            EquipBox.SetActive(true);
            if (eManager == null)
            {
                eManager = EquipmentManager.instance;
            }
            if (eManager.currentEquipment[slotIndex] != null)
            {
                EquipingItem = eManager.currentEquipment[slotIndex];
            }
            if (EquipingItem == null)
            {   
                EquipingBox.SetActive(false);
                return;
            }
            epName.text = EquipingItem.name;
            epDetail.text = EquipingItem.details;
            epDefaultTitle.text = EquipingItem.defaultOption.title;
            epDefaultPoint.text = EquipingItem.defaultPoint + "";
            epDefaultUnit.text = EquipingItem.defaultOption.unit;
            for (int i = 0; i < eOpTitles.Length; i++)
            {
                if (EquipingItem.options==null || EquipingItem.options.Length == 0 || i >= EquipingItem.options.Length)
                {
                    epOpTitles[i].text = "";
                    epOpPoints[i].text = "";
                    epOpUnits[i].text = "";
                }
                else
                {
                    epOpTitles[i].text = EquipingItem.options[i].title;
                    epOpPoints[i].text = EquipingItem.points[i] + "";
                    epOpUnits[i].text = EquipingItem.options[i].unit;
                }
            }
            EquipingItem = null;
            EquipingBox.SetActive(true);
        }
    }

    public void showQuipingItem(Item item){
        ShowingItem = item;
        EquipBox.SetActive(false);
        EquipingBox.SetActive(false);
        NonEquipBox.SetActive(false);
        UnequipBox.SetActive(false);
        ueName.text = ShowingItem.name;
        ueDetail.text = ShowingItem.details;
        ueDefaultTitle.text = ShowingItem.defaultOption.title;
        ueDefaultPoint.text = ShowingItem.defaultPoint + "";
        ueDefaultUnit.text = ShowingItem.defaultOption.unit;
        for (int i = 0; i < ueOpTitles.Length; i++)
        {
            if (i >= ShowingItem.options.Length)
            {
                ueOpTitles[i].text = "";
                ueOpPoints[i].text = "";
                ueOpUnits[i].text = "";
            }
            else
            {
                ueOpTitles[i].text = ShowingItem.options[i].title;
                ueOpPoints[i].text = ShowingItem.points[i] + "";
                ueOpUnits[i].text = ShowingItem.options[i].unit;
            }
        }
        UnequipBox.SetActive(true);
    }

    public void UsingItem(){
        ShowingItem.Use();
    }

    public void UnequipItem(){
        EquipmentManager.instance.UnEquip(ShowingItem);
    }

    public void RemoveItem(){
        ShowingItem.RemoveFromInventory();
    }
}
