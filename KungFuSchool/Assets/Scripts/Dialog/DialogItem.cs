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
    EquipmentManager eManager;
    PlayerManager playerManager;
    StatDetai statDetails;
    Item EquipingItem;
    Item ShowingItem;
    [Header("Non Equip Shop Box")]
    public GameObject NonEquipShop;
    public Image nesIcon;
    public Text nesName;
    public Text nesType;
    public Text nesDetail;
    public Text nesCount;
    public Text nesPrice;
    [Header("Inventory Box")]
    public GameObject InventoryBox;
    public Text eName;
    public Text inventItemType;
    public Text eDetail;
    public Text eDefaultTitle;
    public Text eDefaultPoint;
    public Text eDefaultUnit;
    public Text ePrice;
    public Text eOptionText;
    public Button eUseBTN;
    public Text eDeleteBTN;
    public GameObject[] InventoryBoxOption;
    [Header("Equiping Item")]
    public GameObject EquipingBox;
    public Text epName;
    public Text equipingItemType;
    public Text epDetail;
    public Text epDefaultTitle;
    public Text epDefaultPoint;
    public Text epDefaultUnit;
    public Text epPrice;
    public Text epOptionText;
    public GameObject[] EquipingBoxOption;
    [Header("Unequip Item")]
    public GameObject UnequipBox;
    public Text unequipItemType;
    public Text ueName;
    public Text ueDetail;
    public Text ueDefaultTitle;
    public Text ueDefaultPoint;
    public Text ueDefaultUnit;
    public Text ueOptionText;
    public Text uePrice;
    public GameObject[] UnequipBoxOption;
    private void Start()
    {
        playerManager = PlayerManager.instance;
        statDetails = StatDetai.instance;
    }
    public void DisableAllDialog(){
        NonEquipShop.SetActive(false);
        InventoryBox.SetActive(false);
        EquipingBox.SetActive(false);
        UnequipBox.SetActive(false);
    }
    public void ShowItemInventory(Item item,bool inShop){
        if (ShowingItem!=null && ShowingItem == item)
        {
            return;
        }
        ShowingItem = item;
        DisableAllDialog();
        if (inShop)
        {
            eUseBTN.interactable = false;
            eDeleteBTN.text = "Bán";
        }
        else
        {
            eUseBTN.interactable = true;
            eDeleteBTN.text = "Xóa";
        }
        eName.text = item.name;
        eName.color = item.GetItemColor();
        eDetail.text = item.details;

        ePrice.text = item.priceSell + "";
        InventoryBox.SetActive(true);
        if (InventoryBoxOption == null || InventoryBoxOption.Length!=9)
        {
            InventoryBoxOption = GameObject.FindGameObjectsWithTag("EquipBoxOptions");
        }
        if ((int)item.itemType!=0){
            inventItemType.text = item.GetTypeName();
            eDefaultTitle.enabled = false;
            eDefaultPoint.enabled = false;
            eDefaultUnit.enabled = false;
            eOptionText.enabled = false;
            for (int i = 0; i < InventoryBoxOption.Length; i++)
            {
                InventoryBoxOption[i].SetActive(false);
            }
            return;
        }
        inventItemType.text = item.GetEquipName();
        eDefaultTitle.enabled = true;
        eDefaultPoint.enabled = true;
        eDefaultUnit.enabled = true;
        eDefaultTitle.text = item.defaultOption.title;
        eDefaultPoint.text = item.defaultPoint + "";
        eDefaultUnit.text = item.defaultOption.unit;
        if (item.options.Length > 0)
        {
            eOptionText.enabled = true;
        }
        else
        {
            eOptionText.enabled = false;
        }
        int slotIndex = (int)item.equipSlot;
        for (int i = 0; i < InventoryBoxOption.Length; i++)
        {
            if (i >= item.options.Length)
            {
                InventoryBoxOption[i].SetActive(false);
            }
            else
            {
                InventoryBoxOption[i].SetActive(true);
                InventoryBoxOption[i].GetComponent<ItemOptionDialog>().SetText(item.options[i], item.points[i], item.GetItemColor());
            }
        }
            
        if (EquipmentManager.instance.currentEquipment[slotIndex] != null)
        {
            EquipingItem = EquipmentManager.instance.currentEquipment[slotIndex];
        }
        if (EquipingItem == null)
        {   
            return;
        }
        equipingItemType.text = EquipingItem.GetEquipName();
        epName.text = EquipingItem.name;
        epName.color = EquipingItem.GetItemColor();
        epDetail.text = EquipingItem.details;
        epDefaultTitle.text = EquipingItem.defaultOption.title;
        epDefaultPoint.text = EquipingItem.defaultPoint + "";
        epDefaultUnit.text = EquipingItem.defaultOption.unit;
        epPrice.text = item.priceSell + "";
        if (EquipingItem.options.Length > 0)
        {
            epOptionText.enabled = true;
        }
        else
        {
            epOptionText.enabled = false;
        }
        EquipingBox.SetActive(true);
        if (EquipingBoxOption == null || EquipingBoxOption.Length!=9)
        {
            EquipingBoxOption = GameObject.FindGameObjectsWithTag("EquipingBoxOption");
        }
        for (int i = 0; i < EquipingBoxOption.Length; i++)
        {
            if (i >= EquipingItem.options.Length)
            {
                EquipingBoxOption[i].SetActive(false);
            }
            else
            {
                EquipingBoxOption[i].SetActive(true);
                EquipingBoxOption[i].GetComponent<ItemOptionDialog>().SetText(EquipingItem.options[i], EquipingItem.points[i], EquipingItem.GetItemColor());
            }
        }
        EquipingItem = null;
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


    public void ShowQuipingItem(Item item){
        if (ShowingItem == item)
        {
            return;
        }
        ShowingItem = item;
        DisableAllDialog();
        ueName.text = ShowingItem.name;
        unequipItemType.text = ShowingItem.GetEquipName();
        ueName.color = item.GetItemColor();
        ueDetail.text = ShowingItem.details;
        ueDefaultTitle.text = ShowingItem.defaultOption.title;
        ueDefaultPoint.text = ShowingItem.defaultPoint + "";
        ueDefaultUnit.text = ShowingItem.defaultOption.unit;
        uePrice.text = item.priceSell * item.count + "";
        if (item.options.Length > 0)
        {
            ueOptionText.enabled = true;
        }
        else
        {
            ueOptionText.enabled = false;
        }
        UnequipBox.SetActive(true);
        if (UnequipBoxOption == null || UnequipBoxOption.Length!=9)
        {
            UnequipBoxOption = GameObject.FindGameObjectsWithTag("UnEquipBoxOption");
        }
        for (int i = 0; i < UnequipBoxOption.Length; i++)
        {
            if (i >= item.options.Length)
            {
                UnequipBoxOption[i].SetActive(false);
            }
            else
            {
                UnequipBoxOption[i].SetActive(true);
                UnequipBoxOption[i].GetComponent<ItemOptionDialog>().SetText(item.options[i], item.points[i], item.GetItemColor());
            }
        }
    }

    public void UsingItem(){
        ShowingItem.Use();
        if (ShowingItem.itemType == Item.ItemType.Equipment)
        {
            statDetails.UpdateStat();
        }
        ShowingItem = null;
    }

    public void BuyItem()
    {   
        if (playerManager.player.Gold < ShowingItem.count * ShowingItem.priceBuy)
        {
            Debug.Log("Not enought money");
            return;
        }
        ShowingItem.Buy();
        ShowingItem = null;
    }

    public void UnequipItem(){
        EquipmentManager.instance.UnEquip(ShowingItem);
        ShowingItem = null;
        statDetails.UpdateStat();

    }

    public void RemoveItem(){
        if (InventoryManager.instance.inventoryMode == InventoryManager.InventoryMode.Shop)
        {
            playerManager.player.Gold += ShowingItem.priceSell * ShowingItem.count;
            SQLiteCore.UpdatePlayer(playerManager.player);
            Inventory.instance.UpdateGold();;
        }
        ShowingItem.RemoveFromInventory();
        ShowingItem = null;
    }
}
