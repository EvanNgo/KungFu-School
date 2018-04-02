using UnityEngine;

public class InventoryManager : MonoBehaviour {
    Inventory inventory;
    public GameObject inventoryUI;
    public GameObject inforBarUI;
	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
            if (!inventoryUI.activeSelf)
            {
                DialogItemManager.instance.CloseDialog();
            }
            inforBarUI.SetActive(!inventoryUI.activeSelf);
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
}
