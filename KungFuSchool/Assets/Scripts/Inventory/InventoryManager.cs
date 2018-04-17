using UnityEngine;

public class InventoryManager : MonoBehaviour {
    
    #region Singleton

    public static InventoryManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
            }
            return _instance;
        }
    }
    static InventoryManager _instance;

    void Awake()
    {
        _instance = this;
    }

    #endregion

    Inventory inventory;
    public NPC currentNPC;
    public InventoryMode inventoryMode;
    public GameObject inventoryUI;
    public GameObject playerInformationUI;
    public GameObject equipmentUI;
    public GameObject shopUI;
    public GameObject RegentBar;
	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            if (!inventoryUI.activeSelf)
            {
                inventoryMode = InventoryMode.Inventory;
            }
            InventoryControl();
        }
	}

    public void InventoryControl()
    {   
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        UpdateUI();
        if (!inventoryUI.activeSelf)
        {
            DialogItemManager.instance.CloseDialog();
        }
        RegentBar.SetActive(!inventoryUI.activeSelf);
        switch (inventoryMode)
        {
            case InventoryMode.Inventory:
                equipmentUI.SetActive(!equipmentUI.activeSelf);
                playerInformationUI.SetActive(!playerInformationUI.activeSelf);
                break;
            case InventoryMode.Shop:
                shopUI.SetActive(!shopUI.activeSelf);
                UpdateShop();
                break;
        }
    }

    void UpdateUI(){
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void UpdateShop()
    {
        ShopSlot[] slots = GetComponentsInChildren<ShopSlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < currentNPC.shopList.Length)
            {
                slots[i].AddItem(currentNPC.shopList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public enum InventoryMode { Inventory,Shop }
}
