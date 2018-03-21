using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {
    
    public int id;
    new public string name = "New Item";
    public bool isEquipment;
    public string details;
    public Sprite icon = null;
    public bool isEquiping = false;
    public bool isStacking = false;
    public int count = 1;
    public Option defaultOption;
    public int defaultPoint;
    public Option[] options;
    public int[] points;
    public SkinnedMeshRenderer prefab;
    public EquipmentSlot equipSlot;
    public bool showInInventory = true;
    public virtual void Use()
    {
        Debug.Log("Used");
    }
    public enum EquipmentSlot { Head, Armor, Pant, Foot, Gloves, Ring, Pedan, Weapon, Shield, Rare}
}
