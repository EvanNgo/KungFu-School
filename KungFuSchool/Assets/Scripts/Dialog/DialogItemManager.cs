using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogItemManager : MonoBehaviour {
    #region Singleton

    public static DialogItemManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<DialogItemManager> ();
            }
            return _instance;
        }
    }
    static DialogItemManager _instance;

    void Awake ()
    {
        _instance = this;
    }

    #endregion
    public GameObject dialogUI;

    public void ShowItemInventory(Item item,bool inShop){
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
        DialogItem.instance.ShowItemInventory(item,inShop);
    }

    public void ShowQuipingItem(Item item){
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
        DialogItem.instance.showQuipingItem(item);
    }
        
    public void ShowNonEquipShopBox(Item item)
    {
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
        DialogItem.instance.ShowNonEquipShopBox(item);
    }

    public void CloseDialog(){
        dialogUI.SetActive(false);
    }
}
