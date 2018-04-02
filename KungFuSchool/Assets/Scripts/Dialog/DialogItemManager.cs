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

    public void ShowDialog(Item item){
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
        DialogItem.instance.showItem(item);
    }

    public void ShowQuipingItem(Item item){
        if (!dialogUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
        }
        DialogItem.instance.showQuipingItem(item);
    }

    public void CloseDialog(){
        dialogUI.SetActive(false);
    }
}
