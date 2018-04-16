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
    private PlayerManager playerManager;
    private Item EquipingItem;
    private Item ShowingItem;
    [Header("Non Equip Shop Box")]
    public GameObject NonEquipShop;
    public Image nesIcon;
    public Text nesName;
    public Text nesType;
    public Text nesDetail;
    public Text nesCount;
    public Text nesPrice;
    [Header("Non Equip Item")]
    public GameObject NonEquipBox;
    public Image neIcon;
    public Text neName;
    public Text neType;
    public Text neDetail;
    public Text nePrice;
    public Button neUseBTN;
    public Text neDeleteBTN;
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
    public Text ePrice;
    public Button eUseBTN;
    public Text eDeleteBTN;
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
    public Text epPrice;
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
    public Text uePrice;
    private void Start()
    {
        playerManager = PlayerManager.instance;
    }
    public void DisableAllDialog(){
        NonEquipShop.SetActive(false);
        EquipBox.SetActive(false);
        EquipingBox.SetActive(false);
        NonEquipBox.SetActive(false);
        UnequipBox.SetActive(false);
    }
    public void ShowItemInShop(Item item){
        DisableAllDialog();
        ShowingItem = item;
        if ((int)item.itemType != 0)
        {
            neIcon.overrideSprite = item.icon;
            neName.text = item.name;
            neType.text = ((int)item.itemType == 1 || (int)item.itemType == 2) ? "Dược Phẩm" : "Vật Phẩm Nhiệm Vụ";
            neDetail.text = item.details;
            nePrice.text = item.priceSell * item.count + " Lượng";
            NonEquipBox.SetActive(true);
            neUseBTN.interactable = false;
            neDeleteBTN.text = "Bán";
            NonEquipBox.SetActive(true);
        }
        else
        {
            eName.text = item.name;
            eName.color = item.GetItemColor();
            eDetail.text = item.details;
            eDefaultTitle.text = item.defaultOption.title;
            eDefaultPoint.text = item.defaultPoint + "";
            eDefaultUnit.text = item.defaultOption.unit;
            ePrice.text = item.priceSell * item.count + " Lượng";
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
                    eOpTitles[i].color = item.GetItemColor();
                    eOpPoints[i].color = item.GetItemColor();
                    eOpUnits[i].color = item.GetItemColor();
                }
            }
            eUseBTN.interactable = false;
            eDeleteBTN.text = "Bán";
            EquipBox.SetActive(true);
        }
    }
    public void showItem(Item item){
        ShowingItem = item;
        int slotIndex = (int)item.equipSlot;
        DisableAllDialog();
        if ((int)item.itemType!=0)
        {
            neIcon.overrideSprite = item.icon;
            neName.text = item.name;
            neName.color = Color.white;
            neType.text = ((int)item.itemType == 1 || (int)item.itemType == 2) ? "Dược Phẩm" : "Vật Phẩm Nhiệm Vụ";
            neDetail.text = item.details;
            nePrice.text = item.priceSell * item.count + " Lượng";
            neUseBTN.interactable = true;
            neDeleteBTN.text = "Xóa";
            NonEquipBox.SetActive(true);
        }
        else
        {
            eUseBTN.interactable = true;
            eDeleteBTN.text = "Xóa";
            eName.text = item.name;
            eName.color = item.GetItemColor();
            eDetail.text = item.details;
            eDefaultTitle.text = item.defaultOption.title;
            eDefaultPoint.text = item.defaultPoint + "";
            eDefaultUnit.text = item.defaultOption.unit;
            ePrice.text = item.priceSell + " Lượng";
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
                    eOpTitles[i].color = item.GetItemColor();
                    eOpPoints[i].color = item.GetItemColor();
                    eOpUnits[i].color = item.GetItemColor();
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
                return;
            }
            epName.text = EquipingItem.name;
            epName.color = EquipingItem.GetItemColor();
            epDetail.text = EquipingItem.details;
            epDefaultTitle.text = EquipingItem.defaultOption.title;
            epDefaultPoint.text = EquipingItem.defaultPoint + "";
            epDefaultUnit.text = EquipingItem.defaultOption.unit;
            epPrice.text = item.priceSell + " Lượng";
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
                    epOpTitles[i].color = EquipingItem.GetItemColor();
                    epOpPoints[i].color = EquipingItem.GetItemColor();
                    epOpUnits[i].color = EquipingItem.GetItemColor();
                }
            }
            EquipingItem = null;
            EquipingBox.SetActive(true);
        }
    }

    public void ShowNonEquipShopBox(Item item)
    {
        DisableAllDialog();
        item.count = 1;
        if(PlayerManager.instance.player.Gold >= item.priceBuy)
        {
            item.count = 1;
        }
        else
        {
            item.count = 0;
        }
        ShowingItem = item;
        nesIcon.sprite = item.icon;
        nesName.text = item.name;
        nesType.text = ((int)item.itemType == 1 || (int)item.itemType == 2) ? "Dược Phẩm" : "Vật Phẩm Nhiệm Vụ";
        nesDetail.text = item.details;
        nesCount.text = item.count + "";
        nesPrice.text = item.count * item.priceBuy + "";
        NonEquipShop.SetActive(true);
    }
    public void btnPlus()
    {
        if (ShowingItem.count >= 99)
        {
            ShowingItem.count = 1;
        }
        else
        {
            if(PlayerManager.instance.player.Gold < (ShowingItem.count+1) * ShowingItem.priceBuy)
            {
                return;
            }
            ShowingItem.count++;
        }
        nesCount.text = ShowingItem.count + "";
        nesPrice.text = ShowingItem.count * ShowingItem.priceBuy + "";
    }
    public void btnRemove()
    {
        if (ShowingItem.count <= 1)
        {
            if (PlayerManager.instance.player.Gold >= 99 * ShowingItem.priceBuy)
            {
                ShowingItem.count = 99;
            }
            else
            {
                ShowingItem.count = (int)PlayerManager.instance.player.Gold / ShowingItem.priceBuy;
            }
        }
        else
        {
            ShowingItem.count--;
        }
        nesCount.text = ShowingItem.count + "";
        nesPrice.text = ShowingItem.count * ShowingItem.priceBuy + "";
    }


    public void showQuipingItem(Item item){
        ShowingItem = item;
        DisableAllDialog();
        ueName.text = ShowingItem.name;
        ueName.color = item.GetItemColor();
        ueDetail.text = ShowingItem.details;
        ueDefaultTitle.text = ShowingItem.defaultOption.title;
        ueDefaultPoint.text = ShowingItem.defaultPoint + "";
        ueDefaultUnit.text = ShowingItem.defaultOption.unit;
        uePrice.text = item.priceSell * item.count + " Lượng";
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
                ueOpTitles[i].color = item.GetItemColor();
                ueOpPoints[i].color = item.GetItemColor();
                ueOpUnits[i].color = item.GetItemColor();
            }
        }
        UnequipBox.SetActive(true);
    }

    public void UsingItem(){
        ShowingItem.Use();
    }

    public void BuyItem()
    {   
        if (playerManager.player.Gold < ShowingItem.count * ShowingItem.priceBuy)
        {
            Debug.Log("Not enought money");
            return;
        }
        ShowingItem.Buy();
    }

    public void UnequipItem(){
        EquipmentManager.instance.UnEquip(ShowingItem);
    }

    public void RemoveItem(){
        if (InventoryManager.instance.inventoryMode == InventoryManager.InventoryMode.Shop)
        {
            playerManager.player.Gold += ShowingItem.priceSell * ShowingItem.count;
            SQLiteCore.UpdatePlayer(playerManager.player);
            Inventory.instance.UpdateGold();;
        }
        ShowingItem.RemoveFromInventory();
    }
}
