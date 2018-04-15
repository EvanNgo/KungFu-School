using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicItem : MonoBehaviour {
    public string itemName;
    public Sprite icon;
    public Item.ItemType itemType;
    public string details;
    public int priceBuy;
    public bool isStacking;
    public int count = 1;
}
