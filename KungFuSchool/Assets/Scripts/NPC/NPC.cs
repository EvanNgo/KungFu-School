using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {
    public string npcName;
    public GameObject[] itemlist;
    public BasicItem[] basicItems;
    public Item[] shopList;
    public Text txtname;
    public bool question;
    public bool shop;
    public string[] currentMessage;
    TakingMenuManager takingMenuManager;
    // Use this for initialization Kẻ Thần Bí
    // Hahaha... Bao nhiêu năm rồi ta không nhận sư đồ, nhưng nếu ngươi muốn trước hết hãy thể hiện bản lĩnh của ngươi trước đi.
    // Bằng cách nào xin hãy chỉ dạy ta!
    // Trong làng có vài cái Cọc Gỗ. Trong vòng 1 giờ đồng hồ nếu ngươi có thể hạ gục 10 cái thì hãy đến tìm ta! Trò Chuyện Cửa Hàng Nhiệm Vụ Trở Về
    // Ta đã rõ!
    private void Start()
    {
        takingMenuManager = TakingMenuManager.instance;
        txtname.text = npcName;
        shopList = new Item[itemlist.Length];
        basicItems = new BasicItem[itemlist.Length];
        if (shopList.Length > 0)
        {
            for (int i = 0; i < shopList.Length; i++)
            {
                basicItems[i] = itemlist[i].GetComponent<BasicItem>();
                Item item = ScriptableObject.CreateInstance<Item>();
                item.icon = basicItems[i].icon;
                item.itemType = basicItems[i].itemType;
                item.name = basicItems[i].itemName;
                item.details = basicItems[i].details;
                item.isStacking = basicItems[i].isStacking;
                item.priceBuy = basicItems[i].priceBuy;
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
