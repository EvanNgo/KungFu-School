using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {
    #region Singleton


    public static EquipmentManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<EquipmentManager> ();
            }
            return _instance;
        }
    }
    static EquipmentManager _instance;

    void Awake ()
    {
        _instance = this;
    }

    #endregion
    public Image[] equipSlots;
    public Item[] currentEquipment;
    private List<Item> listCurrentItem;
    Inventory inventory;
    private void Start(){
        listCurrentItem = SQLiteCore.getEquipingitem();
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames (typeof(Item.EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].sprite = Resources.Load <Sprite> ("root_"+i);;
        }
        if (listCurrentItem.Count > 0)
        {
            for (int i = 0; i < listCurrentItem.Count; i++)
            {
                List<Option> list = SQLiteCore.getOptionItem(listCurrentItem[i].id);
                if (list.Count > 0)
                {
                    listCurrentItem[i].options = new Option[list.Count];
                    listCurrentItem[i].points = new int[list.Count];
                    for (int j = 0; j < list.Count; j++)
                    {
                        Option o = ScriptableObject.CreateInstance<Option>();
                        o.title = list[j].title;
                        o.tag = list[j].tag;
                        o.unit = list[j].unit;
                        listCurrentItem[i].options[j] = o;
                        listCurrentItem[i].points[j] = list[j].point;
                    }
                }
                else
                {
                    listCurrentItem[i].options = new Option[0];
                    listCurrentItem[i].points = new int[0];
                }
                LoadCurrentItem(listCurrentItem[i]);
            }
        }
    }

    public void EquipItem(Item item,int index){
        Item oldItem = null;
        int slotIndex = (int)item.equipSlot;
        item.isEquiping = true;
        SQLiteCore.UpdateEquipment(item);
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment [slotIndex];
            inventory.ChangeItemAt (oldItem,index);
        }
        currentEquipment[slotIndex] = item;
        equipSlots[slotIndex].overrideSprite = item.icon;
        equipSlots[slotIndex].enabled = true;
    }

    public void UnEquip(Item item){
        if (!inventory.UnEquipitem(item))
        {   
            Debug.Log("Inventory is FULL");
            return;
        }
        int slotIndex = (int)item.equipSlot;
        currentEquipment[slotIndex] = null;
        equipSlots[slotIndex].overrideSprite = Resources.Load <Sprite> ("root_"+slotIndex);;
        DialogItemManager.instance.CloseDialog();
    }

    public void LoadCurrentItem(Item item){
        int slotIndex = (int)item.equipSlot;
        currentEquipment[slotIndex] = item;
        equipSlots[slotIndex].overrideSprite = item.icon;
        equipSlots[slotIndex].enabled = true;
    }

    public void EquipingItemClick(int index){
        if (currentEquipment[index] == null)
        {
            return;
        }
        DialogItemManager.instance.ShowQuipingItem(currentEquipment[index]);
    }
}
