using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public Item item;
    public bool inventory;
    public bool isStacking;
    public int itemCount = 1;
    public string itemName;
    public string itemDetails;
    public Item.EquipmentSlot EquipmentSlot;
    public Item.ItemType itemType;
    public Option defaultOption;
    public int defaultPoint;
    public int priceBuy = 1;
    public int priceSell = 1;
    public List<Option> avaiableOption;
    public int maxLines;
    public int gold;
    private float autoDestroy = 15f;
    private float timeCount = 0;
    private void Start()
    {
        item = ScriptableObject.CreateInstance<Item>();
        item.icon = GetComponent<SpriteRenderer>().sprite;
        item.itemType = itemType;
        if (item.itemType != Item.ItemType.Gold)
        {
            setEquipSlot();
            item.name = itemName;
            item.count = itemCount;
            item.details = itemDetails;

            item.isStacking = isStacking;
            if ((int)item.itemType == 0)
            {
                item.defaultOption = defaultOption;
                item.defaultPoint = defaultPoint;
                setOption();
            }
        }
        else
        {
            item.priceSell = gold;
        }
    }

    private void Update()
    {
        if (timeCount < autoDestroy)
        {
            timeCount += Time.deltaTime;
        }
        else
        {
            timeCount = 0;
            DoInteraction();
        }
    }
    public void DoInteraction()
    {
        Destroy(gameObject);
    }
    public void setEquipSlot() {
        item.equipSlot = EquipmentSlot;
    }
    public void setOption()
    {
        if ((int)itemType==0) {
            int lines = Random.Range(0, maxLines);
            item.options = new Option[lines];
            item.points = new int[lines];
            for (int i = 0; i < lines; i++) {
                int optionPosition = Random.Range(0, avaiableOption.Count);
                Option op = avaiableOption[optionPosition];
                item.options[i] = op;
                item.points[i] = Random.Range(0, item.options[i].maxPoint) + 1;
                avaiableOption.RemoveAt(optionPosition);
            }
        }
    }
}
