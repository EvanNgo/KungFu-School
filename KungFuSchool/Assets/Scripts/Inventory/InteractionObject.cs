using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public Item item;
    [Header("Basic Setting")]
    public int itemCount = 1;
    public bool isStacking;
    public string itemName;
    public string itemDetails;
    public int priceBuy = 0;
    public int priceSell = 1;
    public Item.ItemType itemType;
    public bool isShopItem = false;

    [Header("Equipment Item's Setting")]
    public int itemOptionLines = 3;
    public ItemLevel itemLevel;
    public Item.EquipmentSlot EquipmentSlot;
    public Option defaultOption;
    public int defaultPoint;
    public List<Option> avaiableOption;

    [Header("Item's System Setting")]
    private float autoDestroy = 15f;
    private float timeCount = 0;
    private void Start()
    {
        item = ScriptableObject.CreateInstance<Item>();
        item.icon = GetComponent<SpriteRenderer>().sprite;
        item.itemType = itemType;
        switch (itemType)
        {
            case Item.ItemType.Equipment:
                BasicSetup();
                setEquipSlot();
                item.defaultOption = defaultOption;
                item.defaultPoint = defaultPoint;
                setOption(itemOptionLines);
                break;
            case Item.ItemType.HPPotion: 
                BasicSetup();
                break;
            case Item.ItemType.MPPotion: 
                BasicSetup();
                break;
            case Item.ItemType.Question: 
                BasicSetup();
                break;
            case Item.ItemType.Gold: 
                item.priceSell = priceSell;
                break;
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
    private void BasicSetup(){
        item.name = itemName;
        item.count = itemCount;
        item.details = itemDetails;
        item.isStacking = isStacking;
    }
    public void DoInteraction()
    {
        Destroy(gameObject);
    }
    private void setEquipSlot() {
        item.equipSlot = EquipmentSlot;
    }
    public void setOption(int maxLines)
    {
        int lines = Random.Range(0, maxLines);
        item.options = new Option[lines];
        item.points = new int[lines];
        for (int i = 0; i < lines; i++) {
            int optionPosition = Random.Range(0, avaiableOption.Count);
            Option op = avaiableOption[optionPosition];
            item.options[i] = op;
            if (op.maxPoint == op.pointPerUnit)
            {
                item.points[i] = op.pointPerUnit;
            }
            else
            {
                int maxpoint = op.maxPoint / 4 * ((int)itemLevel + 1);
                int randomRange = maxpoint / op.pointPerUnit;
                item.points[i] = Random.Range(1, randomRange + 1) * op.pointPerUnit;
            }

            avaiableOption.RemoveAt(optionPosition);
        }
    }

    public enum ItemLevel{VeryLow,Low,Normal,Hight}
}
