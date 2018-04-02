using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public Item itemTest;
    public bool inventory;
    public bool EquipmentItem = false;
    public bool isStacking;
    public int itemCount = 1;
    public string itemName;
    public string itemDetails;
    public Item.EquipmentSlot itemType;
    public Option defaultOption;
    public int defaultPoint;
    public List<Option> avaiableOption;
    public int maxLines;
    private float autoDestroy = 15f;
    private float timeCount = 0;
    private void Start()
    {
        itemTest = ScriptableObject.CreateInstance<Item>();
        setEquipSlot();
        itemTest.icon = GetComponent<SpriteRenderer>().sprite;
        itemTest.name = itemName;
        itemTest.count = itemCount;
        itemTest.details = itemDetails;
        itemTest.isEquipment = EquipmentItem;
        itemTest.isStacking = isStacking;
        if (itemTest.isEquipment) {
            itemTest.defaultOption = defaultOption;
            itemTest.defaultPoint = defaultPoint;
            setOption();
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
        int itemTypeSlot = (int)itemType;
        itemTest.equipSlot = itemType;
    }
    public void setOption()
    {
        if (EquipmentItem) {
            int lines = Random.Range(0, maxLines);
            itemTest.options = new Option[lines];
            itemTest.points = new int[lines];
            for (int i = 0; i < lines; i++) {
                int optionPosition = Random.Range(0, avaiableOption.Count);
                Option op = avaiableOption[optionPosition];
                itemTest.options[i] = op;
                itemTest.points[i] = Random.Range(0, itemTest.options[i].maxPoint) + 1;
                avaiableOption.RemoveAt(optionPosition);
            }
        }
    }
}
