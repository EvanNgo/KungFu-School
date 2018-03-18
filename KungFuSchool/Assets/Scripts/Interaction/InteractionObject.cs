using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {
    public Item itemTest;
    public bool inventory;
    public bool EquipmentItem = false;
    public string itemName;
    public string itemDetails;
    public EquipmentSlot itemType;
    public Option defaultOption;
    public int defaultPoint;
    public List<Option> avaiableOption;
    public int maxLines;
    public float autoDestroy = 15f;
    private float timeCount = 0;
    private void Start()
    {
        itemTest = ScriptableObject.CreateInstance<Item>();
        setEquipSlot();
        itemTest.icon = GetComponent<SpriteRenderer>().sprite;
        itemTest.name = itemName;
        itemTest.details = itemDetails;
        itemTest.isEquipment = EquipmentItem;
        if (itemTest.equipSlot != Item.EquipmentSlot.PotionHP && itemTest.equipSlot != Item.EquipmentSlot.PotionHP && itemTest.equipSlot != Item.EquipmentSlot.PotionHP) {
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
        switch (itemType)
        {
            case EquipmentSlot.Head:
                itemTest.equipSlot = Item.EquipmentSlot.Head;
                break;
            case EquipmentSlot.Armor:
                itemTest.equipSlot = Item.EquipmentSlot.Armor;
                break;
            case EquipmentSlot.Pant:
                itemTest.equipSlot = Item.EquipmentSlot.Pant;
                break;
            case EquipmentSlot.Foot:
                itemTest.equipSlot = Item.EquipmentSlot.Foot;
                break;
            case EquipmentSlot.Gloves:
                itemTest.equipSlot = Item.EquipmentSlot.Gloves;
                break;
            case EquipmentSlot.Ring:
                itemTest.equipSlot = Item.EquipmentSlot.Ring;
                break;
            case EquipmentSlot.Pedan:
                itemTest.equipSlot = Item.EquipmentSlot.Pedan;
                break;
            case EquipmentSlot.Weapon:
                itemTest.equipSlot = Item.EquipmentSlot.Weapon;
                break;
            case EquipmentSlot.Shield:
                itemTest.equipSlot = Item.EquipmentSlot.Shield;
                break;
            case EquipmentSlot.PotionHP:
                itemTest.equipSlot = Item.EquipmentSlot.PotionHP;
                break;
            case EquipmentSlot.PotionMP:
                itemTest.equipSlot = Item.EquipmentSlot.PotionMP;
                break;
            case EquipmentSlot.Question:
                itemTest.equipSlot = Item.EquipmentSlot.Question;
                break;
        }
    }
    public enum EquipmentSlot { Head, Armor, Pant, Foot, Gloves, Ring, Pedan, Weapon, Shield, PotionHP, PotionMP, Question }
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
