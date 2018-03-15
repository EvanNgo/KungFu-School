using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {
    public int id;
    public string itemIcon;
    public string itemName;
    public int itemColor;

    public void Use()
    {
        Debug.Log("Used");
    }
}
