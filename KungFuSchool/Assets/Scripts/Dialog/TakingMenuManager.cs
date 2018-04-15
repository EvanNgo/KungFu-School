using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TakingMenuManager : MonoBehaviour {
    #region Singleton

    public static TakingMenuManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TakingMenuManager>();
            }
            return _instance;
        }
    }
    static TakingMenuManager _instance;

    void Awake()
    {
        _instance = this;
    }

    #endregion
    public GameObject talkingMenu;
    public Text btnBack;
    public Text btnQuestion;
    public Text btnTalking;
    public Text btnShop;
    private DialogueManager dialogue;
    private NPC currentNPC;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        dialogue = DialogueManager.instance;
    }
    // Use this for initialization
    public void StartTalkingMenu(NPC tempNpc)
    {
        talkingMenu.SetActive(!talkingMenu.activeSelf);
        currentNPC = tempNpc;
        btnQuestion.enabled = currentNPC.question;
        btnShop.enabled = currentNPC.shop;
    }

    public void Shop()
    {
        inventoryManager.inventoryMode = InventoryManager.InventoryMode.Shop;
        inventoryManager.currentNPC = currentNPC;
        inventoryManager.InventoryControl();
        talkingMenu.SetActive(!talkingMenu.activeSelf);
    }

    public void Back()
    {
        talkingMenu.SetActive(!talkingMenu.activeSelf);
    }

    public void Question()
    {
        Debug.Log("Question Clicked");
        talkingMenu.SetActive(!talkingMenu.activeSelf);
    }

    public void Talking()
    {
        dialogue.ShowBox(currentNPC.currentMessage);
        talkingMenu.SetActive(!talkingMenu.activeSelf);
    }
}
