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
    public Item.ItemColor itemColor;

    [Header("Equipment Item's Setting")]
    public int itemOptionLines = 3;
    public Item.EquipmentSlot EquipmentSlot;
    public Option defaultOption;
    public int defaultPoint;
    public List<Option> avaiableOption;
    public int minOption;
    public int maxOption;
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
                item.equipSlot = EquipmentSlot;
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
        item.itemColor = itemColor;
    }
    public void DoInteraction()
    {
        Destroy(gameObject);
    }
    public void setOption(int maxLines)
    {
        int lines = 0;
        Debug.Log("avaiableOption:"  +avaiableOption.Count);
        if (avaiableOption.Count < maxLines)
        {
            lines = avaiableOption.Count;
            Debug.Log("maxlines:"  +maxLines);
        }
        else
        {
            lines = Random.Range(0, maxLines+1);
        }
        item.options = new Option[lines];
        item.points = new int[lines];
        if (lines == 0)
        {
            return;
        }
        Debug.Log(lines);
        for (int i = 0; i < lines; i++) {
            int optionPosition = Random.Range(0, avaiableOption.Count);
            Debug.Log(optionPosition);
            Debug.Log(avaiableOption.Count);
            Option op = avaiableOption[optionPosition];
            item.options[i] = op;
            if (op.maxPoint == op.pointPerUnit)
            {
                item.points[i] = op.pointPerUnit;
            }
            else
            {
                int randomNumber = Random.Range(minOption, maxOption) + 1;
                int opNumber =(int) (randomNumber * op.maxPoint) / 100;
                int tempOption = opNumber / op.pointPerUnit;
                item.points[i] = (tempOption != 0) ? tempOption * op.pointPerUnit : op.pointPerUnit ;

            }
            avaiableOption.RemoveAt(optionPosition);
        }
    }

    public int GetPartOption(){
        return 0;
    }
}
