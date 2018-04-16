using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {
    public string npcName;
    public GameObject[] itemlist;
    public InteractionObject[] basicItems;
    public Item[] shopList;
    public Text txtname;
    public bool question;
    public bool shop;
    public string[] currentMessage;
    TakingMenuManager takingMenuManager;
    // Use this for initialization
    private void Start()
    {
        takingMenuManager = TakingMenuManager.instance;
        txtname.text = npcName;
        shopList = new Item[itemlist.Length];
        basicItems = new InteractionObject[itemlist.Length];
        if (shopList.Length > 0)
        {
            for (int i = 0; i < shopList.Length; i++)
            {
                basicItems[i] = itemlist[i].GetComponent<InteractionObject>();
                Item item = ScriptableObject.CreateInstance<Item>();
                item.icon = basicItems[i].GetComponent<SpriteRenderer>().sprite;
                item.itemType = basicItems[i].itemType;
                item.name = basicItems[i].itemName;
                item.details = basicItems[i].itemDetails;
                item.isStacking = basicItems[i].isStacking;
                item.priceBuy = basicItems[i].priceBuy;
                if (item.itemType == 0)
                {
                    item.defaultOption = basicItems[i].defaultOption;
                    item.defaultPoint = basicItems[i].defaultPoint;
                }
                shopList[i] = item;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Talking()
    {
        takingMenuManager.StartTalkingMenu(this);
    }
}
